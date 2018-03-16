using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.AdminDTO
{
    public class CompetitionDto
    {
        public Competition Competiton { get; set; }
        public IEnumerable<CompetitionInstance> Instances { get; set; }
    }
}
