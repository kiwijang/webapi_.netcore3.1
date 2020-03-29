using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessageBoard.API.Models
{
    public class PageModel
    {
        public string CurrentPage { get; set; } = "1";
        public string PageSize { get; set; }
    }
}
