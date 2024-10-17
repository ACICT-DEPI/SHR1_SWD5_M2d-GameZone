using GameZone.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GameZone.Controllers
{
	public class ShopController : Controller
	{
		private readonly IGamesService _gamesService;
		public ShopController(IGamesService gamesService)
        {
			_gamesService = gamesService;
		}
		public IActionResult Index()
		{
			var games = _gamesService.GetAll();
			return View(games);
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
