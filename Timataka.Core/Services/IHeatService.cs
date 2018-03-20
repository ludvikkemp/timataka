using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Services
{
    public interface IHeatService
    {
        Task<Heat> AddAsync(int eventId);
        Task<Heat> EditAsync(Heat h);
        Task<int> RemoveAsync(int heatId);
        IEnumerable<Heat> GetAllHeats();
        IEnumerable<Heat> GetHeatsForEvent(int eventId);
        IEnumerable<Heat> GetDeletedHeatsForEvent(int eventId);
        Task<Heat> GetHeatByIdAsync(int id);
        Task ReorderHeatsAsync(int eventId);
        
        //Contestants In Heat
        IEnumerable<ContestantInHeat> GetContestantsInHeat(int id);
        IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id);
    }
}
