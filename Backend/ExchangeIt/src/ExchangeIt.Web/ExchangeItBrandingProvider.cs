using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ExchangeIt.Web;

[Dependency(ReplaceServices = true)]
public class ExchangeItBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ExchangeIt";
}
