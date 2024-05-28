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

        [Column(TypeName = "varchar(50)")]
        public override string Forename { get; set; }

        [Column(TypeName = "varchar(50)")]
        public override string Surname { get; set; }

        [Column(TypeName = "varchar(255)")]
        public override string? EmailAddress { get; set; }

        [Column(TypeName = "varchar(50)")]
        public override string? TelephoneNo { get; set; }

        #endregion
        #region Constructor

        public Person() : base()
        {
            Forename = string.Empty;
            Surname = string.Empty;
        }

        #endregion
    }
}
