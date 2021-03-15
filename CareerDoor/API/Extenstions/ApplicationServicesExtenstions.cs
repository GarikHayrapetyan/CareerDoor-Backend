﻿using Application.Core;
using Application.GetTogethers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;


namespace API.Extenstions
{
    public static class ApplicationServicesExtenstions
    {
        public static IServiceCollection  AddApplicationServices(this IServiceCollection services, 
            IConfiguration Configuration) 
        {
            services.AddDbContext<DataContext>(op =>
            {
                op.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            services.AddMediatR(typeof(List.Handler).Assembly);

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });`
            });


            return services;
        }
    }
}
