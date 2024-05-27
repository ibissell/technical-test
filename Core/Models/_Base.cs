
namespace Bissell.Core.Models
{
    public abstract class Base
    {
        #region Properties

        public virtual DateTime? InsertedDttm { get; set; }

        public virtual DateTime? UpdatedDttm { get; set; }

        #endregion
        #region Constructors

        public Base()
        {
            InsertedDttm = DateTime.Now;
            InsertedDttm = null;
        }

        #endregion
    }
}
