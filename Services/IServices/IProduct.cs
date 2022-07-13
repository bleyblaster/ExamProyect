using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface IProduct
    {
        System.Collections.Generic.IEnumerable<Domain.Model.Product> GetAllProducts();
        Domain.Model.Product GetProduct(int Id);
        bool InsertProduct(Domain.Model.Product product);
        bool UpdateProduct(Domain.Model.Product product);
        bool DeleteProduct(Domain.Model.Product product, bool isLogicalDelete = true);
    }
}
