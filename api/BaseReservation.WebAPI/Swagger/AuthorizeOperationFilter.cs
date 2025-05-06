using System.Reflection;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BaseReservation.WebAPI.Swagger;

/// <summary>
/// Authorize Operation filter class
/// </summary>
public class AuthorizeOperationFilter : IOperationFilter
{
    private static OpenApiSecurityScheme BearerSchema = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };

    /// <summary>
    /// Apply authorization filter option
    /// </summary>
    /// <param name="operation">Open api operation</param>
    /// <param name="context">Operation context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authorizeAttributeOnMethod = context.MethodInfo.GetCustomAttributes<BaseReservationAuthorizeAttribute>().Any();
        var authorizeAttributeOnClass = context.MethodInfo.DeclaringType?.GetCustomAttributes<BaseReservationAuthorizeAttribute>().Any() ?? false;
        var authorizeAttributeOnParentClass = context.MethodInfo.DeclaringType?.BaseType?.GetCustomAttributes<BaseReservationAuthorizeAttribute>().Any() ?? false;
        var allowAnonymusOnMethod = context.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>() != null;

        if (authorizeAttributeOnMethod || ((authorizeAttributeOnClass || authorizeAttributeOnParentClass) && !allowAnonymusOnMethod))
        {
            operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(), new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add(StatusCodes.Status403Forbidden.ToString(), new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [BearerSchema] = new List<string> { "Bearer" }
                }
            };
        }
    }
}