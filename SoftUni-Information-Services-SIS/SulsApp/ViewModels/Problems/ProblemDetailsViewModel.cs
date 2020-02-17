using System.Collections.Generic;

namespace SulsApp.ViewModels.Problems
{
    public class ProblemDetailsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<ProblemDetailsSubmissionViewModel> Problems { get; set; }
    }
}