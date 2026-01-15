using Result.Domain.Enum;
using Result.Domain.Models;
using SistemaControle.Domain.Models.TransacoesAggregate;
using SistemaControle.Domain.Services.Interfaces;
using SistemaControle.Domain.Shared;
using SistemaControle.Domain.Shared.Enums;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.Domain.Services;

public class TransacoesService : ITransacoesService
{
    private readonly ITransacoesRepository _transacoesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TransacoesService(ITransacoesRepository transacoesRepository, IUnitOfWork unitOfWork)
    {
        _transacoesRepository = transacoesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> CriarAsync(string descricao,
                                                        decimal valor,
                                                        TipoTransacao tipo,
                                                        Guid categoriaId,
                                                        Guid pessoaId,
                                                        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            var erros = new List<Error> { new(DescricaoTransacaoNull, ObterMensagem(DescricaoTransacaoNull)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        if (!Enum.IsDefined(typeof(TipoTransacao), tipo))
        {
            var erros = new List<Error> { new(TipoTransacaoInvalida, ObterMensagem(TipoTransacaoInvalida)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        if (valor <= 0)
        {
            var erros = new List<Error> { new(ValorTransacaoInvalida, ObterMensagem(ValorTransacaoInvalida)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        var categoria = await _transacoesRepository.ObterCategoriaPorIdAsync(categoriaId, ct);
        if (categoria is null)
        {
            var erros = new List<Error> { new(CategoriaTransacaoNull, ObterMensagem(CategoriaTransacaoNull)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.NotFound);
        }

        var pessoa = await _transacoesRepository.ObterPessoaPorIdAsync(pessoaId, ct);
        if (pessoa is null)
        {
            var erros = new List<Error> { new(PessoaTransacaoNull, ObterMensagem(PessoaTransacaoNull)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.NotFound);
        }

        if (pessoa.Idade < 18 && tipo != TipoTransacao.Despesa)
        {
            var erros = new List<Error> { new(PessoaMenorIdadeSomenteDespesa, ObterMensagem(PessoaMenorIdadeSomenteDespesa)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        if (!CategoriaAceitaTipo(categoria.Finalidade, tipo))
        {
            var erros = new List<Error> { new(CategoriaFinalidadeInvalidaParaTipoTransacao, ObterMensagem(CategoriaFinalidadeInvalidaParaTipoTransacao)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        var transacao = Transacao.Create(descricao, valor, tipo, categoriaId, pessoaId);

        await _transacoesRepository.CriarAsync(transacao, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ResultViewModel<Guid>.Success(transacao.Id);
    }

    private static bool CategoriaAceitaTipo(Finalidade finalidade, TipoTransacao tipo)
        => finalidade switch
        {
            Finalidade.Ambas => true,
            Finalidade.Despesa => tipo == TipoTransacao.Despesa,
            Finalidade.Receita => tipo == TipoTransacao.Receita,
            _ => false
        };
}