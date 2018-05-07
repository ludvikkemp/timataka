using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Data.Repositories
{
    public interface IChipRepository : IDisposable
    {
        Chip Add(Chip c);
        Task<Chip> AddAsync(Chip c);

        IEnumerable<Chip> Get();
        IEnumerable<ChipViewModel> GetChips();
        Chip GetChipByCode(string code);
        Task<Chip> GetChipByCodeAsync(string code);

        Chip GetChipByNumber(int number);
        Task<Chip> GetChipByNumberAsync(int number);

        Boolean EditChip(Chip c);
        Task<Boolean> EditChipAsync(Chip c);

        Boolean RemoveChip(Chip c);
        Task<Boolean> RemoveChipAsync(Chip c);

        Boolean AssignChipToUserInHeat(ChipInHeat c);
        Task<Boolean> AssignChipToUserInHeatAsync(ChipInHeat c);

        IEnumerable<ChipInHeat> GetChipsInHeats();
        Task<ChipInHeat> GetChipInHeatByCodeAndUserId(string code, string userId);
        IEnumerable<ChipInHeatViewModel> GetChipsInHeat(int heatId);

        Boolean EditChipInHeat(ChipInHeat c);
        Task<Boolean> EditChipInHeatAsync(ChipInHeat c);

        Boolean RemoveChipInHeat(ChipInHeat c);
        Task<Boolean> RemoveChipInHeatAsync(ChipInHeat c);
        Task<ChipInHeat> GetChipInHeatByCodeUserIdHeatId(string modelOldChipCode, string userId, int modelOldHeatId);
        Task<int> GetInstanceIdForHeat(int heatId);
    }
}