using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Services;
using SulsApp.ViewModels;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            var userId = this.User;
            var model = new IndexViewModel
            {
                Message = Constants.WelcomeMessage,
                Username = usersService.GetUsername(userId) ?? Constants.NotLogged
            };

            return this.View(model); //return View("Index");
        }
    }
}
