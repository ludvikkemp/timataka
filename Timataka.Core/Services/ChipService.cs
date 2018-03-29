using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ChipViewModels;

namespace Timataka.Core.Services
{
    public class ChipService : IChipService
    {
        private readonly IChipRepository _repo;

        public ChipService(IChipRepository repo)
        {
            _repo = repo;
        }

        public Task<Chip> AddChipAsync(Chip c)
        {
            throw new NotImplementedException();
        }

        public Task<ChipInHeat> AssignChipToUserInHeat(ChipInHeat c)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditChipAsync(Chip c)
        {
            throw new NotImplementedException();
        }

        public Task<Chip> GetChipByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<Chip> GetChipByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<ChipInHeat> GetChipInEventByCodeAndUserId(string code, string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChipViewModel> GetChips()
        {
            return _repo.GetChips();

        }

        public Task<ChipInHeat> GetChipsInHeatForUser()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChipInHeat> GetChipsInHeatsForUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkInvalid(ChipInHeat c)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveChipAsync(Chip c)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateChipStory(ChipInHeat c)
        {
            throw new NotImplementedException();
        }
    }
}
