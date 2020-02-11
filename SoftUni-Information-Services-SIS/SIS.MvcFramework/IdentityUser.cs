using System.ComponentModel.DataAnnotations;

namespace SulsApp
{
    public class IdentityUser<T>
    {
        protected const string InvalidLength = "Invalid Length";

        public T Id { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = InvalidLength)]
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = InvalidLength)]
        public string Password { get; set; }
    }
}
