using System;
using System.Collections.Generic;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.Dto.UserDTO
{
    public class RegisterToEventDto
    {
        public string UserId { get; set; }
        public Event Event { get; set; }
        public CompetitionInstance Instance { get; set; }
    }
}
