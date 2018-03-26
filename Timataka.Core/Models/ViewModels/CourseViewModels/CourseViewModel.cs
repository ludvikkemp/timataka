using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.CourseViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Lap { get; set; }
        public int Distance { get; set; }
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        public string ExternalCourseId { get; set; }
    }
}
