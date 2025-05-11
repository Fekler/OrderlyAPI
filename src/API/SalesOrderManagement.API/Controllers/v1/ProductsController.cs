using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesOrderManagement.Application.Dtos.Entities.Product;
using SalesOrderManagement.Application.Interfaces.Business;

namespace SalesOrderManagement.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProductsController(IProductBusiness productBusiness) : ControllerBase
    {
        private readonly IProductBusiness _productBusiness = productBusiness;

        [HttpPost]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            var response = await _productBusiness.Add(createProductDto);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productBusiness.Get(id);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("{guid:guid}")]
        [Authorize]
        public async Task<IActionResult> GetProductByGuid(Guid guid)
        {
            var response = await _productBusiness.GetDto(guid);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productBusiness.GetAll();
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpGet("category/{category}")]
        [Authorize]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            var response = await _productBusiness.GetAllByCategory(category);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpPut("{guid:guid}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> UpdateProduct(Guid guid, UpdateProductDto updateProductDto)
        {
            if (guid != updateProductDto.UUID)
            {
                return BadRequest();
            }
            var response = await _productBusiness.Update(updateProductDto);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            var response = await _productBusiness.Delete(id);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }

        [HttpDelete("{guid:guid}")]
        [Authorize(Roles = "Admin,Seller")]
        public async Task<IActionResult> DeleteProductByGuid(Guid guid)
        {
            var response = await _productBusiness.Delete(guid);
            return StatusCode((int)response.StatusCode, response.ApiReponse);
        }
    }
}