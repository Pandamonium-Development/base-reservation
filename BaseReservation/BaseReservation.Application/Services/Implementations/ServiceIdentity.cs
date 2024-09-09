using BaseReservation.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceIdentity(AuthenticationConfiguration authenticationConfiguration, IRepositoryUsuario repository,
                                IRepositoryTokenMaster repositoryTokenMaster, TokenValidationParameters tokenValidationParameters) : IServiceIdentity
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    /// <exception cref="UnAuthorizedException"></exception>
    public async Task<TokenMaster> LoginAsync(RequestUserLoginDto login)
    {
        string md5Password = Hashing.HashMd5(login.Contrasenna);
        var loginUser = await repository.LoginAsync(login.CorreoElectronico, md5Password);

        if (loginUser == null) throw new UnAuthorizedException("Correo electrónico o contraseña inválido");

        return await AuthenticateAsync(loginUser);
    }

    private async Task<AuthenticationResult> AuthenticateAsync(Usuario usuario)
    {
        var authenticationResult = new AuthenticationResult();
        var tokenHandler = new JwtSecurityTokenHandler();

        ClaimsIdentity subject = GenerarClaims(usuario);

        var tokenDescriptor = ObtenerSecurityTokenDescriptor(subject);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        authenticationResult.Token = tokenHandler.WriteToken(token);

        var refreshToken = GenerarTokenMaster(token.Id, usuario.Id);

        var tokenMaster = await repositoryTokenMaster.CreateTokenMasterAsync(refreshToken);
        if (tokenMaster == null) throw new NotFoundException("Token no almacenado");

        authenticationResult.RefreshToken = refreshToken.Token;
        authenticationResult.Success = true;

        return authenticationResult;
    }

    private ClaimsIdentity GenerarClaims(Usuario usuario)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new Claim("IdUsuario", usuario.Id.ToString()),
            new Claim("Nombre", usuario.Nombre),
            new Claim("Apellidos", usuario.Apellidos),
            new Claim("NombreCompleto", $"{usuario.Nombre} {usuario.Apellidos}"),
            new Claim("CorreoElectronico", usuario.CorreoElectronico),
            new Claim(ClaimTypes.Role, usuario.IdRolNavigation.Descripcion),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        });
    }

    private SecurityTokenDescriptor ObtenerSecurityTokenDescriptor(ClaimsIdentity subject) =>
        new SecurityTokenDescriptor
        {
            Subject = subject,
            Expires = DateTime.UtcNow.Add(authenticationConfiguration.JwtSettings_TokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authenticationConfiguration.JwtSettings_Secret)), SecurityAlgorithms.HmacSha256Signature)
        };

    private TokenMaster GenerarTokenMaster(string idToken, short idUsuario) =>
        new TokenMaster
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = idToken,
            IdUsuario = idUsuario,
            FechaCreacion = DateTime.UtcNow,
            Utilizado = false,
            FechaVencimiento = DateTime.UtcNow.AddMonths(6)
        };

    /// <inheritdoc />
    public async Task<TokenMaster> RefreshTokenAsync(TokenMaster request)
    {
        var response = new AuthenticationResult();
        var authResponse = await GetRefreshTokenAsync(request.Token, request.RefreshToken);
        if (!authResponse.Success) throw new ArtInkException("No se pudo obtener el token actualizado");

        response.Token = authResponse.Token;
        response.RefreshToken = authResponse.RefreshToken;

        return response;
    }

    private async Task<AuthenticationResult> GetRefreshTokenAsync(string token, string refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(token);
        if (validatedToken == null) return new AuthenticationResult { Errors = new[] { "Token Inválido" } };

        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value));

        if (expiryDateTimeUtc > DateTime.UtcNow) return new AuthenticationResult { Errors = new[] { "Token aun no ha expirado" } };

        if (!await repositoryTokenMaster.ExisteTokenMaster(refreshToken)) throw new NotFoundException("Token no encontrado.");
        var existingRefreshToken = await repositoryTokenMaster.GetTokenMasterAsync(refreshToken);

        if (existingRefreshToken == null) return new AuthenticationResult { Errors = new[] { "Token no existe" } };
        if (DateTime.UtcNow > existingRefreshToken.FechaVencimiento) return new AuthenticationResult { Errors = new[] { "El token de actualización ya expiró" } };
        if (existingRefreshToken.Utilizado) return new AuthenticationResult { Errors = new[] { "El token de actualización ya ha sido usado" } };
        if (existingRefreshToken.JwtId != validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value) return new AuthenticationResult { Errors = new[] { "El token de actualización no coincide con el JWt" } };

        existingRefreshToken.Utilizado = true;
        await repositoryTokenMaster.UpdateTokenMasterAsync(existingRefreshToken);
        var usuario = await ObtenerUsuarioAsync(validatedToken.Claims.Single(x => x.Type == "IdUsuario").Value);

        return await AuthenticateAsync(usuario);
    }

    private async Task<Usuario> ObtenerUsuarioAsync(string idUsuario)
    {
        short IdUsuario = 0;
        short.TryParse(idUsuario, out IdUsuario);
        var usuario = await repository.FindByIdAsync(IdUsuario);
        if (usuario == null) throw new NotFoundException("Usuario no encontrado.");

        return usuario;
    }

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

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) => (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
}
