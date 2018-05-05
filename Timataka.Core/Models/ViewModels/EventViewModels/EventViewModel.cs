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
        public int SportId { get; set; }
        public int CompetitionInstanceId { get; set; }
        [Required]
        [Display(Name = "Date From")]
        public DateTime DateFrom { get; set; }
        [Required]
        [Display(Name = "Date To")]
        public DateTime DateTo { get; set; }
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        [Display(Description = "Number of laps in the race, use 1 if no laps")]
        [Range(1,int.MaxValue, ErrorMessage ="Must have a positive number of laps")]
        public int Laps { get; set; }
        [Display(Description ="Number of splits per lap")]
        [Range(1, int.MaxValue, ErrorMessage = "Must have at least one split")]
        public int Splits { get; set; }
        [Display(Name = "Distance offset (m)")]
        public int DistanceOffset { get; set; }
        [Display(Name = "Start Interval (s)")]
        [Range(0, int.MaxValue, ErrorMessage = "Start interval cannot have negative value")]
        public int StartInterval { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Active Chip")]
        public bool ActiveChip { get; set; }
        public string DisciplineName { get; set; }
        public Boolean Deleted { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int NumberOfContestants { get; set; }
    }
}
