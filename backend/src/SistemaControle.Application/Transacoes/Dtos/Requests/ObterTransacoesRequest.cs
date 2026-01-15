using MediatR;
using Result.Domain.Models;
using SistemaControle.Application.Transacoes.Dtos.Responses;

namespace SistemaControle.Application.Transacoes.Dtos.Requests;

public class ObterTransacoesRequest : IRequest<ResultViewModel<TransacaoResponseDto>>
{
}
