using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;

namespace SistemaControle.Application.Pessoa.Handlers;

public class CriarPessoaHandler : IRequestHandler<CriarPessoaRequestDto, ResultViewModel<bool>>
{
    public Task<ResultViewModel<bool>> Handle(CriarPessoaRequestDto request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
