using System;
using System.Collections.Generic;
using System.Text;
using ExchangeIt.Localization;
using Volo.Abp.Application.Services;

namespace ExchangeIt;

/* Inherit your application services from this class.
 */
public abstract class ExchangeItAppService : ApplicationService
{
    protected ExchangeItAppService()
    {
        LocalizationResource = typeof(ExchangeItResource);
    }
}
