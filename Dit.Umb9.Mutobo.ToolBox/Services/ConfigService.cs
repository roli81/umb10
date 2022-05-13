using Dit.Umb9.Mutobo.ToolBox.Common.Exceptions;
using Dit.Umb9.Mutobo.ToolBox.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.Web;

namespace Dit.Umb9.Mutobo.ToolBox.Services
{
    public class ConfigService : BaseService, IConfigService
    {
        private readonly IConfiguration _configuration;

        public ConfigService(
    IConfiguration configuration,
    ILogger<ConfigService> logger,
    IUmbracoContextAccessor contextAccessor)
    : base(logger, contextAccessor)
        {
            _configuration = configuration;
        }


        public bool? GetAppSettingBoolValue(string key, bool essential = true)
        {
            var result = _configuration.GetValue<bool?>(key);

            if (result == null)
            {
                if (essential)
                    throw new AppSettingsException($"Missing config/AppSettings.config or config entry: {key}");
                else
                    Logger.LogWarning($"Missing config/AppSettings.config or config entry: {key}");
            }

            return result;
        }

        public int? GetAppSettingIntValue(string key, bool essential = true)
        {
            var result = _configuration.GetValue<int?>(key);

            if (result == null)
            {
                if (essential)
                    throw new AppSettingsException($"Missing config/AppSettings.config or config entry: {key}");
                else
                    Logger.LogWarning($"Missing config/AppSettings.config or config entry: {key}");
            }

            return result;
        }

        public string GetAppSettingValue(string key, bool essential = true)
        {
            var result = _configuration.GetValue<string>(key);

            if (result == null)
            {
                if (essential)
                    throw new AppSettingsException($"Missing config/AppSettings.config or config entry: {key}");
                else
                    Logger.LogWarning($"Missing config/AppSettings.config or config entry: {key}");
            }

            return result;
        }
    }
}

