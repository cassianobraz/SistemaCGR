using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Application.Pessoa.Dtos.Responses;

namespace SistemaControle.Application.Pessoa.Handlers;

public class ObterPessoasHandler : IRequestHandler<ObterPessoasRequestDto, ResultViewModel<PessoaResponseDto>>
{
    public Task<ResultViewModel<PessoaResponseDto>> Handle(ObterPessoasRequestDto request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
