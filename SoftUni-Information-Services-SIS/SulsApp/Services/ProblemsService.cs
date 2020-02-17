using SulsApp.Models;
using SulsApp.ViewModels.Problems;
using System.Linq;

namespace SulsApp.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly SulsDbContext db;

        public ProblemsService(SulsDbContext db)
        {
            this.db = db;
        }

        public void CreateProblem(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points,
            };
            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }

        public ProblemDetailsViewModel GetProblemDetails(string id)
        {
            return this.db.Problems.Where(x => x.Id == id)
                .Select(x => new ProblemDetailsViewModel
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
        }
    }
}
