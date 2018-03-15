using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Services
{
    public interface ICompetitionService
    {
        Task<Competition> Add(Competition c);
        Task<Competition> Edit(Competition c);
        Task<int> Remove(int CompetitionId);
        IEnumerable<Competition> GetAllCompetitions();
        Task<Competition> GetCompetitionById(int CompetitionId);
        Task<Competition> GetCompetitionByName(string Name);

        //CompetitionInstance
        Task<CompetitionInstance> AddInstance(CompetitionInstance c);
        Task<CompetitionInstance> EditInstance(CompetitionInstance c);
        Task<int> RemoveInstance(int CompetitionInstanceId);
        IEnumerable<CompetitionInstance> GetAllCompetitionInstances();
        IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int Id);
        Task<CompetitionInstance> GetCompetitionInstanceById(int Id);

    }
}
