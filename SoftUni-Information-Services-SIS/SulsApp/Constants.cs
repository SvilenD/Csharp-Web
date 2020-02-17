namespace SulsApp
{
    public static class Constants
    {
        public const string ConnectionString = @"Server=.;Database=SulsApp;Integrated Security=True";

        public const string PasswordsNotIdentical = "Passwords are not matching!";

        public const string InvalidLength = "Invalid Length";

        public const string InvalidNameLength = "Name should be between 5 and 20 characters.";

        public const string InvalidPasswordLength = "Password should be between 6 and 20 characters.";

        public const string DublicatedEmail = "User with this Email is already registered!";

        public const string DublicatedUsername = "User with this Username is already registered!";

        public const string InvalidEmail = "Invalid Email Address!";

        public const string NotLogged = "You are Not Logged In.";

        public const string LoginPath = "/Users/Login";

        public const string ProblemPointsRange = "Points range: [1-100]";
    }
}