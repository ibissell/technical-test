using Bissell.Core.Interfaces;
using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Services.Repository
{
    public class BugHistoryRepository : IRepository<BugHistory, BugSearchParameters>
    {
        #region Properties

        private bool Disposed = false;

        private BugTrackerDbContext BugTrackerDbContext { get; set; }

        #endregion
        #region Constructors

        public BugHistoryRepository(BugTrackerDbContext bugTrackerDbContext)
        {
            BugTrackerDbContext = bugTrackerDbContext;
        }

        #endregion
        #region Methods

        public async Task<BugHistory> CreateAsync(BugHistory bugHistory)
        {
            await BugTrackerDbContext.AddAsync(bugHistory);
            await BugTrackerDbContext.SaveChangesAsync();

            return bugHistory;
        }

        public async Task<bool> DeleteAsync(int BugHistoryId)
        {
            BugHistory? BugHistory = await GetAsync(BugHistoryId);

            if (BugHistory != null)
            {
                BugTrackerDbContext.BugsHistory.Remove(BugHistory);
                await BugTrackerDbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<BugHistory>> GetAllAsync(List<int> ids)
        {
            List<BugHistory> BugHistorys = await BugTrackerDbContext.BugsHistory.Include(x => x.AssignedPerson)
                .Include(x => x.CurrentBug).ToListAsync();

            return BugHistorys;
        }

        public async Task<BugHistory?> GetAsync(int BugHistoryId)
        {
            BugHistory? BugHistory = await BugTrackerDbContext.BugsHistory.Include(x => x.AssignedPerson)
                .Include(x => x.CurrentBug).FirstOrDefaultAsync(x => x.BugHistoryId == BugHistoryId);

            return BugHistory;
        }

        public async Task<IPagedList<BugHistory>> SearchAsync(BugSearchParameters searchParameters)
        {
            //Where
            IQueryable<BugHistory> query = BugTrackerDbContext.BugsHistory;
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

            IPagedList<BugHistory> BugHistorys = await query.ToPagedListAsync(searchParameters.PageNumber, searchParameters.PageSize);

            return BugHistorys;
        }

        public async Task<BugHistory?> UpdateAsync(BugHistory updateBugHistory)
        {
            BugHistory? currentBugHistory = await GetAsync(updateBugHistory.BugHistoryId);

            if (currentBugHistory != null)
            {
                currentBugHistory.UpdatedDttm = DateTime.UtcNow;
                currentBugHistory.Title = updateBugHistory.Title;
                currentBugHistory.Description = updateBugHistory.Description;
                currentBugHistory.AssignedPerson = updateBugHistory.AssignedPerson;
                currentBugHistory.Priority = updateBugHistory.Priority;
                currentBugHistory.Status = updateBugHistory.Status;

                await BugTrackerDbContext.SaveChangesAsync();
            }

            return currentBugHistory;

        }

        #endregion
    }
}
