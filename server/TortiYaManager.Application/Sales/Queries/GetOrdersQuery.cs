using SharedLib.CQRS;
using TortiYaManager.Application.Sales.DTOs;
using TortiYaManager.Core.Sales;

namespace TortiYaManager.Application.Sales.Queries;

public class GetOrdersQuery
{
    public record QueryArgs();
    public record QueryResult(IEnumerable<OrderDto> Orders);

    public class Handler : AppRequestHandler<QueryArgs, QueryResult>
    {
        protected override Task<QueryResult> ExecuteAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new QueryResult([
                new OrderDto
                {
                    Id = "1",
                    Date = DateTime.UtcNow.ToString(Constants.DATE_TIME_FORMAT),
                    PaymentMethod = "Efectivo",
                    Items = [
                        new OrderItemDto { Id = "1", Name = "Product 1", Quantity = 2, Charge = 10.0m, Cost = 5.0m },
                        new OrderItemDto { Id = "2", Name = "Product 2", Quantity = 1, Charge = 20.0m, Cost = 8.0m },
                    ]
                },
                new OrderDto
                {
                    Id = "2",
                    Date = DateTime.UtcNow.AddDays(-1).ToString(Constants.DATE_TIME_FORMAT),
                    PaymentMethod = null,
                    Items = [
                        new OrderItemDto { Id = "3", Name = "Product 3", Quantity = 3, Charge = null, Cost = 15.0m },
                    ]
                },
                new OrderDto
                {
                    Id = "3",
                    Date = DateTime.UtcNow.AddDays(-2).ToString(Constants.DATE_TIME_FORMAT),
                    PaymentMethod = "Efectivo",
                    Items = [
                        new OrderItemDto { Id = "4", Name = "Product 1", Quantity = 2, Charge = 10.0m, Cost = 5.0m },
                        new OrderItemDto { Id = "4", Name = "Product 2", Quantity = 1, Charge = null, Cost = 8.0m }
                    ]
                },
            ]));
        }

        protected override Task<IEnumerable<(string field, string error)>> ValidateAsync(QueryArgs args, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(Enumerable.Empty<(string field, string error)>());
        }
    }
}