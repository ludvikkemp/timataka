using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.ClubViewModels;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Services
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _repo;

        public ClubService(IClubRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Club> GetListOfCLubs()
        {
            var clubs =_repo.Get();
            return clubs;
        }

        public async Task<bool> ClubExistsAsync(string modelName)
        {
            var result = await _repo.GetClubByNameAsync(modelName);
            if (result != null) return true;
            return false;
        }

        public async Task<Club> AddAsync(CreateClubViewModel c)
        {
            var newClub = new Club
            {
                Name = c.Name,
                Webpage = c.Webpage,
                Email = c.Email,
                Phone = c.Phone,
                NameAbbreviation = c.NameAbbreviation
            };
            await _repo.InsertAsync(newClub);
            return newClub;
        }

        public async Task<Club> EditClubAsync(EditClubViewModel m)
        {
            var c = await _repo.GetByIdAsync(m.Id);
            c.Name = m.Name;
            c.Webpage = m.Webpage;
            c.Email = m.Email;
            c.Phone = m.Phone;
            c.NameAbbreviation = m.NameAbbreviation;
            await _repo.EditAsync(c);
            return c;
        }

        public EditClubViewModel GetClubViewModelById(int id)
        {
            var c =_repo.GetById(id);
            var model = new EditClubViewModel
            {
                Name = c.Name,
                Webpage = c.Webpage,
                NameAbbreviation = c.NameAbbreviation,
                Email = c.Email,
                Phone = c.Phone
            };
            return model;
        }
    }
}
