using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace jqgrid_asp.net_mvc.Tests.BDD.UI.Steps
{
    [Binding]
    public class Verification
    {
        [Then(@"the record will be shown at jqGrid")]
        public void ThenTheRecordWillBeShownAtJqGrid()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
