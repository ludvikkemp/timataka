using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ClubViewModels;

namespace Timataka.Core.Services
{
    public interface IClubService
    {
        IEnumerable<Club> GetListOfCLubs();
        Task<bool> ClubExistsAsync(string modelName);
        Task<Club> AddAsync(CreateClubViewModel c);
        Task<Club> EditClubAsync(EditClubViewModel m);
        EditClubViewModel GetClubViewModelById(int id);
        Task<int> RemoveAsync(int clubId);
    }
}
