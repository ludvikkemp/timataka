using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Timataka.Core.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public string Nationality { get; set; }
        public string PhoneExtension { get; set; }
    }
}
