using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product_Management_System.Business_Services.IServices;
using Product_Management_System.Data;
using Product_Management_System.Models;

namespace Product_Management_System.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger, IMapper mapper)
        {
            _productService = productService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                if (product != null)
                {

                }
                await _productService.AddProduct(product);
                return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a product.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving products.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                return OKorNotFount(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the product with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetProductByName([FromQuery] string name)
        {
            try
            {
                var products = await _productService.GetProductsByName(name);
                return OKorNotFount(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the product with Name {name}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("total-count")]
        public async Task<IActionResult> GetTotalCount()
        {
            try
            {
                var count = await _productService.GetTotalCount();
                return OKorNotFount(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the product count.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetProductsByCategory(string category)
        {
            try
            {
                var products = await _productService.GetProductsByCategory(category);
                
                return OKorNotFount(products);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"An error occurred while retrieving the product from product Category.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sort")]
        public async Task<IActionResult> SortProducts([FromQuery] string sortBy, [FromQuery] bool ascending)
        {
            try
            {
                var products = await _productService.SortProducts(sortBy, ascending);
                return OKorNotFount(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the products with sort {sortBy} and order by {ascending}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest();
                }

                await _productService.UpdateProduct(id, product);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the product with id {id} .");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the product with id {id} .");
                return StatusCode(500, "Internal server error");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllProducts()
        {
            try
            {
                await _productService.DeleteAllProducts();
                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, $"An error occurred while deleting all products.");
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
