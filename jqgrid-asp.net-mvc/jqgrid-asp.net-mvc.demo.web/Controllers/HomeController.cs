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

        #region jqgrid read and search

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

        #endregion

        #region jqgrid add, edit, delete

        public ActionResult UpdateForJqGrid(Person person, string oper)
        {
            return JqGrid.UpdateForJqGrid(person, oper, AddViaJqGrid, EditViaJqGrid, DelViaJqGird);
        }

        private ActionResult DelViaJqGird(Person person)
        {
            var deletepersonentity = db.Persons.Single(p => p.ID == person.ID);
            db.Persons.Remove(deletepersonentity);

            db.SaveChanges();

            return Content("Delete success");
        }

        private ActionResult EditViaJqGrid(Person person)
        {
            var editpersonentity = db.Persons.Single(p => p.ID == person.ID);
            editpersonentity.FirstName = person.FirstName;
            editpersonentity.LastName = person.LastName;
            editpersonentity.City = person.City;
            editpersonentity.Zip = person.Zip;

            db.SaveChanges();

            return Content("Update success");

        }

        private ActionResult AddViaJqGrid(Person person)
        {
            var newpersonentity = new Person();
            newpersonentity.FirstName = person.FirstName;
            newpersonentity.LastName = person.LastName;
            newpersonentity.City = person.City;
            newpersonentity.Zip = person.Zip;

            db.Persons.Add(newpersonentity);
            db.SaveChanges();

            return Content("Add success");
        }

        #endregion
    }
}