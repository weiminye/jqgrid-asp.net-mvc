using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jqgrid_asp.net_mvc.demo.web.Models
{
    public class CRUDDemoDBContextNoClearInitializer : IDatabaseInitializer<CRUDDemoDBContext>
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

            DataInit.Init(context);
        }
    }
}
