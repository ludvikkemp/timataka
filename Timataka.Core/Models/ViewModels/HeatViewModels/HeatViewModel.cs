using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.HeatViewModels
{
    public class HeatViewModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int HeatNumber { get; set; }
        public Boolean Deleted { get; set; }
    }
}
