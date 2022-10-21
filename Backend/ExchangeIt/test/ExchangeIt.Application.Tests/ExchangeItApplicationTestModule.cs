using Volo.Abp.Modularity;

namespace ExchangeIt;

[DependsOn(
    typeof(ExchangeItApplicationModule),
    typeof(ExchangeItDomainTestModule)
    )]
public class ExchangeItApplicationTestModule : AbpModule
{

}
