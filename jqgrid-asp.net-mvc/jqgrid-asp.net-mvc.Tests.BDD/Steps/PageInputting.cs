using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using FluentAssertions;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;
using jqgrid_asp.net_mvc.demo.web.Models;
using TechTalk.SpecFlow.Assist;

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
            var person = table.CreateInstance<Person>();
            WebBrowser.Current.TextField("FirstName").TypeText(person.FirstName);
            WebBrowser.Current.TextField("LastName").TypeText(person.LastName);
            WebBrowser.Current.TextField("City").TypeText(person.City);
            WebBrowser.Current.TextField("Zip").TypeText(person.Zip);
        }

        [When(@"submit")]
        public void WhenSubmit()
        {
            WebBrowser.Current.Link(Find.ByClass("fm-button ui-state-default ui-corner-all fm-button-icon-left")).Click();
        }

    }
}
