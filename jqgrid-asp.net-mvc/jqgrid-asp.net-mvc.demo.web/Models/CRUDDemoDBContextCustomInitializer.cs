using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqgrid_asp.net_mvc.demo.web.Models
{
    public class CRUDDemoDBContextCustomInitializer : IDatabaseInitializer<CRUDDemoDBContext>
    {
        public void InitializeDatabase(CRUDDemoDBContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                    context.Database.Create();
                }
            }
            else
            {
                context.Database.Create();
            }

            if (context.Persons.SingleOrDefault(c => c.City == "San Francisco" && c.FirstName == "Weimin" && c.LastName == "Ye") == null)
            {
                context.Persons.Add(new Person() { City = "San Francisco", FirstName = "Weimin", LastName = "Ye", Zip = "94112" });
                context.Persons.Add(new Person() { City = "San Jose", FirstName = "Eric", LastName = "Ye", Zip = "94102" });
                context.Persons.Add(new Person() { City = "San Maeto", FirstName = "Weimin", LastName = "English", Zip = "92103" });
                context.Persons.Add(new Person() { City = "New York", FirstName = "Handsome", LastName = "Yip", Zip = "11111" });
                context.Persons.Add(new Person() { City = "Austin", FirstName = "Frank", LastName = "Gates", Zip = "51000" });
                context.Persons.Add(new Person() { City = "Guang Zhou", FirstName = "Weimin", LastName = "Jobs", Zip = "510000" });
                context.Persons.Add(new Person() { City = "D.C.", FirstName = "John", LastName = "Smith", Zip = "10000" });

                context.SaveChanges();
            }
        }
    }
}
