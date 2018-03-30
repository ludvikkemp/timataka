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
        IEnumerable<ChipViewModel> GetChips();
        IEnumerable<Chip> Get();

        bool AssignChipToUserInHeat(ChipInHeat c);
        Task<ChipInHeat> GetChipInEventByCodeAndUserId(string code, string userId);
        IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId);
        Task<Boolean> UpdateChipStory(ChipInHeat c);
        IEnumerable<ChipInHeat> GetChipsInHeatsForUser(string id);
        Task<Boolean> MarkInvalid (ChipInHeat c);
    }
}