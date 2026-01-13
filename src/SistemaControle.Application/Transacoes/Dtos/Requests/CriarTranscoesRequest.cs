using MediatR;
using Result.Domain.Models;

namespace SistemaControle.Application.Transacoes.Dtos.Requests;

public class CriarTranscoesRequest : IRequest<ResultViewModel<bool>>
{
    public string  Descricao { get; set; }
    public decimal Valor { get; set; }
    public string Tipo { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid PessoaId { get; set; }
}