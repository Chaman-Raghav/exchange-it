using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace ExchangeIt.Data;

/* This is used if database provider does't define
 * IExchangeItDbSchemaMigrator implementation.
 */
public class NullExchangeItDbSchemaMigrator : IExchangeItDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
