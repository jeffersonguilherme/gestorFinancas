using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Categories;

public class DeleteCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    =>  app.MapDelete("/{id}", HandleAsync)
        .WithName("Category: Delete")
        .WithSummary("Delete uma categoria")
        .WithDescription("Deleta uma categoria")
        .WithOrder(3)
        .Produces<Response<Category>>();
    
        private static async Task<IResult> HandleAsync(
            ClaimsPrincipal user,
            ICategoryHandler handler,
            long id
        )
    {
        var request = new DeleteCategoryRequest
        {
            UserId = user.Identity?.Name ?? string.Empty,
            Id = id
        };

        var result = await handler.DeleteAsync(request);
        if (result.IsSucess)
            return TypedResults.Ok(result);

        return TypedResults.BadRequest(result.Data);   
        }
}