using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExchangeIt.Data;
using Volo.Abp.DependencyInjection;

namespace ExchangeIt.EntityFrameworkCore;

public class EntityFrameworkCoreExchangeItDbSchemaMigrator
    : IExchangeItDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreExchangeItDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ExchangeItDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ExchangeItDbContext>()
            .Database
            .MigrateAsync();
    }
}
