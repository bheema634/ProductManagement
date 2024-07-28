using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Product_Management_System.Business_Services.IServices;
using Product_Management_System.Data;
using Product_Management_System.Models;

namespace Product_Management_System.Business_Services.Services
{

    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(ProductContext context, ILogger<ProductService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var response = await _context.Products.ToListAsync();
                return _mapper.Map<IEnumerable<Product>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting products.");
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            try
            {
                var response = await _context.Products.FindAsync(id);
                return _mapper.Map<Product>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting the product with ID {id}.");
                return null;
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            try
            {
                var response = await _context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
                return _mapper.Map<IEnumerable<Product>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting products with name {name}.");
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            try
            {
                var resposne = await _context.Products.Where(p => p.Category == category).ToListAsync();
                return _mapper.Map<IEnumerable<Product>>(resposne);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting products in category {category}.");
                return new List<Product>();
            }
        }

        public async Task<int> GetTotalCount()
        {
            try
            {
                return await _context.Products.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the total count of products.");
                return 0;
            }
        }

        public async Task<IEnumerable<Product>> SortProducts(string sortBy, bool ascending)
        {
            try
            {
                IQueryable<ProductDTO> query = _context.Products;

                query = sortBy switch
                {
                    "name" => ascending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
                    "category" => ascending ? query.OrderBy(p => p.Category) : query.OrderByDescending(p => p.Category),
                    "price" => ascending ? query.OrderBy(p => p.Price) : query.OrderByDescending(p => p.Price),
                    _ => query
                };

                var response = await query.ToListAsync();
                return _mapper.Map<IEnumerable<Product>>(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while sorting products by {sortBy}.");
                return new List<Product>();
            }
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                var productDTO = _mapper.Map<ProductDTO>(product);
                _context.Products.Add(productDTO);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new product.");
            }
        }

        public async Task UpdateProduct(int id, Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Description = product.Description;
                    existingProduct.Category = product.Category;
                    existingProduct.Price = product.Price;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating the product with ID {id}.");
            }
        }

        public async Task DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the product with ID {id}.");
            }
        }

        public async Task DeleteAllProducts()
        {
            try
            {
                _context.Products.RemoveRange(_context.Products);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting all products.");
            }
        }
    }
}