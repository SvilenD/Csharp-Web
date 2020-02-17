using SulsApp.ViewModels.Home;
using System.Collections.Generic;

namespace SulsApp.Controllers
{
    public class LoggedInViewModel
    {
        public string Username { get; set; }

        public List<IndexProblemViewModel> Problems { get; set; }
    }
}