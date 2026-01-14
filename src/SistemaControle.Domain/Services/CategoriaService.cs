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

    public async Task<ResultViewModel<bool>> CriarAsync(string descricao, Finalidade finalidade, CancellationToken ct)
    {
        if (descricao is null)
        {
            var erros = new List<Error> { new(DescricaoCategoriaNull, ObterMensagem(DescricaoCategoriaNull)) };
            return ResultViewModel<bool>.Failure(erros, TipoErro.Domain);
        }

        if (!Enum.IsDefined(typeof(Finalidade), finalidade))
        {
            var erros = new List<Error> { new(FinalidadeCategoriaInvalida, ObterMensagem(FinalidadeCategoriaInvalida)) };
            return ResultViewModel<bool>.Failure(erros, TipoErro.Domain);
        }

        var CriarCategoria = Categoria.Create(descricao, finalidade);

        await _categoriaRepository.CriarAsync(CriarCategoria, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return ResultViewModel<bool>.Success(true);
    }
}