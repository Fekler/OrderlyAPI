using Mapster;
using Microsoft.Extensions.Logging;
using SalesOrderManagement.Application.Dtos.Entities.Product;
using SalesOrderManagement.Application.Interfaces.Business;
using SalesOrderManagement.Domain.Entities;
using SalesOrderManagement.Domain.Entities._bases;
using SalesOrderManagement.Domain.Errors;
using SalesOrderManagement.Domain.Interfaces.Repositories;
using SharedKernel.Utils;
using System.Net;

namespace SalesOrderManagement.Application.Business
{
    public class ProductBusiness(IProductRepository productRepository, ILogger<ProductBusiness> logger) : IProductBusiness
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ILogger<ProductBusiness> _logger = logger;

        public async Task<Response<Guid>> Add(CreateProductDto createProductDto)
        {
            try
            {
                var product = createProductDto.Adapt<Product>();
                await _productRepository.Add(product);
                _logger.LogInformation($"Produto criado com UUID: {product.UUID}");
                return new Response<Guid>().Sucess(product.UUID, message: Const.CREATE_SUCCESS, statusCode: HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar novo produto.");
                return new Response<Guid>().Failure(default, message: "Erro ao adicionar novo produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(int id)
        {
            try
            {
                var deleted = await _productRepository.Delete(id);
                if (deleted)
                {
                    _logger.LogInformation($"Produto com ID: {id} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar produto com ID: {id}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar produto.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar produto com ID: {id}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Delete(Guid guid)
        {
            try
            {
                var productToDelete = await _productRepository.Get(guid);
                if (productToDelete == null)
                {
                    return new Response<bool>().Failure(false, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                var result = await _productRepository.Delete(productToDelete.UUID);
                if (result)
                {
                    _logger.LogInformation($"Produto com UUID: {guid} deletado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.DELETE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao deletar produto com UUID: {guid}.");
                    return new Response<bool>().Failure(false, message: "Falha ao deletar produto.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar deletar produto com UUID: {guid}.");
                return new Response<bool>().Failure(false, message: "Erro ao deletar produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<ProductDto>> Get(int id)
        {
            try
            {
                var product = await _productRepository.Get(id);
                if (product == null)
                {
                    return new Response<ProductDto>().Failure(default, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                var productDto = product.Adapt<ProductDto>();
                return new Response<ProductDto>().Sucess(productDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter produto com ID: {id}.");
                return new Response<ProductDto>().Failure(default, message: "Erro ao obter produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productRepository.GetAll();
            var productDtos = products.Adapt<IEnumerable<ProductDto>>();
            return new Response<IEnumerable<ProductDto>>().Sucess(productDtos, statusCode: HttpStatusCode.OK);

        }

        public async Task<Response<IEnumerable<ProductDto>>> GetAllByCategory(string category)
        {
            var products = await _productRepository.GetAllByCategory(category);
            var productDtos = products.Adapt<IEnumerable<ProductDto>>();
            return new Response<IEnumerable<ProductDto>>().Sucess(productDtos, statusCode: HttpStatusCode.OK);
        }

        public async Task<Response<ProductDto>> GetDto(Guid guid)
        {
            try
            {
                var product = await _productRepository.Get(guid);
                if (product == null)
                {
                    return new Response<ProductDto>().Failure(default, message: "Produto não encontrado.", statusCode: HttpStatusCode.NotFound);
                }
                var productDto = product.Adapt<ProductDto>();
                return new Response<ProductDto>().Sucess(productDto, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter produto com UUID: {guid} como DTO.");
                return new Response<ProductDto>().Failure(default, message: "Erro ao obter produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<Product>> GetEntity(Guid guid)
        {
            try
            {
                var product = await _productRepository.Get(guid);
                if (product == null)
                {
                    return new Response<Product>().Failure(default, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }
                return new Response<Product>().Sucess(product, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar obter produto com UUID: {guid} como entidade.");
                return new Response<Product>().Failure(default, message: "Erro ao obter produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Response<bool>> Update(UpdateProductDto updateProductDto)
        {
            try
            {
                var existingProduct = await _productRepository.Get(updateProductDto.UUID);
                if (existingProduct == null)
                {
                    return new Response<bool>().Failure(false, message: Error.PRODUCT_NOT_FOUND, statusCode: HttpStatusCode.NotFound);
                }

                updateProductDto.Adapt(existingProduct);
                existingProduct.UpdateAt = DateTime.UtcNow;
                var result = await _productRepository.Update(existingProduct);
                if (result)
                {
                    _logger.LogInformation($"Produto com UUID: {updateProductDto.UUID} atualizado com sucesso.");
                    return new Response<bool>().Sucess(true, message: Const.UPDATE_SUCCESS, statusCode: HttpStatusCode.OK);
                }
                else
                {
                    _logger.LogWarning($"Falha ao atualizar produto com UUID: {updateProductDto.UUID}.");
                    return new Response<bool>().Failure(false, message: "Falha ao atualizar produto.", statusCode: HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao tentar atualizar produto com UUID: {updateProductDto.UUID}.");
                return new Response<bool>().Failure(false, message: "Erro ao atualizar produto.", statusCode: HttpStatusCode.InternalServerError);
            }
        }
    }
}