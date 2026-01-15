using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Categoria.Dtos;
using SistemaControle.Application.Categoria.Dtos.Requests;
using SistemaControle.Domain.Models.CategoriaAggregate;

namespace SistemaControle.Application.Categoria.Handlers;

public class ObterTotaisPorCategoriaHandler: IRequestHandler<ObterTotaisPorCategoriaRequestDto, ResultViewModel<TotaisPorCategoriaResponseDto>>
{
    private readonly ICategoriaRepository _categoriaRepository;

    public ObterTotaisPorCategoriaHandler(ICategoriaRepository categoriaRepository)
        => _categoriaRepository = categoriaRepository;

    public async Task<ResultViewModel<TotaisPorCategoriaResponseDto>> Handle(
        ObterTotaisPorCategoriaRequestDto request,
        CancellationToken ct)
    {
        var domain = await _categoriaRepository.ConsultarTotaisPorCategoriaAsync(ct);

        var categorias = domain.Categorias
            .Select(c => new CategoriaTotaisDto(
                c.CategoriaId,
                c.Descricao,
                c.TotalReceitas,
                c.TotalDespesas,
                c.Saldo
            ))
            .ToList();

        var response = new TotaisPorCategoriaResponseDto(
            categorias,
            domain.TotalReceitasGeral,
            domain.TotalDespesasGeral,
            domain.SaldoGeral
        );

        return ResultViewModel<TotaisPorCategoriaResponseDto>.Success(response);
    }
}
