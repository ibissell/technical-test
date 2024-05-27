using Bissell.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bissell.Database.Entities
{
    /// <summary></summary>
    public class Person : BasePerson
    {
        #region Properties

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int PersonId { get; set; }

        public virtual List<Bug>? AssignedBugs { get; set; }

        #endregion
        #region Constructor

        public Person() : base()
        {
    
        }

        #endregion
    }
}
