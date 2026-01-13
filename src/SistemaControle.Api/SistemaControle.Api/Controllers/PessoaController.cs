using MediatR;
using Microsoft.AspNetCore.Mvc;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos;
using SistemaControle.Application.Pessoa.Dtos.Requests;

namespace SistemaControle.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PessoaController : ControllerBase
{
    private readonly IMediator _mediator;
    public PessoaController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Cadastrar uma pessoa no banco.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CriarPessoaRequestDto request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return BadRequest(result.Errors);

        return NoContent();
    }

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

        var categorias = result.Value?.Result ?? new List<PessoaDto>();

        return Ok(categorias);
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
    public async Task<IActionResult> DeleteId(Guid id, CancellationToken ct)
    {
        var request = new DeletarPessoaRequestDto { Id = id };

        var result = await _mediator.Send(request, ct);

        if (result.IsFailure)
            return NotFound(result.Errors);

        return NoContent();
    }
}