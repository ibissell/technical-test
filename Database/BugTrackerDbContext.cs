using Bissell.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bissell.Database
{
    public class BugTrackerDbContext : DbContext
    {
        #region Entities

        public DbSet<Bug> Bugs { get; set; }

        public DbSet<BugHistory> BugsHistory { get; set; }

        public DbSet<Person> Persons { get; set; }

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
            modelBuilder.Entity<Bug>().HasOne(e => e.AssignedPerson).WithMany(e => e.AssignedBugs).HasForeignKey(e => e.AssignedPersonId).OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
