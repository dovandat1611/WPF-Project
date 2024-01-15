using Microsoft.EntityFrameworkCore;
using SalesWPFAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SalesWPFAppLibrary.DataAccess.Member;

namespace SalesWPFAppLibrary.Dao
{
    public class MemberDAO
    {
        //-------------------------------------
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        //-------------------------------------
        public Boolean Login(string email, string password)
        {
            Boolean result = false;
            Member member = null;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                member = salesWpfAppDB.Members.SingleOrDefault(member => member.Email.Equals(email) && member.Password.Equals(password));
                if(member != null)
                {
                    SessionDataMember.members.Add(member);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        //-------------------------------------
        public IEnumerable<Member> SearchByEmail(string search)
        {
            List<Member> members;
            try
            {
                var salesWpfAppDB = new SalesWpfappContext();
                members = salesWpfAppDB.Members.Where(members => members.Email.Contains(search)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }


        //-------------------------------------
        public IEnumerable<Member> GetMembersList()
        {
            List<Member> members;
            try
            {
                SalesWpfappContext salesWpfAppDB = new SalesWpfappContext();
                members = salesWpfAppDB.Members.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        //-------------------------------------
        public Member GetMemberByID(int memberID)
        {
            Member member = null;
            try
            {
                SalesWpfappContext salesWpfAppDB = new SalesWpfappContext();
                member = salesWpfAppDB.Members.SingleOrDefault(member => member.MemberId == memberID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        //-------------------------------------
        public void AddNew(Member member)
        {
            try
            {
                SalesWpfappContext salesWpfAppDB = new SalesWpfappContext();

                // Check if the member already exists
                Member _member = GetMemberByID(member.MemberId);

                if (_member == null)
                {
                    // If member does not exist, add it to the context
                    salesWpfAppDB.Members.Add(member);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    // If member already exists, throw an exception
                    throw new Exception("The member is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Update(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Entry<Member>(_member).State = EntityState.Modified;
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //-------------------------------------
        public void Remove(Member member)
        {
            try
            {
                Member _member = GetMemberByID(member.MemberId);
                if (_member != null)
                {
                    var salesWpfAppDB = new SalesWpfappContext();
                    salesWpfAppDB.Members.Remove(_member);
                    salesWpfAppDB.SaveChanges();
                }
                else
                {
                    throw new Exception("The member does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
