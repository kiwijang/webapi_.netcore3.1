using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Models
{
    public class DataModel
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
        public object Items { get; set; }
    }
}
