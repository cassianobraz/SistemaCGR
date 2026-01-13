using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos;
using SistemaControle.Application.Transacoes.Dtos.Requests;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransacaoController : ControllerBase
{
    private readonly IMediator _mediator;
    public TransacaoController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Cadastrar uma nova transação no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarTranscoesRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }

    /// <summary>
    /// Buscar todas as transações cadastradas no banco.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TransacaoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategorias(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterTransacoesRequest(), ct);

        var categorias = result.Value?.Result ?? new List<TransacaoDto>();

        return Ok(categorias);
    }
}
