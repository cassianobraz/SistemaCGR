using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;

namespace SistemaControle.Application.Pessoa.Handlers;

public class DeletarPessoaHandler : IRequestHandler<DeletarPessoaRequestDto, ResultViewModel<bool>>
{
    public Task<ResultViewModel<bool>> Handle(DeletarPessoaRequestDto request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
