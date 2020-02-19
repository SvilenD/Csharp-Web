using IRunes.App.ViewModels.Users;
using IRunes.Services;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using System;
using System.Net.Mail;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Login()
        {
            return this.View();
        }


        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);
            if (userId == null)
            {
                return this.Redirect(Constants.LoginPath);
            }

            this.SignIn(userId);
            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(Constants.LoginPath);
            }

            this.SignOut();
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel model)
        {
            if (model.Username?.Length < 4 || model.Username?.Length > 10)
            {
                return this.Error(Constants.InvalidNameLength);
            }

            if (model.Password?.Length < 6 || model.Password?.Length > 20)
            {
                return this.Error(Constants.InvalidPasswordLength);
            }

            if (!IsValidEmail(model.Email))
            {
                return this.Error(Constants.InvalidEmail);
            }

            if (this.usersService.IsUsernameUsed(model.Username))
            {
                return this.Error(Constants.DublicatedUsername);
            }

            if (this.usersService.IsEmailUsed(model.Email))
            {
                return this.Error(Constants.DublicatedEmail);
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error(Constants.PasswordsNotIdentical);
            }

            this.usersService.CreateUser(model.Username, model.Email, model.Password);

            var userId = this.usersService.GetUserId(model.Username, model.Password);
            this.SignIn(userId);

            return this.Redirect("/");
        }

        private bool IsValidEmail(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
