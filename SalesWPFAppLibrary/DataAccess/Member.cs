using System;
using System.Collections.Generic;

namespace SalesWPFAppLibrary.DataAccess;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? Address { get; set; }

    public bool? Gender { get; set; }

    public string Password { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public static class SessionDataMember
    {
        public static List<Member> members { get; set; } = new List<Member>();
    }

}
