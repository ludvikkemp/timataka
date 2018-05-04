using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.HeatViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IHeatRepository : IDisposable
    {
        void Insert(Heat h);
        Task InsertAsync(Heat h);

        IEnumerable<Heat> Get();

        Heat GetById(int id);
        Task<Heat> GetByIdAsync(int id);

        void Edit(Heat h);
        Task EditAsync(Heat h);

        void Remove(Heat h);
        Task RemoveAsync(Heat h);

        //Contestants In Heat
        IEnumerable<ContestantInHeatViewModel> GetContestantsInHeat(int heatId);
        IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id);
        ContestantInHeat GetContestantInHeatById(int heatId, string userId);
        IEnumerable<ApplicationUser> GetUsersNotInAnyHeatUnderEvent(int eventId);

        void EditContestantInHeat(ContestantInHeat h);
        Task EditAsyncContestantInHeat(ContestantInHeat h);

        void RemoveContestantInHeat(ContestantInHeat h);
        Task RemoveAsyncContestantInHeat(ContestantInHeat h);

        void InsertContestantInHeat(ContestantInHeat h);
        Task InsertAsyncContestantInHeat(ContestantInHeat h);
        ContestantInHeat GetContestantInHeatByUserId(string userId, int heatId);
    }
}
