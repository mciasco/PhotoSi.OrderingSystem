using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;

namespace Orders.WebApi.Application
{
    public class GetOrderByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, Order>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetOrderByIdCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            this._ordersRepository = ordersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Order> Execute(string input)
        {
            return await _ordersRepository.GetOrderById(input);
        }
    }
}
