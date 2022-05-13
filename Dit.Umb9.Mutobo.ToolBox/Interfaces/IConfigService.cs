using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dit.Umb9.Mutobo.ToolBox.Interfaces
{
    public interface IConfigService
    {
        string GetAppSettingValue(string key, bool essential = true);
        int? GetAppSettingIntValue(string key, bool essential = true);
        bool? GetAppSettingBoolValue(string key, bool essential = true);
    }
}
