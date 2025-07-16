using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class GetTransactionByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
        .WithName("Transaction: By Id")
        .WithSummary("Recupera uma transação")
        .WithDescription("Recupera uma transação com base no ID")
        .WithOrder(4)
        .Produces<Response<Transaction?>>();
    }

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        long id
    )
    {
        var request = new GetTransactionByIdRequest
        {
            UserId = "teste@jeff.io",
            Id = id
        };

        var result = await handler.GetByIdAsync(request);
        if (result.IsSucess)
            return TypedResults.Created($"/{result.Data?.Id}", result);

        return TypedResults.BadRequest(result.Data);
    }
}