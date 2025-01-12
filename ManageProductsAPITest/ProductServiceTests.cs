using ManageProductsAPI.Data;
using ManageProductsAPI.Models;
using ManageProductsAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ManageProductsAPITest
{
    public class ProductServiceTests
    {
        private ProductServices _productService;
        private ProductsDbContext _dbContext;

        public ProductServiceTests()
        {
            // Setup In-Memory Database
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                            .UseInMemoryDatabase(databaseName: "ProductTestDb")
                            .Options;

            // Create the in-memory context
            _dbContext = new ProductsDbContext(options);
            _productService = new ProductServices(_dbContext);
        }

        [Fact]
        public async Task AddProduct_ShouldAddProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            // Act
            var addedProduct = await _productService.CreateProductAsync(product);

            // Assert
            Assert.NotNull(addedProduct);
            Assert.Equal("Product1", addedProduct.Name);
            Assert.Equal(100.00m, addedProduct.Price);
            Assert.Equal(50, addedProduct.StockAvailable);
        }

        [Fact]
        public async Task GetProduct_ShouldReturnProductById()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            await _productService.CreateProductAsync(product); // Ensure it's in the DB

            // Act
            var retrievedProduct = await _productService.GetProductByIdAsync(product.Id);

            // Assert
            Assert.NotNull(retrievedProduct);
            Assert.Equal(product.Name, retrievedProduct.Name);
            Assert.Equal(product.Price, retrievedProduct.Price);
            Assert.Equal(product.StockAvailable, retrievedProduct.StockAvailable);
        }

        [Fact]
        public async Task UpdateProduct_ShouldUpdateProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            await _productService.CreateProductAsync(product); // Ensure it's in the DB

            var updatedProduct = new Product
            {
                Name = "Updated Product",
                Price = 150.00m,
                StockAvailable = 30,
                Description = "Updated description"
            };

            // Act
            var result = await _productService.UpdateProductAsync(product.Id, updatedProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedProduct.Name, result.Name);
            Assert.Equal(updatedProduct.Price, result.Price);
            Assert.Equal(updatedProduct.StockAvailable, result.StockAvailable);
        }

        [Fact]
        public async Task DeleteProduct_ShouldDeleteProductSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            await _productService.CreateProductAsync(product); // Ensure it's in the DB

            // Act
            var result = await _productService.DeleteProductAsync(product.Id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DecrementStock_ShouldDecrementStockSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            await _productService.CreateProductAsync(product); // Ensure it's in the DB

            // Act
            var result = await _productService.DecrementStockAsync(product.Id, 10);

            // Assert
            Assert.True(result);
            var updatedProduct = await _productService.GetProductByIdAsync(product.Id);
            Assert.Equal(40, updatedProduct.StockAvailable); // After decrement, stock should be 40
        }

        [Fact]
        public async Task AddToStock_ShouldAddStockSuccessfully()
        {
            // Arrange
            var product = new Product
            {
                Name = "Product1",
                Price = 100.00m,
                StockAvailable = 50,
                Description = "Sample product"
            };

            await _productService.CreateProductAsync(product); // Ensure it's in the DB

            // Act
            var result = await _productService.AddToStockAsync(product.Id, 20);

            // Assert
            Assert.True(result);
            var updatedProduct = await _productService.GetProductByIdAsync(product.Id);
            Assert.Equal(70, updatedProduct.StockAvailable); // After adding stock, it should be 70
        }
    }


}
