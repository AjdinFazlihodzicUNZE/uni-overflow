using System.ComponentModel.DataAnnotations;

namespace UniOverflow.API.DTOs
{
    public class AnswerCreateDto
    {
        [MaxLength(2000)]
        [Required]
        public string Content { get; set; } = string.Empty;

        [Required]
        public string AuthorName { get; set; } = string.Empty;

        [Required]
        public Guid QuestionId { get; set; }
    }
}
