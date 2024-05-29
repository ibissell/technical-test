using Bissell.Core.Models;
using Bissell.Database.Entities;
using Bissell.Services.DataTransferObjects;
using Bissell.Services.Interfaces;
using X.PagedList;

namespace Bissell.Services.Service
{
    public class PersonService : IPersonService
    {
        #region Properties

        private IRepository<Person, PersonSearchParameters> PersonRepository { get; set; }

        #endregion
        #region Constructor

        public PersonService(IRepository<Person, PersonSearchParameters> personRepository) 
        {
            PersonRepository = personRepository;
        }

        #endregion
        #region Methods

        public async Task<IPagedList<PersonDto>> SearchAsync(PersonSearchParameters searchParameters)
        {
            IPagedList<Person> persons = await PersonRepository.SearchAsync(searchParameters);
            IPagedList<PersonDto> personDtos = persons.Select(x => (PersonDto)x);

            return personDtos;
        }

        public async Task<PersonDto?> GetAsync(int personId)
        {
            Person? person = await PersonRepository.GetAsync(personId);
            PersonDto? personDto = person != null ? (PersonDto)person : null;
            
            return personDto;
        }

        public async Task<PersonDto> CreateAsync(PersonDto personDto)
        {
            Person person = (Person)personDto;
            person = await PersonRepository.CreateAsync(person);

            personDto = (PersonDto)person;

            return personDto;
        }

        public async Task<PersonDto?> UpdateAsync(PersonDto personDto)
        {

            Person? person = (Person)personDto;
            person = await PersonRepository.UpdateAsync(person);

            if (person != null)
            {
                personDto = (PersonDto)person;
                return personDto;
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteAsync(int personId)
        {
            bool isDeleted = await PersonRepository.DeleteAsync(personId);

            return isDeleted;
        }

        #endregion
    }
}
