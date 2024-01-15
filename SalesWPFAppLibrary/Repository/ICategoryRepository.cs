using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories();
        Category GetCategoryByID(int productId);
        void InsertCategory(Category product);
        //void DeleteCategory(Category product);
        void UpdateCategory(Category product);
        IEnumerable<Category> SearchByName(string search);
    }
}
