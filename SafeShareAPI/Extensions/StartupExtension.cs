using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SafeShareAPI.Provider;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using static SafeShareAPI.Provider.AccessProvider;

namespace SafeShareAPI.Extensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddExtCors(this IServiceCollection services)
        {
            services.AddCors();
            return services;
        }
        public static IServiceCollection AddExtHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
            return services;
        }
        public static IServiceCollection AddExtAuthentication(this IServiceCollection services)
        {
            byte[] AuthKey = Encoding.ASCII.GetBytes(ConfigProvider.EncryptionKey);
            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(a =>
            {
                a.RequireHttpsMetadata = false;
                a.TokenValidationParameters = new()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(AuthKey),
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false
                };
            });
            return services;
        }
        public static IServiceCollection AddExtControllers(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            return services;
        }
        public static IServiceCollection AddExtApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    // defaults to the latest available version
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                });

            services.AddVersionedApiExplorer(option =>
            {
                // Add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                option.GroupNameFormat = "'v'VVV";

                // This option will instruct the API explorer to substitute API version parameters that are in the route template with the corresponding API version value
                option.SubstituteApiVersionInUrl = true;
            });
            return services;
        }
        public static IServiceCollection AddExtSwagger(this IServiceCollection services)
        {
            IApiVersionDescriptionProvider provider = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
            services.AddSwaggerGen(c =>
            {
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions.Reverse())
                { c.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description)); }
                //c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
                AddSwaggerAuthentication(c);
            });

            return services;
        }
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            return new() { Title = "SafeShare API V1.0", Version = description.ApiVersion.ToString(), Description = description.IsDeprecated ? "This API Version is Deprecated." : "" };
        }
        private static void AddSwaggerAuthentication(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.AddSecurityDefinition("Bearer", new()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Please insert JWT with Bearer into field",
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }

    public static class ConfigureProvider
    {
        public static IApplicationBuilder UseExtCors(this IApplicationBuilder app)
        {
            app.UseCors(o => o.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            return app;
        }
        public static IApplicationBuilder UseExtSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseDeveloperExceptionPage();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "SafeShareAPI"); });
            return app;
        }
        public static IApplicationBuilder UseExtHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/", new HealthCheckOptions() { Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
            return app;
        }
    }

    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params SystemUserType[] allowedRoles)
        {
            IEnumerable<string> allowedRolesAsStrings = allowedRoles.Select(a => Enum.GetName(typeof(SystemUserType), a));
            Roles = string.Join(",", allowedRolesAsStrings);
        }
    }
}
