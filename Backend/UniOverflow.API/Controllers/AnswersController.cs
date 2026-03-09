using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Validation;
using System.Threading.Tasks;
using UniOverflow.API.Data;
using UniOverflow.API.DTOs;
using UniOverflow.API.Models;

namespace UniOverflow.API.Controllers
{
    [ApiController]
    [Route("questions/{questionId}/answers")]
    public class AnswersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public AnswersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerResponseDto>>> GetAnswers(Guid questionId)
        {
            var answers = await _appDbContext.Answers.Where(a => a.QuestionId == questionId).Select(a => new AnswerResponseDto
            {
                Id = a.Id,
                Content = a.Content,
                AuthorName = a.AuthorName,
                CreatedAt = a.CreatedAt,
                QuestionId = a.QuestionId
            }).ToListAsync();
            
            return Ok(answers);
        }
        [HttpPost]
        public async Task<ActionResult<AnswerResponseDto>> CreateAnswer(Guid questionId, [FromBody] AnswerCreateDto answer)
        {
            var newAnswer = new Answer
            {
                Content = answer.Content,
                AuthorName = answer.AuthorName,
            };
            var questionExists = await _appDbContext.Questions.AnyAsync(q => q.Id == questionId);
            if (!questionExists)
            {
                return NotFound("Question doesnt exist");
            }
            newAnswer.QuestionId = questionId;

            _appDbContext.Answers.Add(newAnswer);
            await _appDbContext.SaveChangesAsync();

            return Ok(new AnswerResponseDto
            {
                Id = newAnswer.Id,
                Content = newAnswer.Content,
                AuthorName = newAnswer.AuthorName,
                CreatedAt = newAnswer.CreatedAt,
                QuestionId = newAnswer.QuestionId
            });
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(Guid questionId, Guid id, [FromBody] AnswerCreateDto updatedAnswer)
        {
            var existingAnswer = await _appDbContext.Answers.FirstOrDefaultAsync(a => a.Id == id && a.QuestionId == questionId);
            if (existingAnswer == null)
            {
                return NotFound("Answer not found or he not belong to this question.");
            }
            existingAnswer.Content = updatedAnswer.Content;
            existingAnswer.AuthorName = updatedAnswer.AuthorName;

            await _appDbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAnswer(Guid questionId, Guid id)
        {
        
            var answer = await _appDbContext.Answers.FirstOrDefaultAsync(a => a.Id == id && a.QuestionId == questionId);

            if (answer == null)
            {
                return NotFound();
            }

            _appDbContext.Answers.Remove(answer);
            await _appDbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
