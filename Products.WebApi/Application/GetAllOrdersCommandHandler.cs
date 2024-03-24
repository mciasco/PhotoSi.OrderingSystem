using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class GetAllProductsCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Product>> Execute()
        {
            return await _productsRepository.GetAllProducts();
        }
    }
}
