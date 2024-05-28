using Bissell.Core.Models;
using Bissell.Services.DataTransferObjects;
using System.Threading.Tasks;
using X.PagedList;

namespace Bissell.Services.Interfaces
{
    public interface IBugService
    {
        Task<BugDto?> AssignAsync(int bugId, int personId);
        Task<BugDto> CreateAsync(BugDto bugDto);
        Task<bool> DeleteAsync(int bugId);
        Task<BugDto?> GetAsync(int bugId);
        Task<IPagedList<BugDto>> SearchAsync(BugSearchParameters searchParameters);
        Task<BugDto?> UpdateAsync(BugDto bugDto);
        Task<BugDto?> UpdateAsync(int bugId, BugStatus status);
    }
}
