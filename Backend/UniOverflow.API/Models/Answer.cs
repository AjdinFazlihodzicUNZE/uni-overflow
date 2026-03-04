using System.ComponentModel.DataAnnotations;
namespace UniOverflow.API.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        [MaxLength(2000)]
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string AuthorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        [Required]
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
