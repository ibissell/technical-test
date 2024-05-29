namespace Bissell.Core.Models
{
    /// <summary></summary>
    public abstract class BasePerson : Base
    {
        #region Properties

        public virtual int PersonId { get; set; }

        public virtual string Forename { get; set; }

        public virtual string Surname { get; set; }

        public virtual string? EmailAddress { get; set; }

        public virtual string? TelephoneNo { get; set; }

        #endregion
        #region Constructor

        public BasePerson() : base()
        {
            Forename = string.Empty;
            Surname = string.Empty;
        }

        #endregion
    }

    public class PersonSearchParameters : BaseSearchParameters
    {
        #region Properties 

        public int[]? BugIds { get; set; }

        public int[]? PersonIds { get; set; }

        public string? QuickSearch { get; set; }

        #endregion
        #region Constructors

        public PersonSearchParameters() : base()
        {
            BugIds = null;
            PersonIds = null;
            QuickSearch = null;
        }

        #endregion
    }
}
