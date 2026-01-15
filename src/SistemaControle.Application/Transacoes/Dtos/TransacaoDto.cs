using SistemaControle.Domain.Shared.Enums;

namespace SistemaControle.Application.Transacoes.Dtos;

public class TransacaoDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public TipoTransacao Tipo { get; set; }
    public Guid CategoriaId { get; set; }
    public Guid PessoaId { get; set; }
}