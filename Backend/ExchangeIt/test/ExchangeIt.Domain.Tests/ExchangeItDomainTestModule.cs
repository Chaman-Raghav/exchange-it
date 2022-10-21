using ExchangeIt.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace ExchangeIt;

[DependsOn(
    typeof(ExchangeItEntityFrameworkCoreTestModule)
    )]
public class ExchangeItDomainTestModule : AbpModule
{

}
