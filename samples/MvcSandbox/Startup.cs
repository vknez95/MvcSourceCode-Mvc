// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MvcSandbox.Middleware;

namespace MvcSandbox
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingMiddleware>();

            // app.Use(async (context, next) =>
            // {
            //     if (context.Request.Path.Value.Contains("home"))
            //     {
            //         await context.Response.WriteAsync("Response Generated From Use");
            //     }
            //     else
            //     {
            //         Debug.WriteLine("=== Before Run ===");
            //         await next.Invoke();
            //         Debug.WriteLine("=== After Run ===");
            //     }
            // });

            // app.Run(async context =>
            // {
            //     Debug.WriteLine("=== During Run ====");
            //     await context.Response.WriteAsync("The response from Run.");
            // });

            // app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //         name: "marketing",
            //         template: "home/index",
            //         defaults: new { controller = "home", action = "splash" });

            //     routes.MapRoute(
            //         name: "default",
            //         template: "{controller=Home}/{action=Index}/{id?}");
            // });
        }

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureLogging(factory =>
                {
                    factory
                        .AddConsole()
                        .AddDebug();
                })
                .UseIISIntegration()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}

