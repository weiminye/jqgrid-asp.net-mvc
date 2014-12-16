using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqgrid_asp.net_mvc
{
    public class JqGridQueryParameters
    {
        //DONOT use properties to instead of below ones because it will cause bug.
        //DONOT upper the below ones  because it will cause bug.
        public bool _search;
        public string nd;
        public int? rows;
        public int? page;
        public string sidx;
        public string sord;
        //public Filter filters;
    }
}
