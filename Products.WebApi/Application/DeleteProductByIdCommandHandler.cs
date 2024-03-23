﻿using Products.Contracts.Persistence;

namespace Products.WebApi.Application
{
    public class DeleteProductByIdCommandHandler : BaseCommandHandlerWithInputWithOutput<string, bool>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductByIdCommandHandler(IProductsRepository productsRepository, IUnitOfWork unitOfWork)
        {
            this._productsRepository = productsRepository;
            this._unitOfWork = unitOfWork;
        }

        public override async Task<bool> Execute(string input)
        {
            var productToDelete = await _productsRepository.GetProductById(input);
            if (productToDelete is null)
                throw new ArgumentOutOfRangeException($"Nessun prodotto trovato con id '{input}'");

            await _productsRepository.DeleteProduct(productToDelete);
            return await _unitOfWork.SaveChangesAsync() == 1;
        }
    }
}