﻿using Application.Core;
using Application.GetTogethers;
using Application.Interfaces;
using Infrastructure.Cloud;
using Infrastructure.Photos;
using Infrastructure.Resumes;
using Infrastructure.Security;
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

            services.AddScoped<IUserAccessor, UserAccessor>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddCors(options=> {
                options.AddPolicy("CorsPolicy",builder=> {
                    builder.
                        AllowAnyMethod().
                        AllowAnyHeader().
                        AllowCredentials().
                        WithOrigins("http://localhost:3000");    
                });
            });
            services.AddScoped<IPhotoAccessor, PhotoAccessor>();
            services.AddScoped<IResumeAccessor, ResumeAccessor>();
            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));
            services.AddSignalR();
            return services;
        }
    }
}
