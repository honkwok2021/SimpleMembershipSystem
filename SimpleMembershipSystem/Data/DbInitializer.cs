using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMembershipSystem.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Members.Any())
            {
                return;
            }

            List<Member> memberList = new List<Member>
            {
                new Member{FirstName="Peter", LastName="Green", DateOfBirth=DateTime.Parse("04-12-2010"), PhoneNumber="042766778", Email="test1111@example.com"},
                new Member{FirstName="Jane", LastName="Lee", DateOfBirth=DateTime.Parse("04-11-1997"), PhoneNumber="04234567009", Email="2222211@example.com"},
            };

            var members = new Member[]
            {
                new Member{FirstName="John", LastName="Smith", DateOfBirth=DateTime.Parse("01-05-2000"), PhoneNumber="0423456778", Email="test@example.com"},
                new Member{FirstName="Mary", LastName="Brown", DateOfBirth=DateTime.Parse("01-06-1999"), PhoneNumber="04234567667", Email="22@example.com"},
                new Member{FirstName="Green", LastName="Obrian", DateOfBirth=DateTime.Parse("01-04-2001"), PhoneNumber="046099856778", Email="3test@example.com"}
            };
            foreach (Member m in members)
            {
                context.Members.Add(m);
            }
            foreach (Member l in memberList)
            {
                context.Members.Add(l);
            }
            context.SaveChanges();
        }
    }
}
