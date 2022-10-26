using ExchangeIt.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ExchangeIt.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class ExchangeItPageModel : AbpPageModel
{
    protected ExchangeItPageModel()
    {
        LocalizationResourceType = typeof(ExchangeItResource);
    }
}
