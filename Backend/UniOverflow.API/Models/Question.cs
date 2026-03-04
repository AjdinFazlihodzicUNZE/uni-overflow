
using System.ComponentModel.DataAnnotations;
using System;
namespace UniOverflow.API.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        [MaxLength(2000)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [MinLength(20)]
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}