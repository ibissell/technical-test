namespace Bissell.Core.Models
{
    /// <summary></summary>
    public abstract class BaseBug : Base
    {
        #region Properties

        public virtual int BugId { get; set; }

        public virtual int? AssignedPersonId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Description { get; set; }

        public virtual BugStatus Status { get; set; }

        public virtual BugPriority Priority { get; set; }

        #endregion
        #region Constructor

        public BaseBug() : base()
        {
            Title = string.Empty;
            Description = string.Empty;
            Status = BugStatus.NotStarted;
            Priority = BugPriority.Low;
        }

        #endregion
    }

    public class BugSearchParameters : BaseSearchParameters
    {
        #region Properties 

        public int[]? BugIds { get; set; }

        public int[]? AssignedPersonIds { get; set; }

        public string? QuickSearch { get; set; }

        public BugStatus[]? Statuses { get; set; }

        public BugPriority[]? Priorities { get; set; }

        #endregion
        #region Constructors

        public BugSearchParameters() : base()
        {
            BugIds = null;
            AssignedPersonIds = null;
            Statuses = null;
            Priorities = null;
        }

        #endregion
    }

    public enum BugStatus
    {
        NotStarted = 00,
        Reopened = 01,
        Assigned = 10,
        Open = 20,
        Fixed = 30,
        Retest = 40,
        Verified = 50,
        Closed = 60,

    }

    public enum BugPriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }
}
