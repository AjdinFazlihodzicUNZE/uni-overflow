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
    public async Task<ActionResult<Question>> CreateQuestion([FromBody] Question question)
    {
        _appDbContext.Questions.Add(question);
        await _appDbContext.SaveChangesAsync();
        return Ok(question);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Question>> GetQuestion(Guid id)
    {
        var question = await _appDbContext.Questions.FindAsync(id);
        if (question == null) {
            return NotFound();
        }
        return Ok(question);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateQuestion(Guid id, [FromBody] Question question)
    {
        if (id != question.Id)
        {
            return BadRequest();
        }
        _appDbContext.Entry(question).State = EntityState.Modified;
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
        return Ok(question);
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