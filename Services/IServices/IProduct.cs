using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IProduct
    {
        System.Collections.Generic.IEnumerable<Repository.Model.ProductModel> GetAllProducts();
        Repository.Model.ProductModel GetProduct(int Id);
        bool InsertProduct(Repository.Model.EditProductModel product);
        bool UpdateProduct(Repository.Model.ProductModel product);
        bool DeleteProduct(Repository.Model.ProductModel product, bool isLogicalDelete = true);
    }
}
