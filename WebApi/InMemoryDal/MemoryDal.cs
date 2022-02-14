using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.InMemoryDal
{
    public class MemoryDal
    {
        public static List<Product> ProductList = new List<Product> {
                new Product { Id=1, CategoryId=2, ProductName="Laptop", PublishingDate=DateTime.Now },
                new Product { Id=2, CategoryId=3, ProductName="Tablet", PublishingDate=DateTime.Now },
                new Product { Id=3, CategoryId=4, ProductName="Smart Watch", PublishingDate=DateTime.Now }
            };
        public static List<Category> CategoryList = new List<Category> {
                new Category { Id = 1, CategoryName = "Mobile", Description = "" },
                 new Category { Id = 2, CategoryName = "Desktop", Description = "" },
                new Category { Id = 3, CategoryName = "Remote", Description = "" }
            };

        public static List<ProductDetail> ProductDetailList = new List<ProductDetail> {
                
            };
    }
}
               