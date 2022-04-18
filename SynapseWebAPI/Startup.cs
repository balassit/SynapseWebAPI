using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using SynapseWebAPI.Provider;

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
            services
                .AddControllers()
                .AddOData(options =>
                {
                    options.EnableQueryFeatures();
                });

            services
                .AddSingleton<IODataModelProvider, ODataModelProvider>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddScoped<IQuerySynapse, QuerySynapse>()
                .AddDbContext<AzureSynapseContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("AzureSynapseContext")));
            
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ODataRoutingApplicationModelProvider>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<MatcherPolicy, ODataRoutingMatcherPolicy>());
            services.AddSwaggerGen();

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

            app.UseODataRouteDebug(); // Remove it if not needed

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData 8.x OpenAPI");
            });

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();

            app.Use(async (context, next) =>
            {
                // Opt out of MIME type sniffing ref - https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });
        }
    }
}