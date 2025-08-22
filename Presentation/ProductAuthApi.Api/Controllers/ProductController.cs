using Microsoft.AspNetCore.Mvc;

namespace ProductAuthApi.Api.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
