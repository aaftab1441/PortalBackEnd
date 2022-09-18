using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EaglePortal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()


                        .WithHeaders(HeaderNames.AccessControlAllowOrigin, "*")
                        .WithHeaders(HeaderNames.AccessControlAllowMethods, "*")
                        .WithHeaders(HeaderNames.AccessControlAllowHeaders, "*");
                    });

            });
            services.AddRazorPages();
           // services.Configure<RouteOptions>(options => options.LowercaseUrls = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
           
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("AnyOrigin");
            app.MapWhen(
                context => {
                    var path = context.Request.Path.Value.ToLower();
                    return
                        
                        path.StartsWith("/eagle") ||
                        path.StartsWith("/merchant/") ||
                        path.StartsWith("/user-management") ||
                        path.StartsWith("/salesAgent") ||
                        path.StartsWith("/salesOffice") ||
                        path.StartsWith("/subIso");
                },
            config => config.UseStaticFiles());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "config", pattern: "Config/**");
                endpoints.MapControllerRoute(name: "login", pattern: "Login/**");
                endpoints.MapControllerRoute(name: "fees", pattern: "Fees/**");
                endpoints.MapControllerRoute(name: "dashboard", pattern: "Dashboard/**");
                endpoints.MapControllerRoute(name: "security", pattern: "Security/**");
                endpoints.MapControllerRoute(name: "merchantList", pattern: "merchantList/**");
                endpoints.MapControllerRoute(name: "merchantSearch", pattern: "merchantSearch/**");
                endpoints.MapControllerRoute(name: "iso", pattern: "Iso/**");
                endpoints.MapControllerRoute(name: "salesAgent", pattern: "salesAgent/**");
                endpoints.MapControllerRoute(name: "salesOffice", pattern: "salesOffice/**");
                endpoints.MapControllerRoute(name: "subIso", pattern: "subIso/**");


                endpoints.MapRazorPages();
            });
        }
    }
}
