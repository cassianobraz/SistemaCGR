using Result.Domain.Enum;
using Result.Domain.Models;
using SistemaControle.Domain.Models.PessoaAggregate;
using SistemaControle.Domain.Services.Interfaces;
using SistemaControle.Domain.Shared;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.Domain.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;
    public PessoaService(IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
    {
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> CriarAsync(string nome, int idade, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            var erros = new List<Error> { new(NomePessoaInvalido, ObterMensagem(NomePessoaInvalido)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        if (idade <= 0 || idade > 120)
        {
            var erros = new List<Error> { new(IdadePessoaInvalida, ObterMensagem(IdadePessoaInvalida)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        var pessoa = Pessoa.Create(nome, idade);

        await _pessoaRepository.CriarAsync(pessoa, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ResultViewModel<Guid>.Success(pessoa.Id);
    }


    public async Task<ResultViewModel<bool>> ExcluirAsync(Guid id, CancellationToken ct)
    {
        var pessoa = await _pessoaRepository.ObterPorIdAsync(id, ct);

        if (pessoa is null)
        {
            var erros = new List<Error> { new(PessoaNaoEncontrada, ObterMensagem(PessoaNaoEncontrada)) };
            return ResultViewModel<bool>.Failure(erros, TipoErro.Domain);
        }

        await _pessoaRepository.ExcluirAsync(pessoa, ct);
        await _unitOfWork.SaveChangesAsync(ct);
        return ResultViewModel<bool>.Success(true);
    }
}