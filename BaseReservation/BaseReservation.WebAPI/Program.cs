using BaseReservation.Infrastructure.Configuration;
using BaseReservation.Application.Configuration;
using BaseReservation.WebAPI.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using BaseReservation.Utils.Converter;
using BaseReservation.WebAPI.Authorization;
using BaseReservation.WebAPI.Swagger;

var BaseReservationSpecificOrigins = "_BaseReservationSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                                                    {
                                                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                                        options.SerializerSettings.Converters.Add(new StringEnumConverter());
                                                        options.SerializerSettings.Converters.Add(new DateOnlyJsonConverter());
                                                        options.SerializerSettings.Converters.Add(new TimeOnlyJsonConverter());
                                                    });

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("BaseReservation", p =>
    {
        p.RequireAuthenticatedUser();
        p.AddRequirements(new IdentifiedUser());
        p.Build();
    });
});

//Configure api versioning
builder.Services.ConfigureApiVersioning();

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

//Configure Infrastructure IoC
builder.Services.ConfigureInfraestructure();

//Configure Application, Mapper and Fluent Validation
builder.Services.ConfigureApplication();
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureFluentValidation();

builder.Services.ConfigureSwagger();

//Configure database 
builder.Services.ConfigureDataBase(configuration);

//Configure authentication
builder.Services.ConfigureAuthentication(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: BaseReservationSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:44378",
                                             "http://localhost:5000",
                                             "https://localhost:44378",
                                             "https://localhost:5000",
                                             "https://localhost:5191",
                                             "http://localhost:5191")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});


var app = builder.Build();

app.LoadSwagger();

app.UseHttpsRedirection();

app.UseCors(BaseReservationSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// var logger = app.Services.GetRequiredService<ILogger<Program>>();
// app.ConfigureExceptionHandler(logger);

await app.RunAsync();
