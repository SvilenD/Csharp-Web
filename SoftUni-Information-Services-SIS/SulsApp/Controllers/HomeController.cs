using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.ViewModels;

namespace SulsApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var model = new IndexViewModel
            {
                Message = "Welcome to SULS Platform",
                Year = 2020
            };

            return this.View(model); //return View("Index");
        }
    }
}
