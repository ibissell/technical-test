using Bissell.Core.Models;
using Bissell.Database.Entities;

namespace Bissell.Services.DataTransferObjects
{
    public class BugDto : BaseBug
    {
        #region Properties

        public PersonDto? AssignedPerson { get; set; }

        public List<BugHistoryDto> History { get; set; }

        #endregion
        #region Constructor

        public BugDto()
        {
            AssignedPerson = null;
            History = new();
        }

        #endregion
        #region Methods

        public static explicit operator BugDto(Bug bug) => new BugDto()
        {
            BugId = bug.BugId,
            InsertedDttm = bug.InsertedDttm,
            UpdatedDttm = bug.UpdatedDttm,
            AssignedPersonId = bug.AssignedPersonId,
            AssignedPerson = bug.AssignedPerson != null ? (PersonDto)bug.AssignedPerson : null,
            History = bug.History != null ? bug.History.ConvertAll(x => (BugHistoryDto)x) : new(),
            Title = bug.Title,
            Status = bug.Status,
            Priority = bug.Priority,
            Description = bug.Description,
        };

        public static explicit operator Bug(BugDto bugDto) => new Bug()
        {
            BugId = bugDto.BugId,
            InsertedDttm = bugDto.InsertedDttm,
            UpdatedDttm = bugDto.UpdatedDttm,
            AssignedPersonId = bugDto.AssignedPersonId,
            AssignedPerson = bugDto.AssignedPerson != null ? (Person)bugDto.AssignedPerson : null,
            History = bugDto.History != null ? bugDto.History.ConvertAll(x => (BugHistory)x) : new(),
            Title = bugDto.Title,
            Status = bugDto.Status,
            Priority = bugDto.Priority,
            Description = bugDto.Description,
        };

        public static explicit operator BugHistory(BugDto bugDto) => new BugHistory()
        {
            BugId = bugDto.BugId,
            InsertedDttm = bugDto.InsertedDttm,
            UpdatedDttm = bugDto.UpdatedDttm,
            AssignedPersonId = bugDto.AssignedPersonId,
            AssignedPerson = bugDto.AssignedPerson != null ? (Person)bugDto.AssignedPerson : null,
            Title = bugDto.Title,
            Status = bugDto.Status,
            Priority = bugDto.Priority,
            Description = bugDto.Description,
        };

        #endregion
    }
}
