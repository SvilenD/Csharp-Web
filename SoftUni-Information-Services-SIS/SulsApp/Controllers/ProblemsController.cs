using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Services;

namespace SulsApp.Controllers
{
    public class ProblemsController  : Controller
    {
        private readonly IProblemsService problemsService;

        public ProblemsController(IProblemsService problemsService)
        {
            this.problemsService = problemsService;
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, int points)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            if (string.IsNullOrEmpty(name))
            {
                return this.Error(Constants.InvalidNameLength);
            }

            if (points <= 0 || points > 100)
            {
                return this.Error(Constants.ProblemPointsRange);
            }

            this.problemsService.CreateProblem(name, points);
            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            var viewModel = this.problemsService.GetProblemDetails(id);

            return this.View(viewModel);
        }
    }
}