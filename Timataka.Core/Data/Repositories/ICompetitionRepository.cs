using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Repositories
{
    public interface ICompetitionRepository : IDisposable
    {
        void Insert(Competition c);
        Task InsertAsync(Competition c);

        IEnumerable<Competition> Get();

        Competition GetById(int Id);
        Task<Competition> GetByIdAsync(int Id);

        void Edit(Competition c);
        Task EditAsync(Competition c);

        void Remove(Competition c);
        Task RemoveAsync(Competition c);

        Task<Competition> GetCompetitionByNameAsync(string cName);

        //CompetitionInstance

        void InsertInstance(CompetitionInstance c);
        Task InsertInstanceAsync(CompetitionInstance c);

        IEnumerable<CompetitionInstance> GetInstances();
        IEnumerable<CompetitionInstance> GetInstancesForCompetition(int Id);

        CompetitionInstance GetInstanceById(int Id);
        Task<CompetitionInstance> GetInstanceByIdAsync(int Id);

        void EditInstance(CompetitionInstance c);
        Task EditInstanceAsync(CompetitionInstance c);

        void RemoveInstance(CompetitionInstance c);
        Task RemoveInstanceAsync(CompetitionInstance c);

        //ManagesCompetition

        void AddRole(ManagesCompetition m);
        Task AddRoleAsync(ManagesCompetition m);

        void EditRole(ManagesCompetition m);
        Task EditRoleAsync(ManagesCompetition m);

        void RemoveRole(ManagesCompetition m);
        Task RemoveRoleAsync(ManagesCompetition m);

        IEnumerable<ManagesCompetition> GetAllRoles();
        IEnumerable<ManagesCompetition> GetRolesForCompetition(int Id);
        IEnumerable<ManagesCompetition> GetRolesForUser(int Id);

    }
}
