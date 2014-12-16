using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;
using jqgrid_asp.net_mvc.Tests.BDD.Common;

namespace jqgrid_asp.net_mvc.Tests.BDD.UI.Steps
{
    [Binding]
    public class Navigation
    {
        [Given(@"I am at jqGrid page")]
        public void GivenIAmAtJqGridPage()
        {
            var url = Vars.DemoSiteWebHost;
            WebBrowser.Current.GoTo(url);
            WebBrowser.Current.Url.Should().Be(url);
        }


    }
}