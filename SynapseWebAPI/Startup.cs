using Microsoft.EntityFrameworkCore;

namespace SynapseWebAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IQuerySynapse, QuerySynapse>()
                .AddDbContext<AzureSynapseContext>(options =>
                    options.UseAzureSynapse(Configuration.GetConnectionString("AzureSynapseContext")));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(async (context, next) =>
            {
                // Opt out of MIME type sniffing ref - https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });
        }
    }
}