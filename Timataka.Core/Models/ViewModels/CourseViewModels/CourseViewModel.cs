using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Timataka.Core.Models.ViewModels.CourseViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Boolean Lap { get; set; }
        [Display(Name = "Distance (m)")]
        [Range(0, int.MaxValue, ErrorMessage = "Distance must be a positive number")]
        public int Distance { get; set; }
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }
        public string DisciplineName { get; set; }
        [Display(Name = "External Course Id")]
        public string ExternalCourseId { get; set; }
        public bool Deleted { get; set; }
    }
}
