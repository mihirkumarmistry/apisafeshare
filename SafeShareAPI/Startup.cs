using SafeShareAPI.Extensions;

namespace SafeShareAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) { Configuration = configuration; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExtControllers()
                    .AddExtAuthentication()
                    .AddExtCors()
                    .AddExtApiVersion()
                    .AddExtSwagger()
                    .AddExtHealthChecks()
                    .AddHttpContextAccessor()
                    .AddExtTokenDependency();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }
            app.UseHttpsRedirection()
                .UseRouting()
                .UseExtCors()
                .UseExtHealthChecks();
            app.UseAuthentication()
                .UseAuthorization();
            app.UseExtSwagger();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}