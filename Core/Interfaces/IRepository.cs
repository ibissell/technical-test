using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Bissell.Core.Interfaces
{
    /// <summary></summary>
    /// <typeparam name="T1">Entity Type</typeparam>
    /// <typeparam name="T2">Search Parameters Type</typeparam>
    public interface IRepository<T1,T2> 
    {
        public Task<T1?> GetAsync(int id);

        public Task<List<T1>> GetAllAsync(List<int> ids);

        public Task<IPagedList<T1>> SearchAsync(T2 searchParameters);

        public Task<T1> CreateAsync(T1 entity);

        public Task<T1?> UpdateAsync(T1 entity);

        public Task<bool> DeleteAsync(int id);
    }
}
