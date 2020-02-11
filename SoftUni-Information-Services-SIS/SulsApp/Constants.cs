namespace SulsApp
{
    public static class Constants
    {
        public const string ConnectionString = @"Server=.;Database=SulsApp;Integrated Security=True";

        public const string PasswordsNotIdentical = "Passwords are not matching!";

        public const string InvalidUser = "Invalid User data.";

        public const string InvalidLength = "Invalid Length";

        internal static string DublicatedUserEmail = "User with same Username/Email is already registered.";
    }
}