using Microsoft.AspNetCore.Mvc;
using UniOverflow.API.Data;

namespace UniOverflow.API.Controllers;

[ApiController]
[Route("[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public QuestionsController(AppDbContext appDbContext) { 
        this._appDbContext = appDbContext; 
    }

}