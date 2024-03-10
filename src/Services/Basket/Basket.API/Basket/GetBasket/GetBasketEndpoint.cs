namespace Basket.API.Basket.GetBasket
{
    //public record GetBasketRequest(string userName)
    public record GetBaseketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));
                var response = result.Adapt<GetBaseketResponse>();
                return Results.Ok(response);
            })
            .WithName("GetBaskets")
            .Produces<GetBaseketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Baskets")
            .WithDescription("Get Baskets");
        }
    }
}
