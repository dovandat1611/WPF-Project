using System;
using System.Collections.Generic;

namespace SalesWPFAppLibrary.DataAccess;

public partial class Order
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? ShippedDate { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? Status { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();
}
