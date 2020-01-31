using SIS.HTTP;
using SIS.HTTP.Response;
using SIS.MvcFramework;

namespace SulsApp.Controllers
{
    class HomeController : Controller
    {
        public HttpResponse Index(HttpRequest request)
        {
            return this.View(); //return View("Index");
        }
    }
}
