using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(Product product);
        void UpdateProduct(Product product);
        IEnumerable<Product> SearchByName(string search);
        IEnumerable<Product> SortByPrice(string search);

    }
}
