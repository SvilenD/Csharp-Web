using System;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Models
{
    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(800, MinimumLength = 30, ErrorMessage = Constants.InvalidLength)] //not necessary for db
        public string Code { get; set; }

        [Range(0, 300)] //not necessary for db
        public int AchievedResult { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProblemId { get; set; }

        public virtual Problem Problem { get; set; } //virtual to enable using Lazy Loading (in case)

        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
