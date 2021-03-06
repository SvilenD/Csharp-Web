﻿using SulsApp.Models;

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
    }
}
