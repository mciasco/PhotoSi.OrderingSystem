using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class GetProductsByCategoryCommandHandler : BaseCommandHandlerWithInputWithOutput<string, IEnumerable<Product>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductsByCategoryCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Product>> Execute(string input)
        {
            return await _productsRepository.GetProductsByCategory(input);
        }
    }
}
