using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Pessoa.Handlers;

public class CriarPessoaHandler : IRequestHandler<CriarPessoaRequestDto, ResultViewModel<bool>>
{
    private readonly IPessoaService _pessoaService;

    public CriarPessoaHandler(IPessoaService pessoaService)
        => _pessoaService = pessoaService;

    public async Task<ResultViewModel<bool>> Handle(CriarPessoaRequestDto request, CancellationToken ct)
    {
        var result = await _pessoaService.CriarAsync(request.Nome, request.Idade, ct);

        if (result.IsFailure)
            return ResultViewModel<bool>.Failure(result.Errors, result.ErrorType!.Value);

        return ResultViewModel<bool>.Success(true);
    }
}
