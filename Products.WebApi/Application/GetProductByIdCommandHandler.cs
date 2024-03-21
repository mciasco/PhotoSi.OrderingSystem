using Products.Contracts.Domain;
using Products.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class GetProductByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByIdCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Product> Execute(string input)
        {
            return await _productsRepository.GetProductById(input);
        }
    }
}
