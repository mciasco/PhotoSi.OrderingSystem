using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using Commons.WebApi.Application;

namespace Products.WebApi.Application
{
    public class CreateNewProductCommandHandler : BaseCommandHandlerWithInputWithOutput<CreateNewProductCommandInput, Product>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateNewProductCommandHandler(
            IProductsRepository productsRepository, 
            ICategoriesRepository categoriesRepository,
            IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._categoriesRepository = categoriesRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<Product> Execute(CreateNewProductCommandInput input)
        {
            var category = await _categoriesRepository.GetCategoryByName(input.CategoryName);
            if (category is null)
                throw new ArgumentException($"Cannot find any category with name '{input.CategoryName}'");

            var product = Product.Create(
                input.Name,
                input.Description,
                category,
                input.QtyStock,
                input.Price);

            await _productsRepository.AddProduct(product);

            await _unitOfWork.SaveChangesAsync();

            return product;
        }
    }

    public class CreateNewProductCommandInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CategoryName { get; set; }
        public int QtyStock { get; set; }
        public decimal Price { get; set; }
    }
}
