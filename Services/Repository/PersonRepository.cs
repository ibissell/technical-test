using Bissell.Core.Models;
using Bissell.Database;
using Bissell.Database.Entities;
using Bissell.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Bissell.Services.Repository
{
    public class PersonRepository : IRepository<Person, PersonSearchParameters>
    {
        #region Properties

        private bool Disposed = false;

        private BugTrackerDbContext BugTrackerDbContext { get; set; }

        #endregion
        #region Constructors

        public PersonRepository(BugTrackerDbContext bugTrackerDbContext)
        {
            BugTrackerDbContext = bugTrackerDbContext;
        }

        #endregion
        #region Methods

        public async Task<Person> CreateAsync(Person Person)
        {
            await BugTrackerDbContext.AddAsync(Person);
            await BugTrackerDbContext.SaveChangesAsync();

            return Person;
        }

        public async Task<bool> DeleteAsync(int PersonId)
        {
            Person? Person = await GetAsync(PersonId);

            if (Person != null)
            {
                BugTrackerDbContext.Persons.Remove(Person);
                await BugTrackerDbContext.SaveChangesAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Person>> GetAllAsync(List<int> ids)
        {
            List<Person> Persons = await BugTrackerDbContext.Persons
                .Include(x => x.AssignedBugs).ToListAsync();

            return Persons;
        }

        public async Task<Person?> GetAsync(int PersonId)
        {
            Person? Person = await BugTrackerDbContext.Persons
                .Include(x => x.AssignedBugs).FirstOrDefaultAsync(x => x.PersonId == PersonId);

            return Person;
        }

        public async Task<IPagedList<Person>> SearchAsync(PersonSearchParameters searchParameters)
        {
            //Where
            IQueryable<Person> query = BugTrackerDbContext.Persons;
            query = searchParameters.QuickSearch != null ? query.Where(x => x.Forename.Contains(searchParameters.QuickSearch) || x.Surname.Contains(searchParameters.QuickSearch)) : query;
            query = searchParameters.PersonIds != null ? query.Where(x => searchParameters.PersonIds.Contains(x.PersonId)) : query;
            query = searchParameters.BugIds != null ? query.Where(x => x.AssignedBugs!.Any(y => searchParameters.BugIds.Contains(y.BugId))) : query;

            //Order By
            query = searchParameters.Sort.ToLower() switch
            {
                "name" when !searchParameters.Sort.StartsWith('-') => query.OrderBy(x => x.Surname).ThenBy(x => x.Forename),
                "-name" when searchParameters.Sort.StartsWith('-') => query.OrderByDescending(x => x.Surname).ThenByDescending(x => x.Forename),
                _ => query.OrderBy(x => x.Surname).ThenBy(x => x.Forename)
            };

            IPagedList<Person> Persons = await query.ToPagedListAsync(searchParameters.PageNumber, searchParameters.PageSize);

            return Persons;
        }

        public async Task<Person?> UpdateAsync(Person updatePerson)
        {
            Person? currentPerson = await GetAsync(updatePerson.PersonId);

            if (currentPerson != null)
            {
                currentPerson.UpdatedDttm = DateTime.UtcNow;
                currentPerson.Forename = updatePerson.Forename;
                currentPerson.Surname = updatePerson.Surname;
                currentPerson.EmailAddress = updatePerson.EmailAddress;
                currentPerson.TelephoneNo = updatePerson.TelephoneNo;

                await BugTrackerDbContext.SaveChangesAsync();
            }

            return currentPerson;

        }

        #endregion
    }
}
