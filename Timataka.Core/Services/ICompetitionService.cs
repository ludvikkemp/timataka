using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;

namespace Timataka.Core.Services
{
    public interface ICompetitionService
    {
        Task<Competition> Add(CompetitionsViewModel c);
        Task<Competition> Edit(Competition c);
        Task<int> Remove(int competitionId);
        IEnumerable<Competition> GetAllCompetitions();
        Task<Competition> GetCompetitionById(int competitionId);

        //CompetitionInstance
        Task<CompetitionInstance> AddInstance(CompetitionsInstanceViewModel model);
        Task<CompetitionInstance> EditInstance(CompetitionInstance c, CompetitionsInstanceViewModel model);
        Task<int> RemoveInstance(int competitionInstanceId);
        IEnumerable<CompetitionInstance> GetAllCompetitionInstances();
        IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int id);
        Task<CompetitionInstance> GetCompetitionInstanceById(int id);
        CompetitionsInstanceViewModel GetCompetitionInstanceViewModelById(int id);

        //ManagesCompetition
        Task<ManagesCompetition> AddRole(ManagesCompetition m);
        Task<ManagesCompetition> EditRole(ManagesCompetition m);
        Task RemoveRole(ManagesCompetition m);
        IEnumerable<ManagesCompetition> GetAllRoles();
        IEnumerable<ManagesCompetition> GetAllRolesForCompetition(int Id);
        IEnumerable<ManagesCompetition> GetAllRolesForUser(string Id);
        Role GetRole(string UserId, int CompetitionId);


        
    }
}
