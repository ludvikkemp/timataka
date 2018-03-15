using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Competition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WebPage { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Sponsor { get; set; }
        public Boolean Deleted { get; set; }
    }
}
