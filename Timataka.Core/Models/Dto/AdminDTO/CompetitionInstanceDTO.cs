using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class CompetitionInstanceDTO
    {
        public CompetitionInstance CompetitonInstance { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
