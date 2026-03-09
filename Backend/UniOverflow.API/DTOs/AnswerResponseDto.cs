namespace UniOverflow.API.DTOs
{
    public class AnswerResponseDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public Guid QuestionId { get; set; }
    }
}
