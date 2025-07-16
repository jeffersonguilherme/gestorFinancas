
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Transactions;

public class CreateTransactionEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    =>
        app.MapPost("/", HandleAsync)
        .WithName("Transactions: Create")
        .WithSummary("Cria uma transação")
        .WithDescription("Criar uma nova transação")
        .WithOrder(1)
        .Produces<Response<Transaction?>>();
    

    private static async Task<IResult> HandleAsync(
        ITransactionHandler handler,
        CreateTransactionRequest request)
    {
        request.UserId = "teste@jeff.io";
        var result = await handler.CreateAsync(request);
        if (result.IsSucess)
            return TypedResults.Created($"/{result.Data?.Id}", result);

        return TypedResults.BadRequest(result.Data);
    }
}