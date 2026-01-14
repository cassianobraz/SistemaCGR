using MediatR;
using Result.Domain.Models;
using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Application.Transacoes.Dtos.Requests;

public class CriarTranscoesRequest : IRequest<ResultViewModel<bool>>
{
    public string  Descricao { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid PessoaId { get; set; }
}