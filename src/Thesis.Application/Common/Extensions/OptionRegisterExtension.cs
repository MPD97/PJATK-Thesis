using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thesis.Application.Common.Extensions
{
    public static class OptionRegisterExtension
    {
        public static T RegisterExtensionOptions<T>(IServiceCollection services, IConfiguration configuration, string sectionName) where T : class, new()
        {
            if (string.IsNullOrWhiteSpace(sectionName)) throw new Exception("Section name cannot be null or empty");

            var config = configuration.GetSection(sectionName);
            if (config is null)
            {
                var message = $"appsettigs.json is missing configuration for section: {sectionName}";
                Log.Error(message);

                throw new Exception(message);
            }

            services.Configure<T>(config);

            return GetExtensionOptions<T>(configuration, sectionName);
        }

        private static T GetExtensionOptions<T>(IConfiguration configuration, string sectionName)
            where T : class, new()
        {
            var extensionOptions = new T();
            configuration.GetSection(sectionName).Bind(extensionOptions);

            return extensionOptions;
        }
    }
}
