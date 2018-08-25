using FinalSplitWise.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalSplitWise.Data
{
    public class SystemDBContext : DbContext
    {
        public SystemDBContext(DbContextOptions<SystemDBContext> options) : base(options)
        {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Friend> friends { get; set; }
        public DbSet<Group> groups { get; set; }
        public DbSet<GroupMember> groupMembers { get; set; }
        public DbSet<Bill> bills { get; set; }
        public DbSet<Payer> payers { get; set; }
        public DbSet<SharedWith> sharedWiths { get; set; }
        public DbSet<Settlements> settlements { get; set; }
        public DbSet<Transaction> transactions { get; set; }
    }
}