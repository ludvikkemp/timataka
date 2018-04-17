using System.Collections.Generic;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.ViewModels.HomeViewModels;

namespace Timataka.Core.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _repo;

        public ResultService(IResultRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Function to get all results for given event.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>List of all results</returns>
        public IEnumerable<ResultViewModel> GetResultViewModelsForEvent(int eventId)
        {
            return _repo.GetResultViewModelsForEvent(eventId);
        }
        
        
    }
}
