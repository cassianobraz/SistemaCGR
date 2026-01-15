using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Enum;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos.Responses;
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
    /// Buscar todas as transações cadastradas no banco.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TransacaoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategorias(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterTransacoesRequest(), ct);

        return Ok(result.Value.Result);
    }

    /// <summary>
    /// Cadastrar uma nova transação no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CriarCategoriaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CriarTranscoesRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
        {
            if (result.ErrorType == TipoErro.NotFound)
                return NotFound(result.Errors);

            return BadRequest(result.Errors);
        }

        return Ok(result.Value);
    }
}
