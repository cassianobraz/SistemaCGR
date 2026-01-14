using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Models;
using SistemaControle.Application.Categoria;
using SistemaControle.Application.Categoria.Dtos.Requests;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoriaController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Cadastrar uma categoria no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarCategoriaRequestDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }

    /// <summary>
    /// Buscar todas as categorias cadastradas no banco.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategorias(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterCategoriasRequestDto(), ct);

        return Ok(result.Value.Result);
    }
}