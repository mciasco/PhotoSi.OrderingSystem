using Orders.Contracts.Domain;
using Orders.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Orders.WebApi.Application
{
    public class GetAllOrdersByAccountCommandHandler : BaseCommandHandlerWithInputWithOutput<string, IEnumerable<Order>>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllOrdersByAccountCommandHandler(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            this._ordersRepository = ordersRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Order>> Execute(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("Cannot find order by null id");

            return await _ordersRepository.GetAllOrdersByAccountId(input);
        }
    }
}
