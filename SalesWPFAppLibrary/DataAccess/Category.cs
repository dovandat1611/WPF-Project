using System;
using System.Collections.Generic;

namespace SalesWPFAppLibrary.DataAccess;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
