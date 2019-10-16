using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("/Get")]
        public List<ProductModel> Get()
        {
            var products = (from p in _dbContext.Products select p).ToList();
            return products;
        }
        [HttpGet]
        [Route("/Get/{id:Guid}")]
        public ActionResult<ProductModel> Get(Guid id)
        {
            var product = (from p in _dbContext.Products where p.Id == id select p).FirstOrDefault();
            return product;
        }
        [HttpPost]
        [Route("/Post")]
        public Guid Post(ProductCreateInputModel model)
        {
            if (ModelState.IsValid)
            {
                var newProduct = new ProductModel { Name = model.Name, Price = model.Price };
                _dbContext.Products.Add(newProduct);
                _dbContext.SaveChanges();
                return newProduct.Id;
            }
            else
            {
                return new Guid(Guid.Empty.ToString());
            }
        }
        [HttpPut]
        [Route("/Put")]
        public void Put(ProductUpdateInputModel model)
        {
            if (ModelState.IsValid)
            {
                var product = _dbContext.Products.SingleOrDefault(p => p.Id == model.Id);
                if(product != null)
                {
                    product.Name = model.Name;
                    product.Price = model.Price;
                    _dbContext.Update(product);
                    _dbContext.SaveChanges();
                }
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public void Delete(Guid id)
        {
            var product = _dbContext.Products.FirstOrDefault(p => p.Id == id);
            if(product != null)
            {
                _dbContext.Remove(product);
                _dbContext.SaveChanges();
            }
        }
    }
}