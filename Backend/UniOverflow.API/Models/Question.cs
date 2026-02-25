using System;
namespace UniOverflow.API.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content {  get; set; }
        public string AuthorName { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}