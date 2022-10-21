using ExchangeIt.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace ExchangeIt.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(ExchangeItEntityFrameworkCoreModule),
    typeof(ExchangeItApplicationContractsModule)
    )]
public class ExchangeItDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
