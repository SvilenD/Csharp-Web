using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Services;
using SulsApp.ViewModels.Problems;
using System.Linq;

namespace SulsApp.Controllers
{
    public class ProblemsController  : Controller
    {
        private readonly IProblemsService problemsService;
        private readonly SulsDbContext db;

        public ProblemsController(IProblemsService problemsService, SulsDbContext db)
        {
            this.problemsService = problemsService;
            this.db = db;
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

            var viewModel = this.db.Problems.Where(x => x.Id == id)
                .Select(x => new DetailsViewModel
                {
                    Name = x.Name,
                    Problems = x.Submissions.Select(s =>
                    new ProblemDetailsSubmissionViewModel
                    {
                        CreatedOn = s.CreatedOn,
                        AchievedResult = s.AchievedResult,
                        SubmissionId = s.Id,
                        MaxPoints = x.Points,
                        Username = s.User.Username,
                    })
                }).FirstOrDefault();

            return this.View(viewModel);
        }
    }
}
