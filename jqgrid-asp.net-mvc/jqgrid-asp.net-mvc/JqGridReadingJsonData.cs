using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqgrid_asp.net_mvc
{
    public class JqGridReadingJsonData<TSource, TResult> where TSource : class
    {
        public int total { get; set; }

        public int page { get; set; }
        public int records { get; set; }
        public TResult[] rows { get; set; }

    }
}
