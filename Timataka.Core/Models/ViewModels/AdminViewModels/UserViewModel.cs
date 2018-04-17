using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.ViewModels.AdminViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Ssn { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public bool Deleted { get; set; }
        public string Role { get; set; }
        public bool SuperAdmin { get; set; }
        public int Country { get; set; }
        public int Nationality { get; set; }
    }
}
