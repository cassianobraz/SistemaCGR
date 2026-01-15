using MediatR;
using Result.Domain.Models;

namespace SistemaControle.Application.Pessoa.Dtos.Requests;

public class DeletarPessoaRequestDto : IRequest<ResultViewModel<bool>>
{
    public Guid Id { get; set; }
}