using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.EventViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Discipline")]
        public int DisciplineId { get; set; }
        public int CompetitionInstanceId { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Laps { get; set; }
        public int Splits { get; set; }
        public int DistanceOffset { get; set; }
        public int StartInterval { get; set; }
        public Gender Gender { get; set; }
        public bool ActiveChip { get; set; }
        public string DisciplineName { get; set; }
        public Boolean Deleted { get; set; }
    }
}
