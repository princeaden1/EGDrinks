using EGDrinksCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ProductRepositoryTests
    {
        private AppDbContext _context;
        private ProductRepository _repository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new AppDbContext(options);
            _repository = new ProductRepository(_context);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProduct()
        {
            var product = new Product { Name = "Test Drink", Price = 10.5M, Category= "Non Alcoholic", Description="Less Sugar" };
            await _repository.AddProductAsync(product);
            var products = await _repository.GetAllProductsAsync();

            Assert.Single(products);
        }
    }
}
