using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Middlewares;

namespace WebApplication1
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
            services.AddRazorPages();
            //services.AddMvc(options=>options.EnableEndpointRouting = false);
            services.AddMvc();
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

            // app.UseHttpsRedirection();
            // DefaultFilesOptions option = new DefaultFilesOptions();
            // option.DefaultFileNames.Clear();
            // option.DefaultFileNames.Add("/pages/abc.html");
            //app.UseDefaultFiles(option);//change the request path to default file stored in webroot folder
            //app.UseStaticFiles();//process static files included in default webroot folder
            //app.UseStaticFiles(new StaticFileOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Htmlpages")),
            //     RequestPath= "/Htmlpages"
            // });
            app.UseMyMiddleware();
            FileServerOptions options = new FileServerOptions();
            options.DefaultFilesOptions.DefaultFileNames.Clear();//clearing default names for default files
            options.DefaultFilesOptions.DefaultFileNames.Add("/pages/abc.html");
            //app.UseFileServer(options);

            
            app.UseRouting();
           // app.UseMvcWithDefaultRoute();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            app.Run(async (context) =>
            {
                //throw new Exception("Exception occurred");
                await context.Response.WriteAsync(Directory.GetCurrentDirectory().ToString());
            });
        }
    }
}
