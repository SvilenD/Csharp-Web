﻿using SulsApp.ViewModels.Submissions;

namespace SulsApp.Services
{
    public interface ISubmissionsService
    {
        void Create(string userId, string problemId, string code);

        void Delete(string id);

        CreateFormViewModel GetProblemView(string problemId);
    }
}