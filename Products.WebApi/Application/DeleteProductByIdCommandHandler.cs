using Products.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class DeleteProductByIdCommandHandler : BaseCommandHandlerWithInputNoOutput<string>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductByIdCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task Execute(string input)
        {
            await _productsRepository.DeleteProduct(input);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
