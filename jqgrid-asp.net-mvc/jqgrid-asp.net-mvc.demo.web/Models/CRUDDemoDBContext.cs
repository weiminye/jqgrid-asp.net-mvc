using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqgrid_asp.net_mvc.demo.web.Models
{
    public class CRUDDemoDBContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }
}
