using ManageProductsAPI.Data;
using ManageProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageProductsAPI.Services
{
    public class ProductServices : IProductService
    {
        private readonly ProductsDbContext _context;
        public ProductServices(ProductsDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new NullReferenceException("Product id can't be null");

                product.StockAvailable += quantity;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while adding stock to the product in the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding stock.", ex);
            }

        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                if (product == null)
                    throw new NullReferenceException("Product cannot be null.");

                // Generate a unique product Id using a simple increment approach.
                product.Id = new Random().Next(100000, 999999); // Ensure unique ID for simplicity.

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return product;
            }
            catch (DbUpdateException ex)
            {
                // Handle any issues related to database update
                throw new Exception("An error occurred while saving the product to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other unforeseen exceptions
                throw new Exception("An unexpected error occurred while creating the product.", ex);
            }
        }

        public async Task<bool> DecrementStockAsync(int id, int quantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new NullReferenceException("Product id can't be null");

                if (product.StockAvailable < quantity)
                    throw new ArgumentException("StockAvailable should not be less then quantity ");

                product.StockAvailable -= quantity;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the stock in the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while decrementing the stock.", ex);
            }

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new NullReferenceException("Product id can't be null");

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while deleting the product.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the product.", ex);
            }

        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            try
            {
                return await _context.Products.ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while fetching all products from the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving the products.", ex);
            }

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                    throw new NullReferenceException("Product id can't be null");

                return product;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while querying the database for the product.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving the product.", ex);
            }

        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(id);
                if (existingProduct == null)
                    throw new NullReferenceException("Product id can't be null");

                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.StockAvailable = product.StockAvailable;
                existingProduct.Description = product.Description;

                await _context.SaveChangesAsync();
                return existingProduct;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while updating the product.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the product.", ex);
            }

        }
    }
}
