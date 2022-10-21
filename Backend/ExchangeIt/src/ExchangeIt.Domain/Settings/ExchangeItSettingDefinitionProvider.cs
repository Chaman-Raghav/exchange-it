using Volo.Abp.Settings;

namespace ExchangeIt.Settings;

public class ExchangeItSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ExchangeItSettings.MySetting1));
    }
}
