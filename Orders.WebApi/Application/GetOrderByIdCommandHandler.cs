using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;
using Commons.WebApi.Application;

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
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Cannot find order by null id");

            return await _ordersRepository.GetOrderById(input);
        }
    }
}
