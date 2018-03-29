using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface IChipRepository : IDisposable
    {
        Chip Add(Chip c);
        Task<Chip> AddAsync(Chip c);

        IEnumerable<Chip> GetChips();
        Chip GetChipByCode(string code);
        Task<Chip> GetChipByCodeAsync(string code);

        Boolean EditChip(Chip c);
        Task<Boolean> EditChipAsync(Chip c);

        Boolean RemoveChip(Chip c);
        Task<Boolean> RemoveChipAsync(Chip c);

        Boolean AssignChipToUserInHeat(ChipInHeat c);
        Task<Boolean> AssignChipToUserInHeatAsync(ChipInHeat c);

        IEnumerable<ChipInHeat> GetChipsInHeats();
        Task<ChipInHeat> GetChipInHeatByCodeAndUserId(string code, string userId);

        Boolean EditChipInHeat(ChipInHeat c);
        Task<Boolean> EditChipInHeatAsync(ChipInHeat c);

        Boolean RemoveChipInHeat(ChipInHeat c);
        Task<Boolean> RemoveChipInHeatAsync(ChipInHeat c);
    }
}