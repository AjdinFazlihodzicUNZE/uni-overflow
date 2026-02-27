namespace UniOverflow.API.Models
{
    public class Answer
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsApproved { get; set; } = false;

        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
