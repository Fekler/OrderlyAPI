using SalesOrderManagement.Application.Dtos.Entities.Product;
using SalesOrderManagement.Application.Interfaces.Business._bases;
using SalesOrderManagement.Domain.Entities;
using SharedKernel.Utils;

namespace SalesOrderManagement.Application.Interfaces.Business
{
    public interface IProductBusiness : IBusinessBase<Product, CreateProductDto, UpdateProductDto, ProductDto>
    {
        Task<Response<IEnumerable<ProductDto>>> GetAll();
        Task<Response<IEnumerable<ProductDto>>> GetAllByCategory(string category);
    }
}
