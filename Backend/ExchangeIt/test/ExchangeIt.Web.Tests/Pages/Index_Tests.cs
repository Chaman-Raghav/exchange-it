using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace ExchangeIt.Pages;

public class Index_Tests : ExchangeItWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
