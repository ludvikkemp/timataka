﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Models.ViewModels.CategoryViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Event")]
        public int EventId { get; set; }
        public string EventName { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        [Display(Name = "Age From")]
        [Range(0, 1000)]
        public int AgeFrom { get; set; }
        [Display(Name = "Age To")]
        [Range(0, 1000)]
        public int AgeTo { get; set; }
        public Gender Gender { get; set; }
    }
}
