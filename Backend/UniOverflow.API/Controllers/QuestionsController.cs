using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using UniOverflow.API.Data;
using UniOverflow.API.DTOs;
using UniOverflow.API.Models;

namespace UniOverflow.API.Controllers;
/*Source that helped me: https://github.com/TrackableEntities/AspNetCore.ApiControllers.Templates/blob/master/sample/TemplatesSample/Controllers/ProductsController.cs */

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public QuestionsController(AppDbContext appDbContext)
    {
        this._appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuestionResponseDto>>> GetQuestions(string? searchTerm, int pageNumber = 1,int pageSize = 10)
    {
        var query = _appDbContext.Questions.AsQueryable();
        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(e => e.Title.Contains(searchTerm) || e.Content.Contains(searchTerm));
        }
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        var result = await query.Select(q => new QuestionResponseDto
        {
            Id = q.Id,
            Title = q.Title,
            Content = q.Content,
            AuthorName = q.AuthorName,
            CreatedAt = q.CreatedAt
        }).ToListAsync();
        return Ok(result);
    }
    [HttpPost]
    public async Task<ActionResult<QuestionResponseDto>> CreateQuestion([FromBody] QuestionCreateDto question)
    {
        var newQuestion = new Question
        {
            Title = question.Title,
            Content = question.Content,
            AuthorName = question.AuthorName,
        };
        _appDbContext.Questions.Add(newQuestion);
        await _appDbContext.SaveChangesAsync();
        return Ok(new QuestionResponseDto
        {
           Id = newQuestion.Id,
           Title = newQuestion.Title,
           Content = newQuestion.Content,
           AuthorName =newQuestion.AuthorName,
           CreatedAt = newQuestion.CreatedAt
        });
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<QuestionResponseDto>> GetQuestion(Guid id)
    {
        var question = await _appDbContext.Questions.FindAsync(id);
        if (question == null) {
            return NotFound();
        }
        return Ok(new QuestionResponseDto
        {
            Id = question.Id,
            Title = question.Title,
            Content = question.Content,
            AuthorName = question.AuthorName,
            CreatedAt = question.CreatedAt
        });
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateQuestion(Guid id, [FromBody] QuestionCreateDto updatedQuestion)
    {
        var existingQuestion = await _appDbContext.Questions.FindAsync(id);
        if (existingQuestion == null)
        {
            return NotFound();
        }
        existingQuestion.Title = updatedQuestion.Title;
        existingQuestion.Content = updatedQuestion.Content;
        existingQuestion.AuthorName = updatedQuestion.AuthorName;
        try
        {
            await _appDbContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_appDbContext.Questions.Any(q => q.Id == id))
            {
                return NotFound();
            }
            throw;
        }
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteQuestion(Guid id)
    {
        var question = await _appDbContext.Questions.FindAsync(id);
        if(question == null)
        {
            return NotFound();
        }
        _appDbContext.Questions.Remove(question);
        await _appDbContext.SaveChangesAsync();

        return NoContent();
    }
}