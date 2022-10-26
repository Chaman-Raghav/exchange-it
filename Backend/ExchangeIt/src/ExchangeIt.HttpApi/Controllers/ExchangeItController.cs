using ExchangeIt.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ExchangeIt.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ExchangeItController : AbpControllerBase
{
    protected ExchangeItController()
    {
        LocalizationResource = typeof(ExchangeItResource);
    }
}
