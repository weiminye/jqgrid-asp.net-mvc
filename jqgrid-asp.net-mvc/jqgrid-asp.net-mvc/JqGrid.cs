using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using jqgrid_asp.net_mvc.Helpers;

namespace jqgrid_asp.net_mvc
{
    public class JqGrid
    {
        public static ActionResult UpdateForJqGrid<TSource>(
                TSource t,
                string oper,
                Func<TSource, ActionResult> addentry,
                Func<TSource, ActionResult> editentry,
                Func<TSource, ActionResult> delentry
                )
                where TSource : class
        {
            switch (oper)
            {
                case "add": return addentry(t);
                case "edit": return editentry(t);
                case "del": return delentry(t);
                default:
                    throw new ArgumentOutOfRangeException("oper value is " + oper);
            }

            throw new ArgumentOutOfRangeException("oper value is " + oper);
        }


        //public static ActionResult Load<TSource, TResult, TKey>(DbSet<TSource> dbset, Func<TSource, TKey> orderquery, Func<TSource, TResult> selector, JqGridQueryParameters jqgridqueryparameter, bool orderbydescending = true) where TSource : class
        //{
        //    return Load(dbset, orderquery, selector, jqgridqueryparameter.rows, jqgridqueryparameter.page, jqgridqueryparameter._search,ref jqgridqueryparameter.filters);
        //}

        public static ActionResult Load<TSource, TResult, TKey>(DbSet<TSource> dbset, Func<TSource, TKey> orderquery, Func<TSource, TResult> selector, int? rows, int? page, bool _search, ref jqgrid_asp.net_mvc.Filter filters, bool orderbydescending = true) where TSource : class
        {
            IQueryable<TSource> where_predicate = JqGrid.GenerateWherePredicate(dbset, _search, ref filters);

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

            var jsonData = new JqGridReadingJsonData<TSource, TResult>()
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

        private static IQueryable<TSource> GenerateWherePredicate<TSource>(DbSet<TSource> dbset, bool _search, ref jqgrid_asp.net_mvc.Filter filters)
            where TSource : class
        {
            var request = HttpContext.Current.Request;
            IQueryable<TSource> where_predicate = null;
            if (_search)
            {
                if (filters == null) filters = jqgrid_asp.net_mvc.Filter.Create(request["filters"] ?? "");

                if (filters == null) throw new NullReferenceException("flters is null, load mvc parse is error");

                where_predicate = dbset;
                //And
                if (filters.groupOp == "AND")
                    foreach (var rule in filters.rules)
                    {
                        if (rule.op == "true")
                        {
                            where_predicate = where_predicate.Where<TSource>(
                                rule.field, rule.data,
                                WhereOperation.Contains);
                        }
                        else
                        {
                            where_predicate = where_predicate.Where<TSource>(
                                rule.field, rule.data,
                                (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        }
                    }
                else
                {
                    //Or
                    var temp = (new List<TSource>()).AsQueryable();
                    foreach (var rule in filters.rules)
                    {
                        var t = where_predicate.Where<TSource>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));

                        if (rule.op == "true")
                        {
                            t = where_predicate.Where<TSource>(
                                                    rule.field, rule.data,
                                                    WhereOperation.Contains);
                        }

                        temp = temp.Concat<TSource>(t);

                    }
                    //remove repeating records
                    where_predicate = temp.Distinct<TSource>();
                }

            }
            else
            {
                where_predicate = null;
            }
            return where_predicate;
        }
    }


}
