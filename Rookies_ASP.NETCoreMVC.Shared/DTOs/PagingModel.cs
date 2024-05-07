using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rookies_ASP.NETCoreMVC.Shared.DTOs
{
    public class PagingModel
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int Size { get; set; }
    }
}
