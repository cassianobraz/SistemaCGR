using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacaoController : ControllerBase
{
    private readonly IMediator _mediator;
    public TransacaoController(IMediator mediator)
        => _mediator = mediator;

    [HttpGet]
    public IActionResult GetTransacao()
    {
        var categorias = new[]
        {
            new { Id = 1, Nome = "Eletrônicos" },
            new { Id = 2, Nome = "Roupas" },
            new { Id = 3, Nome = "Alimentos" }
        };
        return Ok(categorias);
    }
}
