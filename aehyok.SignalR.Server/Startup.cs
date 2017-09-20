﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.SignalR.Server
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("fiver",
                    policy => policy.AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod());
            });

            services.AddSignalR(); // <-- SignalR
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseCors("fiver");

            app.UseSignalR(routes =>  // <-- SignalR
            {
                routes.MapHub<Chat>("Chat");
            });
        }
    }
}
