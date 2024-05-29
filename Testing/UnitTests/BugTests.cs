using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Bissell.Services.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using X.PagedList;

namespace Testing.UnitTests
{
    public class BugTests
    {
        private readonly BugRepository BugRepository;


        public BugTests() {

            var mock = new Mock<BugTrackerDbContext>();
            mock.Setup(x => x.Bugs).ReturnsDbSet(GenerateDbSet());

            BugRepository = new BugRepository(mock.Object);
        }

        [Fact]
        public async Task Search()
        {
            IPagedList<Bug> bugs = await BugRepository.SearchAsync(new Bissell.Core.Models.BugSearchParameters()
            {
                QuickSearch = "Node.js"
            });

            Assert.True(bugs.Count == 3);
        }

        [Fact]
        public async Task Get()
        {

            Bug? bug = await BugRepository.GetAsync(4);

            Assert.Null(bug);

            bug = await BugRepository.GetAsync(3);

            Assert.Equal(bug?.Status, BugStatus.Fixed);
        }

        private List<Bug> GenerateDbSet()
        {
            return new List<Bug> {
                new Bug()
                {
                    BugId = 1,
                    Title = "Node.js doesn't load",
                    Description = "Can't get Node.js to run",
                    Priority = BugPriority.High,
                    Status = BugStatus.NotStarted
                },
                new Bug()
                {
                    BugId = 2,
                    Title = "Node.js doesn't load",
                    Description = "Can't get Node.js to run",
                    Priority = BugPriority.Medium,
                    Status = BugStatus.NotStarted
                },
                new Bug()
                {
                    BugId = 3,
                    Title = "Node.js doesn't load",
                    Description = "Can't get Node.js to run",
                    Priority = BugPriority.Low,
                    Status = BugStatus.Fixed
                }
            };
        }
    }
}