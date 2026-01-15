using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Pessoa.Dtos;
using SistemaControle.Application.Pessoa.Dtos.Requests;
using SistemaControle.Application.Pessoa.Dtos.Responses;
using SistemaControle.Domain.Models.PessoaAggregate;

namespace SistemaControle.Application.Pessoa.Handlers;

public class ObterPessoasHandler : IRequestHandler<ObterPessoasRequestDto, ResultViewModel<PessoaResponseDto>>
{
    private readonly IPessoaRepository _pessoaRepository;
    public ObterPessoasHandler(IPessoaRepository pessoaRepository)
        => _pessoaRepository = pessoaRepository;

    public async Task<ResultViewModel<PessoaResponseDto>> Handle(ObterPessoasRequestDto request, CancellationToken ct)
    {
        var pessoa = await _pessoaRepository.ListarTodosAsync(ct);

        var response = new PessoaResponseDto
        {
            Result = pessoa.Select(c => new PessoaDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Idade = c.Idade
            }).ToList()
        };

        return ResultViewModel<PessoaResponseDto>.Success(response);
    }
}
