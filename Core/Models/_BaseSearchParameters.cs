using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bissell.Core.Models
{
    public abstract class BaseSearchParameters
    {
        #region Properties

        public string Sort { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        #endregion
        #region Constructor

        public BaseSearchParameters()
        {
            Sort = nameof(BaseBug.Title).ToLower();
            PageNumber = 1;
            PageSize = 10;
        }

        #endregion
    }
}
