using Domain.Model;
using Repository.Repository;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ProductService: IProduct
    {
        private IRepository<Product> _ProductRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _ProductRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ProductRepository.GetAll();
        }

        public Product GetProduct(int Id)
        {
            return _ProductRepository.Get(Id);
        }

        public bool InsertProduct(Product product)
        {
            product.IsActive = true;
            product.ModifiedDate = DateTime.Now;
            _ProductRepository.Insert(product);
            return _ProductRepository.SaveChanges();
        }

        public bool UpdateProduct(Product product)
        {
            Product productObject = _ProductRepository.Get(product.Id);
            if (productObject != null)
            {
                productObject.ExpirationDate = product.ExpirationDate;
                productObject.ProductName = product.ProductName;
                productObject.ModifiedDate = DateTime.Now;
                productObject.ProductCode = product.ProductCode;
                _ProductRepository.Update(productObject);
            }

            return _ProductRepository.SaveChanges();
        }

        public bool DeleteProduct(Product product, bool isLogicalDelete = true)
        {
            if (isLogicalDelete)
            {
                Product productObject = _ProductRepository.Get(product.Id);
                if (productObject != null)
                {
                    productObject.IsActive = false;
                    productObject.ModifiedDate = DateTime.Now;
                    _ProductRepository.Update(productObject);
                }
            }
            else
            {
                _ProductRepository.Delete(product);
            }

            return _ProductRepository.SaveChanges();
        }
    }
}
