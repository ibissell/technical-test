using Bissell.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bissell.Database.Entities
{
    /// <summary></summary>
    public class BugHistory : BaseBug
    {
        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BugHistoryId { get; set; }

        [ForeignKey(nameof(Bug))]
        public override int BugId { get; set; }

        public virtual Bug? CurrentBug { get;set;}

        [ForeignKey(nameof(Person))]
        public override int? AssignedPersonId { get; set; }

        public virtual Person? AssignedPerson { get; set; }

        [Column(TypeName = "varchar(50)")]
        public override string Title { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string Description { get; set; }

        #endregion
        #region Constructor

        public BugHistory() : base()
        {
            Title = String.Empty;
            Description = String.Empty;
        }

        #endregion
        #region Methods

        public static explicit operator BugHistory(Bug bug) => new BugHistory()
        {
            BugId = bug.BugId,
            InsertedDttm = bug.InsertedDttm,
            UpdatedDttm = bug.UpdatedDttm,
            AssignedPersonId = bug.AssignedPersonId,
            AssignedPerson = bug.AssignedPerson != null ? bug.AssignedPerson : null,
            Title = bug.Title,
            Description = bug.Description,
        };

        public static explicit operator Bug(BugHistory bugHistory) => new Bug()
        {
            BugId = bugHistory.BugId,
            InsertedDttm = bugHistory.InsertedDttm,
            UpdatedDttm = bugHistory.UpdatedDttm,
            AssignedPersonId = bugHistory.AssignedPersonId,
            AssignedPerson = bugHistory.AssignedPerson != null ? bugHistory.AssignedPerson : null,
            Title = bugHistory.Title,
            Description = bugHistory.Description,
        };

        #endregion
    }
}
