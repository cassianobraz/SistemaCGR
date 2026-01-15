using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Application.Categoria.Dtos.Responses;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IMediator _mediator;
    public CategoriaController(IMediator mediator)
        => _mediator = mediator;

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

    /// <summary>
    /// Buscar o total de receitas, despesas e saldo.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("totais-categorias")]
    [ProducesResponseType(typeof(List<TotaisPorCategoriaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReceita(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterTotaisPorCategoriaRequestDto(), ct);

        return Ok(result.Value);
    }

    /// <summary>
    /// Cadastrar uma categoria no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CriarCategoriaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarCategoriaRequestDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}