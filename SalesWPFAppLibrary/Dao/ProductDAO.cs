using Microsoft.EntityFrameworkCore;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Dao
{
    public class ProductDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        //-------------------------------------
        public IEnumerable<Product> GetProductsList()
        {
            List<Product> products;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                products = salesWpfAppDB.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        //-------------------------------------
        public Product GetProductByID(int productID)
        {
            Product product = null;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                product = salesWpfAppDB.Products.SingleOrDefault(product => product.ProductId == productID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        //-------------------------------------
        public void AddNew(Product product)
        {
            try
            {
                Product _product = GetProductByID(product.ProductId);
                if (_product == null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Products.Add(product);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Update(Product product)
        {
            try
            {
                Product p = GetProductByID(product.ProductId);
                if (p != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Entry<Product>(product).State = EntityState.Modified;
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Remove(Product product)
        {
            try
            {
                Product _product = GetProductByID(product.ProductId);
                if (_product != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Products.Remove(product);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //----------------------------------------
        public IEnumerable<Product> SearchByName(string search)
        {
            List<Product> products;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                products = salesWpfAppDB.Products.Where(x => x.ProductName.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        //----------------------------------------
        public IEnumerable<Product> SortByPrice(string sort)
        {
            List<Product> products;
            try
            {
                using (var salesWpfAppDB = new SalesWpfappContext())
                {
                    var productList = salesWpfAppDB.Products;

                    if (productList != null)
                    {
                        if (sort.Equals("Ascending", StringComparison.OrdinalIgnoreCase))
                        {
                            products = productList.OrderBy(x => x.UnitPrice).ToList();
                        }
                        else if (sort.Equals("Descending", StringComparison.OrdinalIgnoreCase))
                        {
                            products = productList.OrderByDescending(x => x.UnitPrice).ToList();
                        }
                        else
                        {
                            products = productList.ToList();
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Product list is null.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while sorting products.", ex);
            }
            return products;
        }

    }
}
