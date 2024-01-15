using SalesWPFAppLibrary.Dao;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SalesWPFAppLibrary.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public Member GetMemberByID(int memberId) => MemberDAO.Instance.GetMemberByID(memberId);
        public IEnumerable<Member> GetMembers() => MemberDAO.Instance.GetMembersList();
        public void InsertMember(Member member) => MemberDAO.Instance.AddNew(member);
        public void DeleteMember(Member member) => MemberDAO.Instance.Remove(member);
        public void UpdateMember(Member member) => MemberDAO.Instance.Update(member);
        public Boolean Login(string email, string password) => MemberDAO.Instance.Login(email, password);
        public IEnumerable<Member> SearchByEmail(string search) => MemberDAO.Instance.SearchByEmail(search);
    }
}
