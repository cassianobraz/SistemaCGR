namespace SistemaControle.Application.Pessoa.Dtos.Responses;

public class PessoaReceitaResponseDto
{
    public required IReadOnlyCollection<PessoaReceitaDto> Result { get; init; }
    public required decimal TotalReceitasGeral { get; init; }
    public required decimal TotalDespesasGeral { get; init; }
    public required decimal SaldoGeral { get; init; }
}