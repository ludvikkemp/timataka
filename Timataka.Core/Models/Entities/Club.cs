using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAbbreviation { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Webpage { get; set; }
        public bool Deleted { get; set; }
    }
}