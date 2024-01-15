using SalesWPFAppLibrary.Dao;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Product GetProductByID(int productId) => ProductDAO.Instance.GetProductByID(productId);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductsList();
        public void InsertProduct(Product product) => ProductDAO.Instance.AddNew(product);
        public void DeleteProduct(Product product) => ProductDAO.Instance.Remove(product);
        public void UpdateProduct(Product product) => ProductDAO.Instance.Update(product);
        public IEnumerable<Product> SearchByName(string search) => ProductDAO.Instance.SearchByName(search);

        public IEnumerable<Product> SortByPrice(string search) => ProductDAO.Instance.SortByPrice(search);
    }
}
