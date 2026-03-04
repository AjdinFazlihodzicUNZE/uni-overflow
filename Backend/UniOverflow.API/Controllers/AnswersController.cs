using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UniOverflow.API.Data;
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
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers(Guid questionId)
        {
            var answers = await _appDbContext.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();
            return Ok(answers);
        }
        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<Answer>> CreateAnswer(Guid questionId, [FromBody] Answer answer)
        {
            var questionExists = await _appDbContext.Questions.AnyAsync(q => q.Id == questionId);
            if (!questionExists)
            {
                return NotFound("Question doesnt exist");
            }
            answer.QuestionId = questionId;

            _appDbContext.Answers.Add(answer);
            await _appDbContext.SaveChangesAsync();

            return Ok(answer);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAnswer(Guid questionId, Guid id, [FromBody] Answer updatedAnswer)
        {
            if (id != updatedAnswer.Id)
            {
                return BadRequest("Answer ID mismatch");
            }
            if (questionId != updatedAnswer.QuestionId)
            {
                return BadRequest("Question ID mismatch");
            }

            _appDbContext.Entry(updatedAnswer).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();

            return Ok(updatedAnswer);
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
