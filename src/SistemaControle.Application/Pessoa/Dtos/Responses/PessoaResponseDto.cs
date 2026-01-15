namespace SistemaControle.Application.Pessoa.Dtos.Responses;

public class PessoaResponseDto
{
    public required IEnumerable<PessoaDto> Result { get; set; }
}