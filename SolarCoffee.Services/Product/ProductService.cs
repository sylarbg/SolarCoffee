using SolarCoffee.Data;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SolarCoffee.Services
{
    public class ProductService : IProductService
    {
        private readonly SolarDbContext db;

        public ProductService(SolarDbContext db)
        {
            this.db = db;
        }
        public ServiceResponse<Data.Models.Product> ArchiveProduct(int id)
        {
            try
            {
                var product = this.db.Products.Find(id);
                product.IsArchived = true;

                this.db.Products.Update(product);
                this.db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>()
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Product was updated successfully",
                    IsSuccess = true,
                };

            } catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>()
                {
                    Data = null,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false,
                };
            }                     
        }

        public ServiceResponse<Data.Models.Product> CreateProduct(Data.Models.Product product)
        {
           try
            {
                this.db.Products.Add(product);

                var newInventory = new ProductInventory()
                {
                    Product = product,
                    QuantityOnHand = 0,
                    IdealQuantity = 10,
                };

                this.db.ProductInventories.Add(newInventory);

                this.db.SaveChanges();

                return new ServiceResponse<Data.Models.Product>() {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = "Saved new product",
                    IsSuccess = true
                };
            } catch (Exception e)
            {
                return new ServiceResponse<Data.Models.Product>()
                {
                    Data = product,
                    Time = DateTime.UtcNow,
                    Message = e.StackTrace,
                    IsSuccess = false
                };
            }
        }

        public List<Data.Models.Product> GetAllProducts()
        {
            return this.db.Products.ToList();
        }

        public Data.Models.Product GetProductById(int id)
        {
            return this.db.Products.Find(id);
        }
    }
}
