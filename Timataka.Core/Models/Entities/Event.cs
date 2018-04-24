using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public enum Gender { Female = 0, Male = 1, All = 2 }
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int DisciplineId { get; set; }
        [ForeignKey(nameof(DisciplineId))]
        public virtual Discipline Discipline{ get; set; }

        public int CompetitionInstanceId { get; set; }
        [ForeignKey(nameof(CompetitionInstanceId))]
        public virtual CompetitionInstance CompInstanceId { get; set; }

        public int CourseId { get; set; }
        [ForeignKey(nameof(CourseId))]
        public virtual Course Course { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        //Number of laps, 0 if no laps
        public int Laps { get; set; }
        //Number of splits per lap
        public int Splits { get; set; }
        //Used to offset total distance for different start/finish
        //locations on a circuit
        public int DistanceOffset { get; set; }
        //Time between starts
        public int StartInterval { get; set; }
        public Gender Gender { get; set; }
        //Set to 1 for active chips and 0 for passive chips
        public Boolean ActiveChip { get; set; }
        public Boolean Deleted { get; set; }
    }
}
