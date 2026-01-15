using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Responses;

namespace SistemaControle.Application.Pessoa.Dtos.Requests;

public class ObterPessoasRequestDto : IRequest<ResultViewModel<PessoaResponseDto>>
{
}
