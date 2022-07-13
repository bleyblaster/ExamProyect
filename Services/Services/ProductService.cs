using AutoMapper;
using Domain.Model;
using Repository.Model;
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
        private  IMapper _mapper;
        public ProductService(IRepository<Product> productRepository, IMapper mapper)
        {
            _ProductRepository = productRepository;
            _mapper = mapper;
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            var result = _ProductRepository.GetAll();
            var productModel = _mapper.Map<List<ProductModel>>(result);
            return productModel;
        }

        public ProductModel GetProduct(int Id)
        {
            var result = _ProductRepository.Get(Id);
            var productModel = _mapper.Map<ProductModel>(result);
            return productModel;
        }

        public bool InsertProduct(EditProductModel product)
        {
            var productEntity = _mapper.Map<Product>(product);

            productEntity.IsActive = true;
            productEntity.ModifiedDate = DateTime.Now;
            _ProductRepository.Insert(productEntity);
            return _ProductRepository.SaveChanges();
        }

        public bool UpdateProduct(ProductModel product)
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

            return productObject != null && _ProductRepository.SaveChanges();
        }

        public bool DeleteProduct(ProductModel product, bool isLogicalDelete = true)
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
                var productEntity = _mapper.Map<Product>(product);
                _ProductRepository.Delete(productEntity);
            }

            return _ProductRepository.SaveChanges();
        }
    }
}
