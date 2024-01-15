using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member GetMemberByID(int memberId);
        void InsertMember(Member member);
        void DeleteMember(Member member);
        void UpdateMember(Member member);
        Boolean Login(string email, string password);
        IEnumerable<Member> SearchByEmail(string search);
    }
}
