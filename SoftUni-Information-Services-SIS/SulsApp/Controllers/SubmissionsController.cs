using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Services;

namespace SulsApp.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;

        public SubmissionsController(ISubmissionsService submissionsService)
        {
            this.submissionsService = submissionsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            var problem = this.submissionsService.GetProblemView(id);

            if (problem == null)
            {
                return this.Error(Constants.ProblemNotExists);
            }

            return this.View(problem);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            if (code == null || code.Length < 30)
            {
                return this.Error(Constants.InvalidCodeLength);
            }

            this.submissionsService.Create(this.User, problemId, code);

            return Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.NotLogged);
            }

            this.submissionsService.Delete(id);

            return this.Redirect("/");
        }
    }
}