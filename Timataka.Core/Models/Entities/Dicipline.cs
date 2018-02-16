using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    class Dicipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Sport")]
        public int SportId { get; set; }
    }
}
