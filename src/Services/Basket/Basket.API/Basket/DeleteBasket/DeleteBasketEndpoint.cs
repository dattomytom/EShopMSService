namespace Basket.API.Basket.DeleteBasket
{
    //public record DeleteBasketRequest(string UserName)
    public record DeleteBasketResponse(bool isSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Basket/{userName}", (string userName, ISender sender) =>
            {
                var result = sender.Send(new DeleteBasketCommand(userName));
                var reponse = result.Adapt < DeleteBasketResponse>();
                return Results.Ok(reponse);
            })
            .WithName("DeleteBasket")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Basket")
            .WithDescription("Delete Basket"); ;
        }
    }
}
