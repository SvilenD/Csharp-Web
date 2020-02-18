using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Models
{
    public class Problem
    {
        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
        }

        public string Id { get; set; }

        [StringLength(20, MinimumLength = 5, ErrorMessage = Constants.InvalidLength)]
        [Required]
        public string Name { get; set; }

        [Range(5, 300)]
        public int Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
