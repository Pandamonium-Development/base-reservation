using BaseReservation.Application.Common;
using BaseReservation.Application.Configuration.Authentication;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs.Authentication;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using BaseReservation.Utils;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceIdentity(AuthenticationConfiguration authenticationConfiguration, IRepositoryUser repository,
                                IRepositoryTokenMaster repositoryTokenMaster, TokenValidationParameters tokenValidationParameters) : IServiceIdentity
{
    /// <inheritdoc />    
    public async Task<TokenModel> LoginAsync(RequestUserLoginDto login)
    {
        string md5Password = Hashing.HashMd5(login.Password);
        var loginUser = await repository.LoginAsync(login.Email, md5Password);

        if (loginUser == null) throw new UnAuthorizedException("Correo electrónico o contraseña inválido");

        return await AuthenticateAsync(loginUser);
    }

    /// <inheritdoc />
    public async Task<TokenModel> RefreshTokenAsync(TokenModel request)
    {
        var response = new AuthenticationResult();
        var authResponse = await GetRefreshTokenAsync(request.Token, request.RefreshToken);
        if (!authResponse.Success) throw new BaseReservationException("No se pudo obtener el token actualizado");

        response.Token = authResponse.Token;
        response.RefreshToken = authResponse.RefreshToken;

        return response;
    }

    /// <summary>
    /// Generate the token from a valid authentication
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns>AuthenticationResult</returns>
    private async Task<AuthenticationResult> AuthenticateAsync(User user)
    {
        var authenticationResult = new AuthenticationResult();
        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsIdentity subject = GenerateClaims(user);

        var tokenDescriptor = GetSecurityTokenDescriptor(subject);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        authenticationResult.Token = tokenHandler.WriteToken(token);

        var refreshToken = GenerateTokenMaster(token.Id, user.Id);

        var tokenMaster = await repositoryTokenMaster.CreateTokenMasterAsync(refreshToken);
        if (tokenMaster == null) throw new NotFoundException("Token no almacenado");

        authenticationResult.RefreshToken = refreshToken.Token;
        authenticationResult.Success = true;

        return authenticationResult;
    }

    /// <summary>
    /// Generate all the claims needed on the JWT
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns>ClaimsIdentity</returns>
    private ClaimsIdentity GenerateClaims(User user)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("FullName", $"{user.FirstName} {user.LastName}"),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.RoleIdNavigation.Description),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        });
    }

    /// <summary>
    /// Get security token descriptor for claims identity
    /// </summary>
    /// <param name="subject">Claims identity subject</param>
    /// <returns>SecurityTokenDescriptor</returns>
    private SecurityTokenDescriptor GetSecurityTokenDescriptor(ClaimsIdentity subject) =>
        new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.Add(authenticationConfiguration.JwtSettings_TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationConfiguration.JwtSettings_Secret)), SecurityAlgorithms.HmacSha256Signature)
        };

    /// <summary>
    /// Generate element that contains information to be saved on the database
    /// </summary>
    /// <param name="tokenId">Token id</param>
    /// <param name="userId">User id</param>
    /// <returns>TokenMaster</returns>
    private TokenMaster GenerateTokenMaster(string tokenId, short userId) =>
        new TokenMaster
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = tokenId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            Used = false,
            ExpireAt = DateTime.UtcNow.AddMonths(6)
        };

    /// <summary>
    /// Get the refresh token information
    /// </summary>
    /// <param name="token">Jwt token</param>
    /// <param name="refreshToken">Jwt refresh token</param>
    /// <returns>AuthenticationResult</returns>
    private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(token);
        if (validatedToken == null) return new AuthenticationResult { Errors = new[] { "Token Inválido" } };

        var expiryDateTimeUtc = DateTime.UnixEpoch
            .AddSeconds(long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value));

        if (expiryDateTimeUtc > DateTime.UtcNow) return new AuthenticationResult { Errors = new[] { "Token aun no ha expirado" } };

        if (!await repositoryTokenMaster.ExistsTokenMasterAsync(refreshToken)) throw new NotFoundException("Token no encontrado.");
        var existingRefreshToken = await repositoryTokenMaster.FindByTokenAsync(refreshToken);

        if (existingRefreshToken == null) return new AuthenticationResult { Errors = new[] { "Token no existe" } };
        if (DateTime.UtcNow > existingRefreshToken.ExpireAt) return new AuthenticationResult { Errors = new[] { "El token de actualización ya expiró" } };
        if (existingRefreshToken.Used) return new AuthenticationResult { Errors = new[] { "El token de actualización ya ha sido usado" } };
        if (existingRefreshToken.JwtId != validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value) return new AuthenticationResult { Errors = new[] { "El token de actualización no coincide con el JWt" } };

        existingRefreshToken.Used = true;
        await repositoryTokenMaster.UpdateTokenMasterAsync(existingRefreshToken);
        var user = await GetUserAsync(validatedToken.Claims.Single(x => x.Type == "IdUsuario").Value);

        return await AuthenticateAsync(user);
    }

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="userId">Id to look for</param>
    /// <returns>Usuario</returns>
    private async Task<User> GetUserAsync(string userId)
    {
        short userIdParsed;
        short.TryParse(userId, out userIdParsed);
        var user = await repository.FindByIdAsync(userIdParsed);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");

        return user;
    }

    /// <summary>
    /// Get Claim from jwt token
    /// </summary>
    /// <param name="token">Jwt token</param>
    /// <returns>ClaimsPrincipal</returns>
    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var validationParameters = tokenValidationParameters.Clone();
            validationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken)) return null!;

            return principal;
        }
        catch
        {
            return null!;
        }
    }

    /// <summary>
    /// Validate if jwt is using the correct security algorithm
    /// </summary>
    /// <param name="validatedToken">Jwt security token</param>
    /// <returns>bool</returns>
    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) => (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
}
