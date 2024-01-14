using AuthServer.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDTO> _serviceGeneric;

        public ProductsController(IServiceGeneric<Product, ProductDTO> serviceGeneric)
        {
            _serviceGeneric = serviceGeneric;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResltInstance(await _serviceGeneric.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDTO productDTO)
        {
            return ActionResltInstance(await _serviceGeneric.AddAsync(productDTO));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDTO productDTO)
        {
            return ActionResltInstance(await _serviceGeneric.Update(productDTO, productDTO.Id));
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(int Id)
        {
            return ActionResltInstance(await _serviceGeneric.Remove(Id));
        }

    }
}
