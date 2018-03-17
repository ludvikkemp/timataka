using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Timataka.Core.Models.Entities
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Lap { get; set; }
        public int Distance { get; set; }
        public int DisciplineId { get; set; }
        [ForeignKey(nameof(DisciplineId))]
        public virtual Discipline Discipline { get; set; }
        public string ExternalCourseId { get; set; }
    }
}
