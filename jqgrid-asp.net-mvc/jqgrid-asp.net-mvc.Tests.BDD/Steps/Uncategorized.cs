using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using FluentAssertions;
using WatiN.Core;
using Table = TechTalk.SpecFlow.Table;
using jqgrid_asp.net_mvc.demo.web.Models;
using TechTalk.SpecFlow.Assist;

namespace jqgrid_asp.net_mvc.Tests.BDD.UI.Steps
{
    [Binding]
    public class Uncategorized
    {
        private readonly PageInputting _pageInputting = new PageInputting();
        private const string CST_TestDataGuid = "TestDataGuid";

        [When(@"Create a new test record")]
        public void WhenCreateANewTestRecord()
        {
            //_pageInputting.WhenIPressPlusButtonAtJqGrid();
            ScenarioContext.Current[CST_TestDataGuid] = Guid.NewGuid();
            WebBrowser.Current.TextField("FirstName").TypeText("FirstName" + ScenarioContext.Current[CST_TestDataGuid]);
            WebBrowser.Current.TextField("LastName").TypeText("LastName" + ScenarioContext.Current[CST_TestDataGuid]);
            WebBrowser.Current.TextField("City").TypeText("City" + ScenarioContext.Current[CST_TestDataGuid]);
            WebBrowser.Current.TextField("Zip").TypeText("Zip" + ScenarioContext.Current[CST_TestDataGuid]);
        }

        [When(@"Click the submit button")]
        public void WhenClickTheSubmitButton()
        {
            _pageInputting.WhenSubmit();
        }

        [Then(@"the added test record will be shown at jqGrid")]
        public void ThenTheAddedTestRecordWillBeShownAtJqGrid()
        {
            WebBrowser.Current.WaitUntilContainsText(ScenarioContext.Current[CST_TestDataGuid].ToString());

            var tabletext = WebBrowser.Current.Table("list").Text;
            tabletext.Should().Contain("FirstName" + ScenarioContext.Current[CST_TestDataGuid]);
            tabletext.Should().Contain("LastName" + ScenarioContext.Current[CST_TestDataGuid]);
            tabletext.Should().Contain("City" + ScenarioContext.Current[CST_TestDataGuid]);
            tabletext.Should().Contain("Zip" + ScenarioContext.Current[CST_TestDataGuid]);
        }

        [When(@"Click the update button at the test record")]
        public void WhenClickTheUpdateButtonAtTheTestRecord()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Edit with new value")]
        public void WhenEditWithNewValue()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"submit the update")]
        public void WhenSubmitTheUpdate()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the updated test record  will be shown at jqGrid with updated values")]
        public void ThenTheUpdatedTestRecordWillBeShownAtJqGridWithUpdatedValues()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"Click the delete button at the test record")]
        public void WhenClickTheDeleteButtonAtTheTestRecord()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the deleted test record  will be not shown at jqGrid")]
        public void ThenTheDeletedTestRecordWillBeNotShownAtJqGrid()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
