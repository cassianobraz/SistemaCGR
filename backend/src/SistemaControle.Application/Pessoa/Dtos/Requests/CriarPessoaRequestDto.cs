using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Responses;

namespace SistemaControle.Application.Pessoa.Dtos.Requests;

public class CriarPessoaRequestDto : IRequest<ResultViewModel<CriarPessoaResponseDto>>
{
    public string? Nome { get; set; }
    public int Idade { get; set; }
}
