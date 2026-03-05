using System.ComponentModel.DataAnnotations;

namespace UniOverflow.API.DTOs
{
    public class QuestionCreateDto
    {
        [MaxLength(2000)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [MinLength(20)]
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string AuthorName { get; set; } = string.Empty;
    }
}
