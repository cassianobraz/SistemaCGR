using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Application.Pessoa.Dtos.Responses;
using SistemaControle.Domain.Services.Interfaces;

namespace SistemaControle.Application.Pessoa.Handlers;

public class CriarPessoaHandler : IRequestHandler<CriarPessoaRequestDto, ResultViewModel<CriarPessoaResponseDto>>
{
    private readonly IPessoaService _pessoaService;

    public CriarPessoaHandler(IPessoaService pessoaService)
        => _pessoaService = pessoaService;

    public async Task<ResultViewModel<CriarPessoaResponseDto>> Handle(CriarPessoaRequestDto request, CancellationToken ct)
    {
        var result = await _pessoaService.CriarAsync(request.Nome, request.Idade, ct);

        if (result.IsFailure)
            return ResultViewModel<CriarPessoaResponseDto>.Failure(result.Errors, result.ErrorType!.Value);

        return ResultViewModel<CriarPessoaResponseDto>.Success(new CriarPessoaResponseDto(result.Value));
    }
}