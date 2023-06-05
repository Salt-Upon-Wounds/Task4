using Task4.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Task4.Models;
using Task4.Services;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> userManager;
        private readonly IUserService userService;
        private readonly SignInManager<User> signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<User> userManager, IUserService userService, SignInManager<User> signInManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.userService = userService;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        [HttpPost]
        public IActionResult BlockUser()
        {
            var idsToBlock = HttpContext.Request.Form["userId"];
            userService.ToggleStatusById(idsToBlock, HttpContext.User, true);
            return RedirectPermanent("~/Home/Index");
        }

        [HttpPost]
        public IActionResult UnblockUser()
        {
            var idsToUnblock = HttpContext.Request.Form["userId"];
            userService.ToggleStatusById(idsToUnblock, HttpContext.User, false);
            return RedirectPermanent("~/Home/Index");
        }

        [HttpPost]
        public IActionResult DeleteUser()
        {
            var idsToDelete = HttpContext.Request.Form["userId"];
            userService.DeleteById(idsToDelete, HttpContext.User);
            return RedirectPermanent("~/Home/Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}