using Bissell.Core.Models;
using Bissell.Database.Entities;

namespace Bissell.Services.DataTransferObjects
{
    public class PersonDto : BasePerson
    {
        #region Constructors

        public PersonDto() : base()
        {

        }

        #endregion
        #region Methods

        public static explicit operator PersonDto(Person person) => new PersonDto()
        {
            PersonId = person.PersonId,
            InsertedDttm = person.InsertedDttm,
            UpdatedDttm = person.UpdatedDttm,
            Forename = person.Forename,
            Surname = person.Surname,
            EmailAddress = person.EmailAddress,
            TelephoneNo = person.TelephoneNo
        };

        public static explicit operator Person(PersonDto personDto) => new Person()
        {
            PersonId = personDto.PersonId,
            InsertedDttm = personDto.InsertedDttm,
            UpdatedDttm = personDto.UpdatedDttm,
            Forename = personDto.Forename,
            Surname = personDto.Surname,
            EmailAddress = personDto.EmailAddress,
            TelephoneNo = personDto.TelephoneNo
        };

        #endregion
    }
}
