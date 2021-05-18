﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thesis.Application.Common.Interfaces;
using Thesis.Infrastructure.Services;

namespace Thesis.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ITransaction, Transaction>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDateTime, DateTimeService>();
            services.AddScoped<IDataSeederService, DataSeederService>();

            return services;
        }
    }
}