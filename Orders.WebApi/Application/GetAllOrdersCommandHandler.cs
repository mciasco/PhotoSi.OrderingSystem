using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Orders.WebApi.Application
{
    public class GetAllOrdersCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            this._ordersRepository = ordersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Order>> Execute()
        {
            return await _ordersRepository.GetAllOrders();
        }
    }
}
