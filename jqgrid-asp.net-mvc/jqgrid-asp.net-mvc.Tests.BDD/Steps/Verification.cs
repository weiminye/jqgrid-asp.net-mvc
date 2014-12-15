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
    public class Verification
    {
        [Then(@"the record will be shown at jqGrid")]
        public void ThenTheRecordWillBeShownAtJqGrid(Table table)
        {
            var person = table.CreateInstance<Person>();
            var tabletext = WebBrowser.Current.Table("list").Text;
            tabletext.Should().Contain(person.City);
            tabletext.Should().Contain(person.FirstName);
            tabletext.Should().Contain(person.LastName);
            tabletext.Should().Contain(person.Zip);
        }
    }
}