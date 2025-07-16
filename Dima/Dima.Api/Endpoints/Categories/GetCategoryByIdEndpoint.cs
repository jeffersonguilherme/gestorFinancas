using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;


namespace Dima.Api.Endpoints.Categories;

public class GetCategoryByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", HandleAsync)
        .WithName("Category: By Id")
        .WithSummary("Recupera uma categoria")
        .WithDescription("Recupera uma categoria com base no ID")
        .WithOrder(4)
        .Produces<Response<Category?>>();
    }

    private static async Task<IResult> HandleAsync(
        ICategoryHandler handler,
        long id
    )
    {
        var request = new GetCategoryByIdRequest
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