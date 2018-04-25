using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Timataka.Core.Services
{
    public interface IAccountService
    {
        List<SelectListItem> GetNationsListItems();
        List<SelectListItem> GetNationalityListItems();
    }
}
