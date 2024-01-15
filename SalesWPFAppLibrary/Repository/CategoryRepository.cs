using SalesWPFAppLibrary.Dao;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Category GetCategoryByID(int productId) => CategoryDAO.Instance.GetByID(productId);
        public IEnumerable<Category> GetCategories() => CategoryDAO.Instance.GetList();
        public void InsertCategory(Category product) => CategoryDAO.Instance.AddNew(product);
        public void DeleteCategory(Category product) => CategoryDAO.Instance.Remove(product);
        public void UpdateCategory(Category product) => CategoryDAO.Instance.Update(product);
        public IEnumerable<Category> SearchByName(string search) => CategoryDAO.Instance.SearchByName(search);

    }
}
