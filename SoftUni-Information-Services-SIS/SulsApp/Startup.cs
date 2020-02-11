using SIS.HTTP;
using SIS.HTTP.Enums;
using SIS.MvcFramework;
using SulsApp.Controllers;
using System.Collections.Generic;

namespace SulsApp
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices()
        {
            using var db = new SulsDbContext();
            db.Database.EnsureCreated();
        }

        public void Configure(IList<Route> routeTable)
        {
        }
    }
}
