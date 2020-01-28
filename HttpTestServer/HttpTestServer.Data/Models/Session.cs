namespace HttpTestServer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Session
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        public DateTime FirstLogin { get; set; }

        [Required]
        public DateTime LastLogin { get; set; }

        public bool IsExpired { get; set; }
    }
}
