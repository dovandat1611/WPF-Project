using Microsoft.EntityFrameworkCore;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Dao
{
    public class CategoryDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static CategoryDAO instance = null;
        private static readonly object instanceLock = new object();
        private CategoryDAO() { }
        public static CategoryDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CategoryDAO();
                    }
                    return instance;
                }
            }
        }

        //-------------------------------------
        public IEnumerable<Category> SearchByName(string search)
        {
            List<Category> categories;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                categories = salesWpfAppDB.Categories.Where(categories => categories.CategoryName.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }


        //-------------------------------------
        public IEnumerable<Category> GetList()
        {
            List<Category> categories;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                categories = salesWpfAppDB.Categories.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return categories;
        }

        //-------------------------------------
        public Category GetByID(int id)
        {
            Category category = null;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                category = salesWpfAppDB.Categories.FirstOrDefault(x => x.CategoryId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return category;
        }

        //-------------------------------------
        public void AddNew(Category category)
        {
            try
            {
                Category c = GetByID(category.CategoryId);
                if (c == null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Categories.Add(c);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The category is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Update(Category category)
        {
            try
            {
                Category c = GetByID(category.CategoryId);
                if (c != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Entry<Category>(c).State = EntityState.Modified;
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The category does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Remove(Category category)
        {
            try
            {
                Category c = GetByID(category.CategoryId);
                if (c != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Categories.Remove(c);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The category does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
