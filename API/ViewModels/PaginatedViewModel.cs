using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class PaginatedViewModel<T>
    {
        public int page { get; set; }
        public int pagesize { get; set; }
        public int total { get; set; }
        public List<T> results { get; set; }
    }
}