using IRunes.Data;
using IRunes.ViewModels.Home;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly RunesDbContext db;

        public HomeController(RunesDbContext db)
        {
            this.db = db;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {

                var loggedInViewModel = new IndexViewModel
                {
                    Username = this.db.Users
                        .Where(x => x.Id == this.User)
                        .Select(x => x.Username)
                        .FirstOrDefault()
                };

                return this.View(loggedInViewModel, "Home");
            }

            return this.View();
        }


        [HttpGet("/Home/Index")]
        public HttpResponse IndexFullPage()
        {
            return this.Index();
        }
    }
}
