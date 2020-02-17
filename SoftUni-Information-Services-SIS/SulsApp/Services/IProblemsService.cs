using SulsApp.ViewModels.Problems;

namespace SulsApp.Services
{
    public interface IProblemsService
    {
        void CreateProblem(string name, int points);

        ProblemDetailsViewModel GetProblemDetails(string problemId);
    }
}