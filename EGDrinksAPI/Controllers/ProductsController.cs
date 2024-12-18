﻿using EGDrinksCore.Entities;
using EGDrinksCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EGDrinksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger; // create the logger

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Fetching all products"); // Log the action

            var products = await _productRepository.GetAllProductsAsync();

            if (products == null || !products.Any())
            {
                _logger.LogWarning("No products found");
            }

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (product == null)
            {
                _logger.LogError("Product is empty");
                return BadRequest("Product cannot be empty");
            }

            // Input sanitization for product name
            var sanitizedProductName = product.Name.Trim();
            var sanitizedProductDesc = product.Description.Trim();
            var sanitizedProductCat = product.Category.Trim();

            if (string.IsNullOrWhiteSpace(sanitizedProductName) || string.IsNullOrWhiteSpace(sanitizedProductDesc) || string.IsNullOrWhiteSpace(sanitizedProductCat))
            {
                return BadRequest("Product name cannot be empty or just whitespace.");
            }

            product.Name = sanitizedProductName;
            product.Description = sanitizedProductDesc;
            product.Category = sanitizedProductCat;

            _logger.LogInformation($"Adding a new product: {product.Name}, Price: {product.Price}");
            await _productRepository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Attempting to delete product with ID {id}");

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning($"Product with ID {id} not found");
                return NotFound($"Product with ID {id} not found");
            }

            await _productRepository.RemoveProductAsync(id);
            _logger.LogInformation($"Product with ID {id} successfully deleted");

            return NoContent();
        }
    }
}
