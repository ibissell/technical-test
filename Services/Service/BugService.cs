using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Bissell.Services.DataTransferObjects;
using Bissell.Services.Interfaces;
using Bissell.Services.Repository;
using X.PagedList;

namespace Bissell.Services.Service
{
    public class BugService : IBugService
    {
        #region Properties

        private BugTrackerDbContext BugTrackerDbContext { get; set; }

        private IRepository<Bug,BugSearchParameters> BugRepository { get; set; }

        private IRepository<Person, PersonSearchParameters> PersonRepository { get; set; }

        private IRepository<BugHistory, BugSearchParameters> BugHistoryRepository { get; set; }

        #endregion
        #region Constructor

        public BugService(BugTrackerDbContext bugTrackerDbContext, IRepository<Bug,BugSearchParameters> bugRepostiory, IRepository<BugHistory, BugSearchParameters> bugHistoryRepository) 
        {
            BugTrackerDbContext = bugTrackerDbContext;
            BugRepository = bugRepostiory;
            BugHistoryRepository = bugHistoryRepository;
        }

        #endregion
        #region Methods

        public async Task<IPagedList<BugDto>> SearchAsync(BugSearchParameters searchParameters)
        {
            IPagedList<Bug> bugs = await BugRepository.SearchAsync(searchParameters);
            IPagedList<BugDto> bugDtos = bugs.Select(x => (BugDto)x);

            return bugDtos;
        }

        public async Task<BugDto?> GetAsync(int bugId)
        {
            Bug? bug = await BugRepository.GetAsync(bugId);
            BugDto? bugDto = bug != null ? (BugDto)bug : null;
            
            return bugDto;
        }

        public async Task<BugDto> CreateAsync(BugDto bugDto)
        {
            Bug bug = (Bug)bugDto;
            bug = await BugRepository.CreateAsync(bug);

            bugDto = (BugDto)bug;

            return bugDto;
        }

        public async Task<BugDto?> UpdateAsync(BugDto bugDto)
        {
            await BugTrackerDbContext.Database.BeginTransactionAsync();

            try
            {
                Bug? originalBug = await BugRepository.GetAsync(bugDto.BugId);
                

                if (originalBug != null)
                {
                    Bug? updatebug = (Bug)bugDto;
                    BugHistory bugHistory = (BugHistory)originalBug;

                    updatebug = await BugRepository.UpdateAsync(updatebug);
                    bugHistory = await BugHistoryRepository.CreateAsync(bugHistory);

                    await BugTrackerDbContext.Database.CommitTransactionAsync();

                    bugDto = (BugDto)updatebug!;

                    return bugDto;
                }
            }
            catch (Exception ex)
            {
                await BugTrackerDbContext.Database.RollbackTransactionAsync();
                throw new Exception("An Exception occured trying to Update Bug",ex);
            }

            return null;
        }

        public async Task<BugDto?> UpdateAsync(int bugId, BugStatus status)
        {
            await BugTrackerDbContext.Database.BeginTransactionAsync();

            try
            {
                Bug? originalBug = await BugRepository.GetAsync(bugId);

                if (originalBug != null)
                {
                    BugHistory bugHistory = (BugHistory)originalBug;

                    originalBug.Status = status;

                    originalBug = await BugRepository.UpdateAsync(originalBug);
                    bugHistory = await BugHistoryRepository.CreateAsync(bugHistory);

                    await BugTrackerDbContext.Database.CommitTransactionAsync();

                    BugDto bugDto = (BugDto)originalBug!;

                    return bugDto;
                }
            }
            catch (Exception ex)
            {
                await BugTrackerDbContext.Database.RollbackTransactionAsync();
                throw new Exception("An Exception occured trying to Update Bug Status", ex);
            }

            return null;
        }

        public async Task<BugDto?> AssignAsync(int bugId, int personId)
        {
            await BugTrackerDbContext.Database.BeginTransactionAsync();

            try
            {
                Bug? originalBug = await BugRepository.GetAsync(bugId);

                if (originalBug != null)
                {
                    BugHistory bugHistory = (BugHistory)originalBug;
                    originalBug.AssignedPersonId = personId;

                    originalBug = await BugRepository.UpdateAsync(originalBug);
                    bugHistory = await BugHistoryRepository.CreateAsync(bugHistory);

                    await BugTrackerDbContext.Database.CommitTransactionAsync();

                    BugDto bugDto = (BugDto)originalBug!;

                    return bugDto;
                }
            }
            catch (Exception ex)
            {
                await BugTrackerDbContext.Database.RollbackTransactionAsync();
                throw new Exception("An Exception occured trying to Update Bug Status", ex);
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int bugId)
        {
            bool isDeleted = await BugRepository.DeleteAsync(bugId);

            return isDeleted;
        }

        #endregion
    }
}
