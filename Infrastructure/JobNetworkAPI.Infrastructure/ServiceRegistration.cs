﻿using JobNetworkAPI.Application.Abstractions.Services.Configurations;
using JobNetworkAPI.Application.Abstractions.Storage;
using JobNetworkAPI.Application.Abstractions.Token;
//using JobNetworkAPI.Application.Services;
using JobNetworkAPI.Infrastructure.Enums;
using JobNetworkAPI.Infrastructure.Services.Configurations;
//using JobNetworkAPI.Infrastructure.Services;
using JobNetworkAPI.Infrastructure.Services.Storage;
using JobNetworkAPI.Infrastructure.Services.Storage.Azure;
using JobNetworkAPI.Infrastructure.Services.Storage.Local;
using JobNetworkAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobNetworkAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
            serviceCollection.AddScoped<IApplicationService, ApplicationService>();

        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : Storage, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();

                    break;
                case StorageType.AWS:

                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}