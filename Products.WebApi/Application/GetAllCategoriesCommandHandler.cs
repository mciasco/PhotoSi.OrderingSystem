using Products.Contracts.Domain;
using Products.Contracts.Persistence;
using Commons.WebApi.Application;
using Commons.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class GetAllCategoriesCommandHandler : BaseCommandHandlerNoInputWithOutput<IEnumerable<Category>>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCategoriesCommandHandler(ICategoriesRepository categoriesRepository, IUnitOfWork unitOfWork)
        {
            this._categoriesRepository = categoriesRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<IEnumerable<Category>> Execute()
        {
            return await _categoriesRepository.GetAllCategories();
        }
    }
}
