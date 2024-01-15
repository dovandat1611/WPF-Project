using System;
using System.Collections.Generic;

namespace SalesWPFAppLibrary.DataAccess;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? Status { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
