using System.Threading.Tasks;

namespace ExchangeIt.Data;

public interface IExchangeItDbSchemaMigrator
{
    Task MigrateAsync();
}
