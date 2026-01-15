using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Application.Pessoa.Dtos.Responses;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IMediator _mediator;
    public PessoaController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Buscar todas as pessoas cadastradas no banco.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<PessoaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPessoas(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterPessoasRequestDto(), ct);

        return Ok(result.Value.Result);
    }

    /// <summary>
    /// Buscar o total de receitas, despesas e saldo.
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet("totais-pessoas")]
    [ProducesResponseType(typeof(ConsultaTotaisPorPessoaDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReceita(CancellationToken ct)
    {
        var result = await _mediator.Send(new ObterReceitaRequestDto(), ct);

        return Ok(result.Value);
    }

    /// <summary>
    /// Cadastrar uma pessoa no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CriarPessoaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarPessoaRequestDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    /// <summary>
    /// Apagar uma pessoa pelo Id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletaId(Guid id, CancellationToken ct)
    {
        var request = new DeletarPessoaRequestDto { Id = id };

        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return NotFound(result.Errors);

        return NoContent();
    }
}