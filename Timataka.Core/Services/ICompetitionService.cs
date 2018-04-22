using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Services
{
    public interface ICompetitionService
    {
        Task<Competition> AddAsync(CompetitionsViewModel c);
        Task<Competition> EditAsync(Competition c, CompetitionsViewModel m);
        Task<int> RemoveAsync(int competitionId);
        IEnumerable<Competition> GetAllCompetitions();
        Task<Competition> GetCompetitionByIdAsync(int competitionId);
        CompetitionsViewModel GetCompetitionViewModelById(int id);
        Task<bool> CompetitionExistsAsync(string modelName);

        //CompetitionInstance
        Task<CompetitionInstance> AddInstanceAsync(CompetitionsInstanceViewModel model);
        Task<CompetitionInstance> EditInstanceAsync(CompetitionInstance c, CompetitionsInstanceViewModel model);
        Task<int> RemoveInstanceAsync(int competitionInstanceId);
        IEnumerable<CompetitionInstance> GetAllCompetitionInstances();
        IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int id);
        Task<CompetitionInstance> GetCompetitionInstanceByIdAsync(int id);
        CompetitionsInstanceViewModel GetCompetitionInstanceViewModelById(int id);
        IEnumerable<ContestantsInCompetitionViewModel> GetContestantsInCompetitionInstance(int id);
        IEnumerable<Heat> GetHeatsForUserInCompetition(string userId, int competitionInstanceId);
        EditContestantChipHeatResultDto GetEditContestantChipHeatResultDtoFor(string userId, int eventId, int competitionInstanceId);
        IEnumerable<LatestResultsDTO> GetLatestResults(int sportId);
        IEnumerable<LatestResultsDTO> GetUpcomingEvents(int sportId);

        //ManagesCompetition
        Task<ManagesCompetition> AddRole(ManagesCompetition m);
        Task<ManagesCompetition> EditRole(ManagesCompetition m);
        Task RemoveRole(ManagesCompetition m);
        IEnumerable<ManagesCompetition> GetAllRoles();
        IEnumerable<ManagesCompetitionViewModel> GetAllRolesForCompetition(int id);
        IEnumerable<ManagesCompetition> GetAllRolesForUser(string id);
        Role GetRole(string userId, int competitionId);
    }
}
