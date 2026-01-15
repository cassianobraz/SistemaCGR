using Result.Domain.Enum;
using Result.Domain.Models;
using SistemaControle.Domain.Models.CategoriaAggregate;
using SistemaControle.Domain.Services.Interfaces;
using SistemaControle.Domain.Shared;
using SistemaControle.Domain.Shared.Enums;
using static SistemaControle.Domain.Shared.CatalogoDeErros;

namespace SistemaControle.Domain.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CategoriaService(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultViewModel<Guid>> CriarAsync(string descricao, Finalidade finalidade, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(descricao))
        {
            var erros = new List<Error> { new(DescricaoCategoriaNull, ObterMensagem(DescricaoCategoriaNull)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        if (!Enum.IsDefined(typeof(Finalidade), finalidade))
        {
            var erros = new List<Error> { new(FinalidadeCategoriaInvalida, ObterMensagem(FinalidadeCategoriaInvalida)) };
            return ResultViewModel<Guid>.Failure(erros, TipoErro.Domain);
        }

        var categoria = Categoria.Create(descricao.Trim(), finalidade);

        await _categoriaRepository.CriarAsync(categoria, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ResultViewModel<Guid>.Success(categoria.Id);
    }
}