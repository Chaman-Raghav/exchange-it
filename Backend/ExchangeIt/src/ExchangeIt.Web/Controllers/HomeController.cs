using Microsoft.AspNetCore.Mvc;

namespace ExchangeIt.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
