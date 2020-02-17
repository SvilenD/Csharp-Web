using SulsApp.Models;
using SulsApp.ViewModels.Submissions;
using System;
using System.Linq;

namespace SulsApp.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly SulsDbContext db;
        private readonly Random random;

        public SubmissionsService(SulsDbContext db, Random random)
        {
            this.db = db;
            this.random = random;
        }

        public void Create(string userId, string problemId, string code)
        {
            var problem = this.db.Problems
                .FirstOrDefault(x => x.Id == problemId);

            var submission = new Submission()
            {
                CreatedOn = DateTime.Now,
                UserId = userId,
                ProblemId = problemId,
                Code = code,
                AchievedResult = random.Next(0, problem.Points + 1),
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var submission = this.db.Submissions.Find(id);
            this.db.Remove(submission);
            this.db.SaveChanges();
        }

        public CreateFormViewModel GetProblemView(string problemId)
        {
            return db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => new CreateFormViewModel
                {
                    Name = x.Name,
                    ProblemId = x.Id,
                }).FirstOrDefault();
        }
    }
}