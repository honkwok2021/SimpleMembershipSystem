using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SimpleMembershipSystem.Models.Membership;

namespace SimpleMembershipSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Member> Members { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SimpleMembershipSystem.Models.Membership.Create> Create { get; set; }
        public DbSet<SimpleMembershipSystem.Models.Membership.Detail> Detail { get; set; }
        public DbSet<SimpleMembershipSystem.Models.Membership.Edit> Edit { get; set; }
        public DbSet<SimpleMembershipSystem.Models.Membership.Delete> Delete { get; set; }
    }
}
