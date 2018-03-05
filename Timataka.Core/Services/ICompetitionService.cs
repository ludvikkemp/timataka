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
    }
}
