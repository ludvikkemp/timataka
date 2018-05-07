using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Services
{
    public interface IChipService
    {
        // *** ADD & ASSIGN *** //
        Task<Chip> AddChipAsync(Chip c);
        bool AssignChipToUserInHeat(ChipInHeat c);
        Task<ChipInHeatViewModel> AssignChipToUserInHeatAsync(ChipInHeat c);

        // *** EDIT *** //
        Task<Boolean> EditChipAsync(Chip c);
        Boolean EditChipInHeat(ChipInHeat c);

        // *** GET *** //
        IEnumerable<ChipViewModel> GetChips();
        IEnumerable<Chip> Get();
        IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId);
        Task<Chip> GetChipByCodeAsync(string code);
        IEnumerable<ChipInHeat> GetChipsInHeats();
        IEnumerable<ChipInHeat> GetChipsInHeatsForUser(string userId);
        IEnumerable<ChipInHeat> GetChipsInHeatsForUserInHeat(string userId, int heatId);
        IEnumerable<ChipInHeat> GetChipsInHeatsForEvent(int eventId);
        IEnumerable<ChipInHeat> GetChipsInHeatForHeat(int heatId);
        IEnumerable<ChipInHeat> GetChipsInCompetitionInstanceForUser(int competitionInstanceId, string userId);
        ChipInHeatViewModel GetChipInHeatByCodeAndUserId(string code, string userId, int heatId);
        Task<Chip> GetChipByNumberAsync(int modelNumber);
        Task<ChipInHeat> GetChipInHeatByCodeUserIdAndHeatId(string modelOldChipCode, string userId, int modelOldHeatId);

        // *** REMOVE *** //
        Task<Boolean> RemoveChipAsync(Chip c);
        Boolean RemoveChipInHeat(ChipInHeat c);
        Task<Boolean> RemoveChipInHeatAsync(ChipInHeat c);

        // *** UPDATE & MARK INVALID *** //
        Task<Boolean> UpdateChipStory(ChipInHeat c);
        Task<Boolean> MarkInvalid(ChipInHeat c);
    }
}