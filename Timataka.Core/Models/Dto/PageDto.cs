using System;
using System.Collections.Generic;
using System.Text;

namespace Timataka.Core.Models.Dto
{
    public class PageDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalPages { get; set; }
    }
}
