﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.AdminDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.EventViewModels;
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
        
        // *** CONTESTANTS IN HEATS *** //
        IEnumerable<ContestantInHeatViewModel> GetContestantsInHeat(int id);
        IEnumerable<ApplicationUser> GetApplicationUsersInHeat(int id);
        ContestantInHeat GetContestantInHeatById(int heatId, string userId);
        Task<ContestantInEventViewModel> GetContestantInEventViewModelAsync(string userId, int heatId);
        IEnumerable<ApplicationUser> GetUsersNotInAnyHeatUnderEvent(int eventId);
        Task<ContestantInHeat> GetContestantsInHeatByUserIdAndHeatIdAsync(string userId, int heatId);

        void EditContestantInHeat(ContestantInHeat h);
        Task EditAsyncContestantInHeat(ContestantInHeat h);

        void RemoveContestantInHeat(ContestantInHeat h);
        Task RemoveAsyncContestantInHeat(ContestantInHeat h);

        void AddContestantInHeat(ContestantInHeat h);
        Task AddAsyncContestantInHeat(ContestantInHeat h);
    }
}
