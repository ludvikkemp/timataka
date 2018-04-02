using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Services
{
    public interface IChipService
    {
        Task<Chip> AddChipAsync(Chip c);
        Task<Chip> GetChipByCodeAsync(string code);
        Task<Boolean> EditChipAsync(Chip c);
        Task<Boolean> RemoveChipAsync(Chip c);
        Task<Boolean> UpdateChipStory(ChipInHeat c);
        Task<Boolean> MarkInvalid(ChipInHeat c);
        IEnumerable<ChipViewModel> GetChips();

        Task<ChipInHeat> AssignChipToUserInHeatAsync(ChipInHeat c);
        IEnumerable<ChipInHeat> GetChipsInHeats();
        IEnumerable<ChipInHeat> GetChipsInHeatsForUser(string userId);
        IEnumerable<ChipInHeat> GetChipsInHeatsForUserInHeat(string userId, int heatId);
        IEnumerable<ChipInHeat> GetChipsInHeatsForEvent(int eventId);
        IEnumerable<ChipInHeat> GetChipsInHeatForHeat(int heatId);
        IEnumerable<ChipInHeat> GetChipsInCompetitionInstanceForUser(int competitionInstanceId, string userId);
        ChipInHeat GetChipInHeatByCodeAndUserId(string code, string userId, int heatId);

    }
}