namespace SistemaControle.Application.Transacoes.Dtos.Responses;

public class TransacaoResponseDto
{
    public required IEnumerable<TransacaoDto> Result { get; set; }
}
