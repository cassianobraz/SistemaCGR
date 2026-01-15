using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Pessoa.Handlers;

public class DeletarPessoaHandler : IRequestHandler<DeletarPessoaRequestDto, ResultViewModel<bool>>
{
    private readonly IPessoaService _pessoaService;

    public DeletarPessoaHandler(IPessoaService pessoaService)
        => _pessoaService = pessoaService;

    public async Task<ResultViewModel<bool>> Handle(DeletarPessoaRequestDto request, CancellationToken ct)
    {
        var result = _pessoaService.ExcluirAsync(request.Id, ct);

        if (result.Result.IsFailure)
            return ResultViewModel<bool>.Failure(result.Result.Errors, result.Result.ErrorType!.Value);

        return ResultViewModel<bool>.Success(result.Result.Value);
    }
}