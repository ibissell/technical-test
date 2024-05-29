using Bissell.Core.Models;
using Bissell.Database.Entities;

namespace Bissell.Services.DataTransferObjects
{
    public class BugHistoryDto : BaseBug
    {
        #region Properties

        public int BugHistoryId { get; set; }

        public PersonDto? AssignedPerson { get; set; }

        #endregion
        #region Constructors

        public BugHistoryDto() : base()
        {
            
        }

        #endregion
        #region Methods

        public static explicit operator BugHistoryDto(BugHistory bugHistory) => new BugHistoryDto()
        {
            BugHistoryId= bugHistory.BugHistoryId,
            BugId = bugHistory.BugId,
            InsertedDttm = bugHistory.InsertedDttm,
            UpdatedDttm = bugHistory.UpdatedDttm,
            AssignedPersonId = bugHistory.AssignedPersonId,
            AssignedPerson = bugHistory.AssignedPerson != null ? (PersonDto)bugHistory.AssignedPerson : null,
            Title = bugHistory.Title,
            Description = bugHistory.Description,
            Status = bugHistory.Status,
            Priority = bugHistory.Priority,
        };

        public static explicit operator BugHistoryDto(BugDto butDto) => new BugHistoryDto()
        {
            BugId = butDto.BugId,
            InsertedDttm = butDto.InsertedDttm,
            UpdatedDttm = butDto.UpdatedDttm,
            AssignedPersonId = butDto.AssignedPersonId,
            AssignedPerson = butDto.AssignedPerson != null ? (PersonDto)butDto.AssignedPerson : null,
            Title = butDto.Title,
            Description = butDto.Description,
            Status = butDto.Status,
            Priority = butDto.Priority,
        };

        public static explicit operator BugHistory(BugHistoryDto bugHistoryDto) => new BugHistory()
        {
            BugHistoryId = bugHistoryDto.BugHistoryId,
            BugId = bugHistoryDto.BugId,
            InsertedDttm = bugHistoryDto.InsertedDttm,
            UpdatedDttm = bugHistoryDto.UpdatedDttm,
            AssignedPersonId = bugHistoryDto.AssignedPersonId,
            AssignedPerson = bugHistoryDto.AssignedPerson != null ? (Person)bugHistoryDto.AssignedPerson : null,
            Title = bugHistoryDto.Title,
            Description = bugHistoryDto.Description,
            Status = bugHistoryDto.Status,
            Priority = bugHistoryDto.Priority,
        };

        #endregion
    }
}
