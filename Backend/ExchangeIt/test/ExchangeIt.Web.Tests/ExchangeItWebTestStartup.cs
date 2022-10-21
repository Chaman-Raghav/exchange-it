using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace ExchangeIt;

public class ExchangeItWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<ExchangeItWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
