using Bissell.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Bissell.Database
{
    public class BugTrackerDbContext : DbContext
    {
        #region Entities

        public virtual DbSet<Bug> Bugs { get; set; }

        public virtual DbSet<BugHistory> BugsHistory { get; set; }

        public virtual DbSet<Person> Persons { get; set; }

        #endregion
        #region Constructor

        public BugTrackerDbContext()
        {
        }

        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options) : base(options)
        {
        }

        #endregion
        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>().Property(b => b.InsertedDttm).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Person>().Property(b => b.InsertedDttm).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Bug>().HasOne(e => e.AssignedPerson).WithMany(e => e.AssignedBugs).HasForeignKey(e => e.AssignedPersonId).OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
