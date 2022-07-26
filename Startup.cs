using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Middleware;
using WebApplication1.Model;

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
            app.UseFetchStudentMiddleware();
            app.Use(async (context, NextMiddleware) =>
            {
                //1 - Operate on the request

                    Console.WriteLine($"Got request. Method={context.Request.Method} Path={context.Request.Path}");

                    var sw = Stopwatch.StartNew();

                    //2 - Call the next middleware
                    await NextMiddleware();

                    //3 - Operate on the response
                    sw.Stop();
                    Console.WriteLine($"Request finished. Method={context.Request.Method} Path={context.Request.Path} StatusCode={context.Response.StatusCode} ElapsedMilliseconds={sw.ElapsedMilliseconds}");
                
            });

            //app.Map("/", a => a.Run(async context =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.WriteAsync("Testing Path");
            //}));
            //app.Map("/string", a => a.UseMiddleware<FetchStudentMiddleware>);
            //app.MapWhen(context => context.Request.Path.StartsWithSegments("/{alpha}", StringComparison.OrdinalIgnoreCase)), appBuilder =>
            //{
            //    appBuilder.UseFetchStudentMiddleware();
            //});
            //app.MapWhen("/{alpha}", });
            app.UseEndpoints(endpoints =>
            {
                //endpoints.Map("/{alpha}", ShowStudentInfo);
                endpoints.Map("", async context =>
                {
                    //Will Exceute if we exception occurs in run block, however if exception occur before run block it will throw error in compile time
                    //throw new Exception("Sample Exception");
                    //Student student = GetStudentInfo(context.Request.Path.ToString());
                    //if(student != null)
                    //{
                    //    context.Items["Student"] = student;
                    //    app.UseMiddleware<FetchStudentMiddleware>();
                    //}

                    await context.Response.WriteAsync("Hello World!");
                });
            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapRazorPages();
            //});
        }

        private void NewFunction(IApplicationBuilder obj)
        {
            throw new NotImplementedException();
        }

       

    }
}
