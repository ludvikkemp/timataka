using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Timataka.Core.Data.Repositories;

namespace Timataka.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }

        // Returns a list of nations sorted by name in a dropdown list
        public List<SelectListItem> GetNationsListItems()
        {
            List<SelectListItem> selectNationsListItems =
                new List<SelectListItem>();

            var listOfNations = _repo.GetNations();

            selectNationsListItems.Add(
                new SelectListItem
                {
                    Text = "Iceland",
                    Value = "352"
                });

            foreach (var item in listOfNations)
            {
                if (item.Id != 352)
                {
                    selectNationsListItems.Add(
                        new SelectListItem
                        {
                            Text = item.Name,
                            Value = item.Id.ToString()
                        });
                }
                
            }

            return selectNationsListItems;
        }
    }
}
