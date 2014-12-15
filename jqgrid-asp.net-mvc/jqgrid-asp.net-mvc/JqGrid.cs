using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace jqgrid_asp.net_mvc
{
    public class JqGrid
    {
        public static ActionResult Load<TSource, TResult, TKey>(DbSet<TSource> dbset, Func<TSource, TKey> orderquery, Func<TSource, TResult> selector, int? rows, int? page, IQueryable<TSource> where_predicate = null, bool orderbydescending = true) where TSource : class
        {
            TSource[] cachecresult = null;

            if (where_predicate != null)
            {
                if (orderbydescending)
                {
                    cachecresult = where_predicate
                        .OrderByDescending(orderquery)
                        //.Include("Roles")
                        .ToArray();
                }
                else
                {
                    cachecresult = where_predicate
                        .OrderBy(orderquery)
                        //.Include("Roles")
                        .ToArray();
                }
            }
            else
            {
                if (orderbydescending)
                {
                    cachecresult = dbset
                        .OrderByDescending(orderquery)
                        //.Include("Roles")
                        .ToArray();
                }
                else
                {
                    cachecresult = dbset
                        .OrderBy(orderquery)
                        //.Include("Roles")
                        .ToArray();
                }

            }

            var result = cachecresult.Select(selector).ToArray();

            var pageSize = rows ?? 10;
            var pageNum = page ?? 1;
            var totalRecords = result.Length;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = pageNum,
                records = totalRecords,
                rows = result.Skip((pageNum - 1) * pageSize).Take(pageSize).ToArray()
            };

            //return Json(jsonData, JsonRequestBehavior.AllowGet);
            var returnjson = new JsonResult();
            returnjson.Data = jsonData;
            returnjson.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return returnjson;
        }
    }

}
