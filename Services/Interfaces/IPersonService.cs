using Bissell.Core.Models;
using Bissell.Services.DataTransferObjects;
using X.PagedList;


namespace Bissell.Services.Interfaces
{
    public interface IPersonService
    {
        Task<PersonDto> CreateAsync(PersonDto personDto);
        Task<bool> DeleteAsync(int personId);
        Task<PersonDto?> GetAsync(int personId);
        Task<IPagedList<PersonDto>> SearchAsync(PersonSearchParameters searchParameters);
        Task<PersonDto?> UpdateAsync(PersonDto personDto);
    }
}
