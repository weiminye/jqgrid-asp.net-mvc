using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;

namespace jqgrid_asp.net_mvc.Tests.BDD.UI.Steps
{
    [Binding]
    public class PageInputting
    {
        [When(@"I press plus button at jqGrid")]
        public void WhenIPressPlusButtonAtJqGrid()
        {
            WebBrowser.Current.Span(Find.ByClass("ui-icon ui-icon-plus")).Click();
        }

        [When(@"input new record as below")]
        public void WhenInputNewRecordAsBelow(Table table)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"submit")]
        public void WhenSubmit()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
