using Bissell.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bissell.Database.Entities
{
    /// <summary></summary>
    public class Bug : BaseBug
    {
        #region Properties

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int BugId { get; set; }

        [ForeignKey(nameof(Person))]
        public override int? AssignedPersonId { get; set; }

        public virtual Person? AssignedPerson { get; set; }

        public virtual List<BugHistory>? History { get; set; }

        #endregion
        #region Constructor

        public Bug() : base()
        {

        }

        #endregion
    }
}
