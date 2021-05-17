using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            if (string.IsNullOrWhiteSpace(sectionName)) throw new Exception("Section name cannot be null or whitespace");

            services.Configure<T>(configuration.GetSection(sectionName));

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
