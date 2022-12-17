using HellTakerAniX.Models;

using Microsoft.Extensions.Configuration;

namespace HellTakerAniX.Managers;

internal class SettingManager
{
    public static SettingManager Instance => _instance?.Value;

    private static readonly Lazy<SettingManager> _instance = new(() => new());

    public AppSetting Setting { get; init; }

    private SettingManager()
    {
        IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", true, true)
                .Build();

        Setting = config.GetRequiredSection("AppSetting")
            .Get<AppSetting>();
    }
}
