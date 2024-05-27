using Bissell.Core.Interfaces;
using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Services.Repository
{
    public class BugRepository : IRepository<Bug, BugSearchParameters>
    {
        #region Properties

        private bool Disposed = false;

        private BugTrackerDbContext BugTrackerDbContext { get; set; }

        #endregion
        #region Constructors

        public BugRepository(BugTrackerDbContext bugTrackerDbContext)
        {
            BugTrackerDbContext = bugTrackerDbContext;
        }

        #endregion
        #region Methods

        public async Task<Bug> CreateAsync(Bug bug)
        {
            await BugTrackerDbContext.AddAsync(bug);
            await BugTrackerDbContext.SaveChangesAsync();

            return bug;
        }

        public async Task<bool> DeleteAsync(int bugId)
        {
            Bug? bug = await GetAsync(bugId);

            if (bug != null)
            {
                BugTrackerDbContext.Bugs.Remove(bug);
                await BugTrackerDbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Bug>> GetAllAsync(List<int> ids)
        {
            List<Bug> bugs = await BugTrackerDbContext.Bugs.Include(x => x.AssignedPerson)
                .Include(x => x.History).AsSplitQuery().ToListAsync();

            return bugs;
        }

        public async Task<Bug?> GetAsync(int bugId)
        {
            Bug? bug = await BugTrackerDbContext.Bugs.Include(x => x.AssignedPerson)
                .Include(x => x.History).FirstOrDefaultAsync(x => x.BugId == bugId);

            return bug;
        }

        public async Task<IPagedList<Bug>> SearchAsync(BugSearchParameters searchParameters)
        {
            //Where
            IQueryable<Bug> query = BugTrackerDbContext.Bugs;
            query = searchParameters.QuickSearch != null ? query.Where(x => x.Title.Contains(searchParameters.QuickSearch) || x.Description.Contains(searchParameters.QuickSearch)) : query;
            query = searchParameters.BugIds != null ? query.Where(x => searchParameters.BugIds.Contains(x.BugId)) : query;
            query = searchParameters.AssignedPersonIds != null ? query.Where(x => searchParameters.AssignedPersonIds.Contains(x.AssignedPersonId!.Value)) : query;
            query = searchParameters.Statuses != null ? query.Where(x => searchParameters.Statuses.Contains(x.Status)) : query;
            query = searchParameters.Priorities != null ? query.Where(x => searchParameters.Priorities.Contains(x.Priority)) : query;

            //Order By
            query = searchParameters.Sort.Replace("-","").ToLower() switch
            {
                "title" when !searchParameters.Sort.StartsWith('-') => query.OrderBy(x => x.Title),
                "title" when searchParameters.Sort.StartsWith('-') => query.OrderByDescending(x => x.Title),
                "status" when !searchParameters.Sort.StartsWith('-') => query.OrderBy(x => x.Status),
                "status" when searchParameters.Sort.StartsWith('-') => query.OrderByDescending(x => x.Status),
                "priority" when !searchParameters.Sort.StartsWith('-') => query.OrderBy(x => x.Priority),
                "priority" when searchParameters.Sort.StartsWith('-') => query.OrderByDescending(x => x.Priority),
                "assignedperson" when !searchParameters.Sort.StartsWith('-') => query.OrderBy(x => x.AssignedPerson!.Surname).ThenBy(x => x.AssignedPerson!.Forename),
                "assignedperson" when searchParameters.Sort.StartsWith('-') => query.OrderByDescending(x => x.AssignedPerson!.Surname).ThenByDescending(x => x.AssignedPerson!.Forename),
                _ => query.OrderBy(x => x.Title)
            };

            IPagedList<Bug> bugs = await query.ToPagedListAsync(searchParameters.PageNumber, searchParameters.PageSize);

            return bugs;
        }

        public async Task<Bug?> UpdateAsync(Bug updateBug)
        {
            Bug? currentBug = await GetAsync(updateBug.BugId);

            if (currentBug != null)
            {
                currentBug.UpdatedDttm = DateTime.UtcNow;
                currentBug.Title = updateBug.Title;
                currentBug.Description = updateBug.Description;
                currentBug.AssignedPerson = updateBug.AssignedPerson;
                currentBug.Priority = updateBug.Priority;
                currentBug.Status = updateBug.Status;

                await BugTrackerDbContext.SaveChangesAsync();
            }

            return currentBug;

        }

        #endregion
    }
}
