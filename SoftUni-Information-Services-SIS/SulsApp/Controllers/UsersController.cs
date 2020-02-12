using SIS.HTTP.Logging;
using SIS.HTTP.Response;
using SIS.MvcFramework;
using SIS.MvcFramework.Attributes;
using SulsApp.Services;
using SulsApp.ViewModels;
using System;
using System.Net.Mail;

namespace SulsApp.Controllers
{
    public class UsersController : Controller
    {
        private const string loginPath = "/Users/Login";
        private readonly IUsersService usersService;
        private readonly ILogger logger;

        public UsersController(IUsersService usersService, ILogger logger)
        {
            this.usersService = usersService;
            this.logger = logger;
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
                return this.Redirect(loginPath);
            }

            this.SignIn(userId);
            this.logger.Log("User logged in: " + username);
            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect(loginPath);
            }

            this.SignOut();
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                return this.Error(Constants.PasswordsNotIdentical);
            }

            if (input.Username?.Length < 5 || input.Username?.Length > 20)
            {
                return this.Error(Constants.InvalidUsernameLength);
            }

            if (input.Password?.Length < 6 || input.Password?.Length > 20)
            {
                return this.Error(Constants.InvalidPasswordLength);
            }

            if (!IsValidEmail(input.Email))
            {
                return this.Error(Constants.InvalidEmail);
            }

            if (this.usersService.IsUsernameUsed(input.Username))
            {
                return this.Error(Constants.DublicatedUsername);
            }

            if (this.usersService.IsEmailUsed(input.Email))
            {
                return this.Error(Constants.DublicatedEmail);
            }

            this.usersService.CreateUser(input.Username, input.Email, input.Password);
            this.logger.Log("New user: " + input.Username);

            var userId = this.usersService.GetUserId(input.Username, input.Password);
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