using MediatR;
using Result.Domain.Models;

namespace SistemaControle.Application.Pessoa.Dtos.Requests;

public class CriarPessoaRequestDto : IRequest<ResultViewModel<bool>>
{
    public string Nome { get; set; }
    public int Idade { get; set; }
}
