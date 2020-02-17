using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.ViewModels.Home;
using System.Linq;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SulsDbContext db;

        public HomeController(SulsDbContext db)
        {
            this.db = db;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var problems = db.Problems.Select(x => new IndexProblemViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Count = x.Submissions.Count(),
                }).ToList();

                var loggedInViewModel = new LoggedInViewModel()
                {
                    Username = this.db.Users
                        .Where(x => x.Id == this.User)
                        .Select(x => x.Username)
                        .FirstOrDefault(),
                    Problems = problems
                };

                return this.View(loggedInViewModel, "IndexLoggedIn");
            }

            return this.View();
        }
    }
}