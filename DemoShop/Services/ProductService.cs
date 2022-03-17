using DemoShop.Models;
using DemoShop.Models.DTOs;
using DemoShop.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoShop.Services
{
    public class ProductService
    {
        private ShopDbContext dbContext;

        public ProductService(ShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<ProductDTO> GetAll()
        {
            return dbContext.Products
                .Include(p => p.User)   
                .Select(p => ToDto(p))
                .ToList();
        }

        public void Create(Product product, User user)
        {
            product.User = user;

            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public Product GetById(int id)
        {
            return dbContext.Products.FirstOrDefault(x => x.Id == id);
        }

        public void Edit(Product product)
        {
            Product dbProduct = GetById(product.Id);

            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.Stock = product.Stock;

            dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Product dbProduct = GetById(id);
            dbContext.Products.Remove(dbProduct);
            dbContext.SaveChanges();
        }

        private static ProductDTO ToDto(Product p)
        {
            ProductDTO product = new ProductDTO();

            product.Id = p.Id;
            product.Name = p.Name;
            product.Price = p.Price;
            product.Stock = p.Stock;
            product.CreatedBy = $"{p.User.FirstName} {p.User.LastName}";
            product.UserEmail = p.User.Email;

            return product;
        }
    }
}
