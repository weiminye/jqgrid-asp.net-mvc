using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using jqgrid_asp.net_mvc.demo.web.Models;
using jqgrid_asp.net_mvc.demo.web.Models.Helpers;

namespace jqgrid_asp.net_mvc.demo.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private CRUDDemoDBContext db = new CRUDDemoDBContext();

        #region for jqgrid

        public ActionResult IndexJsonList(bool _search, string nd, int? rows, int? page, string sidx, string sord, jqgrid_asp.net_mvc.demo.web.Models.Grid.Filter filters)
        {
            IQueryable<Person> where_predicate = null;
            if (_search)
            {
                if (filters == null) filters = jqgrid_asp.net_mvc.demo.web.Models.Grid.Filter.Create(Request["filters"] ?? "");

                if (filters == null) throw new NullReferenceException("flters is null, load mvc parse is error");

                where_predicate = db.Persons;
                //And
                if (filters.groupOp == "AND")
                    foreach (var rule in filters.rules)
                    {
                        if (rule.op == "true")
                        {
                            where_predicate = where_predicate.Where<Person>(
                                rule.field, rule.data,
                                WhereOperation.Contains);
                        }
                        else
                        {
                            where_predicate = where_predicate.Where<Person>(
                                rule.field, rule.data,
                                (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));
                        }
                    }
                else
                {
                    //Or
                    var temp = (new List<Person>()).AsQueryable();
                    foreach (var rule in filters.rules)
                    {
                        var t = where_predicate.Where<Person>(
                        rule.field, rule.data,
                        (WhereOperation)StringEnum.Parse(typeof(WhereOperation), rule.op));

                        if (rule.op == "true")
                        {
                            t = where_predicate.Where<Person>(
                                                    rule.field, rule.data,
                                                    WhereOperation.Contains);
                        }

                        temp = temp.Concat<Person>(t);

                    }
                    //remove repeating records
                    where_predicate = temp.Distinct<Person>();
                }

            }
            else
            {
                where_predicate = null;
            }

            return JqGrid.Load(db.Persons,
                d => d.FirstName,
                s => new
                {
                    //PersonID = s.ID,
                    s.ID,
                    s.FirstName,
                    s.LastName,
                    s.City,
                    s.Zip,

                }, rows, page, where_predicate);

        }

        public ActionResult UpdateForJqGrid(Person person, string oper)
        {
            //throw new NotImplementedException();

            switch (oper)
            {
                case "add":

                    var newpersonentity = new Person();
                    newpersonentity.FirstName = person.FirstName;
                    newpersonentity.LastName = person.LastName;
                    newpersonentity.City = person.City;
                    newpersonentity.Zip = person.Zip;

                    db.Persons.Add(newpersonentity);
                    db.SaveChanges();

                    return Content("Add success");

                case "edit":
                    //throw new NotImplementedException();

                    //var guidid = Guid.Parse(id);

                    var editpersonentity = db.Persons.Single(p => p.ID == person.ID);
                    editpersonentity.FirstName = person.FirstName;
                    editpersonentity.LastName = person.LastName;
                    editpersonentity.City = person.City;
                    editpersonentity.Zip = person.Zip;

                    db.SaveChanges();

                    return Content("Update success");

                case "del":
                    //throw new NotImplementedException();
                    //var ids = new List<string>();
                    //if (idstr.IsNotNullOrEmpty())
                    //{
                    //    var idstrarray = idstr.Split(',');
                    //    foreach (var id in idstrarray)
                    //    {
                    //        ids.Add(id);
                    //    }
                    //}

                    ////db.Projects
                    //foreach (var id in ids)
                    //{
                    //    var guidid = Guid.Parse(id);
                    //    var entity = dbset.Single(p => p.ID == guidid);

                    //    dbset.Remove(entity);
                    //}
                    var deletepersonentity = db.Persons.Single(p => p.ID == person.ID);
                    db.Persons.Remove(deletepersonentity);

                    db.SaveChanges();


                    return Content("Delete success");

                default:
                    throw new ArgumentOutOfRangeException("oper value is " + oper);
            }

            throw new ArgumentOutOfRangeException("oper value is " + oper);
        }

        #endregion


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}