using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost("/Users/Login")]
        public HttpResponse DoLogin()
        {
            return this.View();
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost("/Users/Register")]
        public HttpResponse DoRegister()
        {
            var user = new User()
            {
                Username = this.Request.FormData["username"],
                Email = this.Request.FormData["email"],
                Password = this.Request.FormData["password"],
            };
            if (IsValid(user) == false)
            {
                return this.Error(Constants.InvalidUser);
            }
            if (this.Request.FormData["password"] != this.Request.FormData["confirmPassword"])
            {
                return this.Error(Constants.PasswordsNotIdentical);
            }

            var db = new SulsDbContext();
            if (db.Users.Any(u=>u.Username == user.Username || u.Email == user.Email))
            {
                return this.Error(Constants.DublicatedUserEmail);
            }
            //User.Password.Length set to 200 in Db, otherwise throws exception 
            user.Password = Hash(user.Password);

            db.Users.Add(user);
            db.SaveChanges();

            //TO DO Add Login

            return this.Redirect("/");
        }

        private static bool IsValid(object entity)
        {
            var validationContext = new ValidationContext(entity);
            var validationResult = new List<ValidationResult>();

            var result = Validator.TryValidateObject(entity, validationContext, validationResult, true);

            return result;
        }
    }
}