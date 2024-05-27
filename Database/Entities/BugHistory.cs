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

        #endregion
        #region Constructor

        public BugHistory() : base()
        {

        }

        #endregion
    }
}
