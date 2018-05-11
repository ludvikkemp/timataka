using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timataka.Core.Data.Repositories;
using Timataka.Core.Models.Dto.CompetitionInstanceDTO;
using Timataka.Core.Models.Entities;
using Timataka.Core.Models.ViewModels.CompetitionViewModels;
using Timataka.Core.Models.ViewModels.ContestantViewModels;
using Timataka.Core.Models.ViewModels.UserViewModels;

namespace Timataka.Core.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly ICompetitionRepository _repo;

        public CompetitionService(ICompetitionRepository repo)
        {
            _repo = repo;
        }

        public CompetitionService()
        {
            //For unit tests
        }

        public EditContestantChipHeatResultDto GetEditContestantChipHeatResultDtoFor(string userId, int eventId, int competitionInstanceId)
        {
            return _repo.GetEditContestantChipHeatResultDtoFor(userId, eventId, competitionInstanceId);
        }

        #region Competition

        /// <summary>
        /// Function to add new competition.
        /// </summary>
        /// <param name="c">CompetitionViewModel</param>
        /// <returns>Returns the newly created competition</returns>
        public async Task<Competition> AddAsync(CompetitionsViewModel c)
        {
            //TODO: Check if competition exists
            var newComp = new Competition
            {
                Name = c.Name,
                WebPage = c.WebPage,
                Email = c.Email,
                Phone = c.PhoneNumber,
                Description = c.Description,
                Sponsor = c.Sponsor,
                Deleted = false
            };
            await _repo.InsertAsync(newComp);
            return newComp;
        }


        /// <summary>
        /// Function to edit a competition
        /// </summary>
        /// <param name="c"></param>
        /// <param name="m"></param>
        /// <returns>Edited competition</returns>
        public async Task<Competition> EditAsync(Competition c, CompetitionsViewModel m)
        {
            c.Description = m.Description;
            c.Email = m.Email;
            c.Name = m.Name;
            c.Phone = m.PhoneNumber;
            c.Sponsor = m.Sponsor;
            c.WebPage = m.WebPage;
            await _repo.EditAsync(c);
            return c;
        }

        /// <summary>
        /// Function to get list of all compeitions
        /// </summary>
        /// <returns>List of all competitions</returns>
        public IEnumerable<Competition> GetAllCompetitions()
        {
            var competitions = (from c in _repo.Get()
                                orderby c.Name
                                select c);
            return competitions;
        }

        /// <summary>
        /// Function to get a competition by its ID
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns>Competition with the given ID if it exists</returns>
        public async Task<Competition> GetCompetitionByIdAsync(int competitionId)
        {
            //TODO: Check if competition exists
            var c = await _repo.GetByIdAsync(competitionId);
            return c;
        }

        /// <summary>
        /// Function to get competition by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>CompetitionViewModel for the given competition</returns>
        public CompetitionsViewModel GetCompetitionViewModelById(int id)
        {
            //TODO: Check if competition exists
            return _repo.GetCompetitionById(id);
        }

        /// <summary>
        /// Function to check if a given cmopetition exists.
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns>True if the competition exists, false otherwise.</returns>
        public async Task<bool> CompetitionExistsAsync(string modelName)
        {
            var result = await _repo.GetCompetitionByNameAsync(modelName);
            if (result == null) return false;
            return true;
        }

        /// <summary>
        /// Function to remove a given competition.
        /// </summary>
        /// <param name="competitionId"></param>
        /// <returns>The ID of the competition removed.</returns>
        public async Task<int> RemoveAsync(int competitionId)
        {
            //TODO: return true/false?
            var c = await GetCompetitionByIdAsync(competitionId);
            await _repo.RemoveAsync(c);
            return competitionId;
        }

        #endregion

        #region Competition Instance
        /// <summary>
        /// Function to add an instance of a competition.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The created instance</returns>
        public async Task<CompetitionInstance> AddInstanceAsync(CompetitionsInstanceViewModel model)
        {
            var newInstance = new CompetitionInstance
            {
                CompetitionId = model.CompetitionId,
                DateFrom = model.DateFrom,
                DateTo = model.DateTo,
                Location = model.Location,
                CountryId = model.CountryId,
                Name = model.Name,
                Status = model.Status,
                Deleted = false
            };

            await _repo.InsertInstanceAsync(newInstance);
            return newInstance;
        }

        /// <summary>
        /// Function to edit an instance of a competition.
        /// </summary>
        /// <param name="compInstance"></param>
        /// <param name="model"></param>
        /// <returns>Edited instance</returns>
        public async Task<CompetitionInstance> EditInstanceAsync(CompetitionInstance compInstance, CompetitionsInstanceViewModel model)
        {
            compInstance.CountryId = model.CountryId;
            compInstance.DateFrom = model.DateFrom;
            compInstance.DateTo = model.DateTo;
            compInstance.Location = model.Location;
            compInstance.Name = model.Name;
            compInstance.Status = model.Status;
            await _repo.EditInstanceAsync(compInstance);
            return compInstance;
        }

        /// <summary>
        /// Function to remove an instance of a competition.
        /// </summary>
        /// <param name="competitionInstanceId"></param>
        /// <returns>The ID of the instance removed.</returns>
        public async Task<int> RemoveInstanceAsync(int competitionInstanceId)
        {
            var c = await GetCompetitionInstanceByIdAsync(competitionInstanceId);
            await _repo.RemoveInstanceAsync(c);
            return competitionInstanceId;
        }

        /// <summary>
        /// Function to get all competition instances.
        /// </summary>
        /// <returns>All instances.</returns>
        public IEnumerable<CompetitionInstance> GetAllCompetitionInstances(Status? status = null)
        {
            var instances = _repo.GetInstances();
            if(status != null)
            {
                instances = (from i in instances
                             where i.Status == status
                             select i).OrderBy(o => o.DateFrom).ToList();
            }
            return instances;
        }

        /// <summary>
        /// Function to get all instances of a given competition.
        /// </summary>
        /// <param name="id">ID of the competition</param>
        /// <returns>List of instances for a given competition.</returns>
        public IEnumerable<CompetitionInstance> GetAllInstancesOfCompetition(int id)
        {
            var instances = _repo.GetInstancesForCompetition(id).OrderByDescending(o => o.DateFrom);
            return instances;
        }

        /// <summary>
        /// Function to get an instance of a competition by ID.
        /// </summary>
        /// <param name="id">ID of the instance.</param>
        /// <returns>Instance of a competition.</returns>
        public async Task<CompetitionInstance> GetCompetitionInstanceByIdAsync(int id)
        {
            return await _repo.GetInstanceByIdAsync(id);
        }

        /// <summary>
        /// Function to get an instance of a competition by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ViewModel for the competition instance.</returns>
        public CompetitionsInstanceViewModel GetCompetitionInstanceViewModelById(int id)
        {
            return _repo.GetCompetitionInstanceById(id);
        }

        /// <summary>
        /// Function to get all contestatns in competition instance.
        /// </summary>
        /// <param name="id">Competition instance ID</param>
        /// <returns></returns>
        public IEnumerable<ContestantsInCompetitionViewModel> GetContestantsInCompetitionInstance(int id)
        {
            return _repo.GetAllContestantsInCompetitionInstance(id);
        }

        /// <summary>
        /// Function to get list of all contestants in competition instance.
        /// </summary>
        /// <param name="competitionInstanceId"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public IEnumerable<ContestantsInCompetitionViewModel> GetContestantsInCompetitionInstanceAndEvent(int competitionInstanceId, int eventId)
        {
            var filteredContestants = (from c in _repo.GetAllContestantsInCompetitionInstance(competitionInstanceId)
            where ((from e in c.EventList
                          where e.Id == eventId
                          select e).ToList().Count()) == 1
                select c).ToList();
            return filteredContestants;
        }

        /// <summary>
        /// Function to get heats for contestant in competition instance.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="competitionInstanceId"></param>
        /// <returns></returns>
        public IEnumerable<Heat> GetHeatsForUserInCompetition(string userId, int competitionInstanceId)
        {
            return _repo.GetHeatsForContestantInCompetitioninstance(userId, competitionInstanceId);
        }

        /// <summary>
        /// Get latest results for all competitions in the sport specified
        /// </summary>
        /// <param name="sportId" topFive="topFive"></param>
        /// <returns></returns>
        public IEnumerable<LatestResultsDTO> GetLatestResults(int? sportId, bool topFive)
        {
            IEnumerable<LatestResultsDTO> result;
            if (sportId == 0) // Other sports than T&F and Cycling
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Ongoing || i.Status == Status.Finished || i.Status == Status.Closed
                    where i.Deleted == false
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                        where e.SportId != 1 && e.SportId != 2
                        select e).Any()
                    orderby i.DateFrom descending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status)
                    }).ToList();
            }
            else if(sportId == null)
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Ongoing || i.Status == Status.Finished || i.Status == Status.Closed
                    where i.Deleted == false
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                           select e).Any()
                    orderby i.DateFrom descending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status),
                        NumberOfContestants = GetNumberOfContestantsInInstance(i.Id)
                    }).ToList();
            }
            else
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Ongoing || i.Status == Status.Finished || i.Status == Status.Closed
                    where i.Deleted == false
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                        where e.SportId == sportId
                        select e).Any()
                    orderby i.DateFrom descending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status),
                        NumberOfContestants = GetNumberOfContestantsInInstance(i.Id)
                    }).ToList();
            }

            return topFive ? result.Take(5) : result;
        }

        public Boolean IsLive(Status s)
        {
            if (s == Status.Ongoing)
                return true;
            return false;
        }

        public IEnumerable<LatestResultsDTO> GetUpcomingEvents(int? sportId, bool topFive)
        {
            IEnumerable<LatestResultsDTO> result;
            if (sportId == 0)
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Pending || i.Status == Status.OpenForRegistration
                    where i.Deleted == false
                    where i.DateFrom.Date >= DateTime.Now.Date
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                        where e.SportId != 1 && e.SportId != 2
                           select e).Any()
                    orderby i.DateFrom ascending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status),
                        NumberOfContestants = GetNumberOfContestantsInInstance(i.Id)
                    }).ToList();
            }
            else if(sportId == null)
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Pending || i.Status == Status.OpenForRegistration
                    where i.Deleted == false
                    where i.DateFrom.Date >= DateTime.Now.Date
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                           select e).Any()
                    orderby i.DateFrom ascending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status),
                        NumberOfContestants = GetNumberOfContestantsInInstance(i.Id)
                    }).ToList();
            }
            else
            {
                result = (
                    from i in _repo.GetInstances()
                    where i.Status == Status.Pending || i.Status == Status.OpenForRegistration
                    where i.Deleted == false
                    where i.DateFrom.Date >= DateTime.Now.Date
                    join c in _repo.Get() on i.CompetitionId equals c.Id
                    where (from e in _repo.GetEventsForInstance(i.Id)
                           where e.SportId == sportId
                           select e).Any()
                    orderby i.DateFrom ascending
                    select new LatestResultsDTO
                    {
                        CompetitionInstanceId = i.Id,
                        CompetitionId = c.Id,
                        Date = i.DateFrom,
                        CompetitionName = c.Name,
                        CompetitionInstanceName = i.Name,
                        Live = IsLive(i.Status),
                        NumberOfContestants = GetNumberOfContestantsInInstance(i.Id)
                    }).ToList();
            }

            return topFive ? result.Take(5) : result;
        }

        public IEnumerable<Heat> GetHeatsInCompetitionInstance(int competitionInstanceId)
        {
            return _repo.GetHeatsInCompetitionInstance(competitionInstanceId);
        }

        public IEnumerable<MyCompetitionsViewModel> GetAllCompetitionInstancesForUser(string userId)
        {
            return _repo.GetAllCompetitionInstancesForUser(userId);
        }

        public List<AddContestantViewModel> GetAddContestantViewModelByCompetitionInstanceId(int competitionInstanceId, string userId)
        {
            return _repo.GetAddContestantViewModelByCompetitionInstanceId(competitionInstanceId, userId);
        }

        public int GetNumberOfContestantsInInstance(int id)
        {
            return _repo.GetNumberOfContestantsInInstance(id);
        }

        #endregion

        #region User roles for competitions

        /// <summary>
        /// Function to add role to a user so the user can manage competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Newly created role</returns>
        public async Task<ManagesCompetition> AddRole(ManagesCompetition m)
        {
            //TODO: Check if role exists
            await _repo.AddRoleAsync(m);
            return m;
        }

        /// <summary>
        /// Function to edit user role in competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns>Edited role</returns>
        public async Task<ManagesCompetition> EditRole(ManagesCompetition m)
        {
            await _repo.EditRoleAsync(m);
            return m;
        }

        /// <summary>
        /// Function to remove user role in competition
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public async Task RemoveRole(ManagesCompetition m)
        {
            //TODO: Return something to confirm
            await _repo.RemoveRoleAsync(m);
            return;
        }

        /// <summary>
        /// Function to get all roles for all competitions
        /// </summary>
        /// <returns>List of all roles</returns>
        public IEnumerable<ManagesCompetition> GetAllRoles()
        {
            var m = _repo.GetAllRoles();
            return m;
        }
        
        /// <summary>
        /// Function to get all roles for a given competiton
        /// </summary>
        /// <param name="id"></param>
        /// <returns>All roles for a given competitions</returns>
        public IEnumerable<ManagesCompetitionViewModel> GetAllRolesForCompetition(int id)
        {
            var m = _repo.GetRolesForCompetition(id);
            return m;
        }

        /// <summary>
        /// Function to get all roles for a given user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of roles in competitions for a given user</returns>
        public IEnumerable<ManagesCompetition> GetAllRolesForUser(string id)
        {
            var m = _repo.GetRolesForUser(id);
            return m;
        }

        /// <summary>
        /// Function to get the role af a user in competition
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="competitionId"></param>
        /// <returns>Role of the user in given competition</returns>
        public Role GetRole(string userId, int competitionId)
        {
            IEnumerable<ManagesCompetition> m = _repo.GetRolesForUser(userId);
            ManagesCompetition r = (from x in m
                                    where x.CompetitionId.Equals(competitionId)
                                    select x).SingleOrDefault();
            return r.Role;
        }



        #endregion

    }
}
