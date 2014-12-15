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
        private string TestDataGuid
        {
            get
            {
                return ScenarioContext.Current["TestDataGuid"].ToString();
            }
            set
            {
                ScenarioContext.Current["TestDataGuid"] = value;
            }
        }

        [When(@"Create a new test record")]
        public void WhenCreateANewTestRecord()
        {
            //_pageInputting.WhenIPressPlusButtonAtJqGrid();
            TestDataGuid = Guid.NewGuid().ToString();
            WebBrowser.Current.TextField("FirstName").TypeText("FirstName" + TestDataGuid);
            WebBrowser.Current.TextField("LastName").TypeText("LastName" + TestDataGuid);
            WebBrowser.Current.TextField("City").TypeText("City" + TestDataGuid);
            WebBrowser.Current.TextField("Zip").TypeText("Zip" + TestDataGuid);
        }

        [When(@"Click the submit button")]
        public void WhenClickTheSubmitButton()
        {
            _pageInputting.WhenSubmit();
            WebBrowser.Current.WaitUntilContainsText(TestDataGuid);
            WebBrowser.Current.Span(Find.ByClass("ui-icon ui-icon-closethick")).Click();
        }

        [Then(@"the added test record will be shown at jqGrid")]
        public void ThenTheAddedTestRecordWillBeShownAtJqGrid()
        {
            var tabletext = WebBrowser.Current.Table("list").Text;
            tabletext.Should().Contain("FirstName" + TestDataGuid);
            tabletext.Should().Contain("LastName" + TestDataGuid);
            tabletext.Should().Contain("City" + TestDataGuid);
            tabletext.Should().Contain("Zip" + TestDataGuid);
        }

        [When(@"Click the update button at the test record")]
        public void WhenClickTheUpdateButtonAtTheTestRecord()
        {
            var testrecordid = string.Empty;
            using (var db = new CRUDDemoDBContext())
            {

                testrecordid = db.Persons.Single(p => p.FirstName.Contains(TestDataGuid)).ID.ToString();
            }
            WebBrowser.Current.Div("jEditButton_" + testrecordid).Click();
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
