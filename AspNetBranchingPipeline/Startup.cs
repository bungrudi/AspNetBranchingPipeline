using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetBranchingPipeline
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Map("/path1", ConfigurePipeline1);
            app.Map("/path2", ConfigurePipeline2);

        }

        private void ConfigurePipeline1(IApplicationBuilder app)
        {
            app.Use(async (context, nextDelegate) => await new Middleware10().Invoke(context,nextDelegate));
            app.Use(async (context, nextDelegate) => await new Middleware11().Invoke(context,nextDelegate));
            app.Run(async (context) => await context.Response.WriteAsync("you are requesting resource at /path1"));
        }
        
        private void ConfigurePipeline2(IApplicationBuilder app)
        {
            app.Use(async (context, nextDelegate) => await new Middleware20().Invoke(context,nextDelegate));
            app.Use(async (context, nextDelegate) => await new Middleware21().Invoke(context,nextDelegate));
            app.Run(async (context) => await context.Response.WriteAsync("you are requesting resource at /path2"));
        }
    }

    public class Middleware10
    {
        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello from Middleware10... \r\n");
            await next.Invoke();
        }
    }

    public class Middleware11
    {
        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello from Middleware11... \r\n");
            await next.Invoke();
        }
    }
    
    public class Middleware20
    {
        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello from Middleware20... \r\n");
            await next.Invoke();
        }
    }

    public class Middleware21
    {
        public async Task Invoke(HttpContext context, Func<Task> next)
        {
            await context.Response.WriteAsync("Hello from Middleware21... \r\n");
            await next.Invoke();
        }
    }
}