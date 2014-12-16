using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jqgrid_asp.net_mvc.demo.web.Models;
using jqgrid_asp.net_mvc.Tests.BDD.Common;
using TechTalk.SpecFlow;
using System.Net.Http;
using System.Net.Http.Headers;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.Net;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json.Linq;

namespace jqgrid_asp.net_mvc.Tests.TDD.API.Steps
{
    [Binding]
    public class Uncategorized
    {
        [When(@"I read records\tvia jqGrid invoking API")]
        public void WhenIReadRecordsViaJqGridInvokingAPI()
        {
            GetPersonListByUri("/Home/IndexJsonList?_search=false&nd=1418698409753&rows=10&page=1&sidx=invid&sord=desc");
        }

        private void GetPersonListByUri(string queryurl)
        {
            Uri url = null;

            if (Uri.TryCreate(new Uri(Vars.DemoSiteWebHost), queryurl, out url))
            {
                Console.WriteLine("url is {0}", url);
            }

            HttpClient client = new HttpClient();
            // Add an Accept header for JSON format.            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List all products.
            HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! 
            if (response.IsSuccessStatusCode)
            {
                JqGridReadingJsonData = response.Content.ReadAsAsync<JqGridReadingJsonData<Person, Person>>().Result;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public JqGridReadingJsonData<Person, Person> JqGridReadingJsonData
        {
            get
            {
                return ScenarioContext.Current["JqGridReadingJsonData"] as JqGridReadingJsonData<Person, Person>;
            }
            set
            {
                ScenarioContext.Current["JqGridReadingJsonData"] = value;
            }
        }

        [Then(@"then should get init records")]
        public void ThenThenShouldGetInitRecords()
        {
            var persons = ReturnandOutputPersons();
            persons.FirstOrDefault(p => p.FirstName == "Weimin" && p.LastName == "Ye" && p.City == "San Francisco" && p.Zip == "94112").Should().NotBeNull();
        }

        private IEnumerable<Person> ReturnandOutputPersons()
        {
            var persons = JqGridReadingJsonData.rows as IEnumerable<Person>;

            foreach (var p in persons)
            {
                Console.WriteLine("FirstName: {0}\tLastName: {1}\tCity: {2}\tZip:{3}", p.FirstName, p.LastName, p.City, p.Zip);
            }
            return persons;
        }

        public string CurrentTestDataGuid
        {
            get
            {
                return ScenarioContext.Current["CurrentTestDataGuid"] as string;
            }
            set
            {
                ScenarioContext.Current["CurrentTestDataGuid"] = value;
            }
        }

        [Given(@"I create a record via jqGrid invoking API")]
        [When(@"I create a record via jqGrid invoking API")]
        public void WhenICreateARecordViaJqGridInvokingAPI()
        {
            CurrentTestDataGuid = Guid.NewGuid().ToString();
            Console.WriteLine("CurrentTestDataGuid is {0}", CurrentTestDataGuid);
            var person = new
            {
                FirstName = "FirstName" + CurrentTestDataGuid,
                LastName = "LastName" + CurrentTestDataGuid,
                Zip = "Zip" + CurrentTestDataGuid,
                City = "City" + CurrentTestDataGuid,
                oper = "add",
                id = "_empty",
            };

            Uri url = null;

            if (Uri.TryCreate(new Uri(Vars.DemoSiteWebHost), "/Home/UpdateForJqGrid", out url))
            {
                Console.WriteLine("url is {0}", url);
            }

            HttpClient client = new HttpClient();
            // Add an Accept header for JSON format.            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Uri finalurl = null;
            // List all products.
            var response = client.PostAsJsonAsync(url, person).Result;  // Blocking call! 
            if (response.IsSuccessStatusCode)
            {
                finalurl = response.Headers.Location;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [Then(@"the record should be listed at the result with reading record")]
        public void ThenTheRecordShouldBeListedAtTheResultWithReadingRecord()
        {
            VerifyIfExistatDB(CurrentTestDataGuid, true);
        }

        private void VerifyIfExistatDB(string testdataguid, bool isexist)
        {
            WhenIReadRecordsViaJqGridInvokingAPI();

            var persons = JqGridReadingJsonData.rows as IEnumerable<Person>;
            if (isexist) persons.FirstOrDefault(p => p.FirstName.Contains(testdataguid)).Should().NotBeNull();
            else persons.FirstOrDefault(p => p.FirstName.Contains(testdataguid)).Should().BeNull();
        }


        [When(@"I update the record via jqGrid invoking API")]
        public void WhenIUpdateTheRecordViaJqGridInvokingAPI()
        {
            UpdatedTestDataGuid = Guid.NewGuid().ToString();
            Console.WriteLine("UpdatedTestDataGuid is {0}", UpdatedTestDataGuid);

            WhenIReadRecordsViaJqGridInvokingAPI();
            var persons = JqGridReadingJsonData.rows as IEnumerable<Person>;
            var id = persons.FirstOrDefault(p => p.FirstName.Contains(CurrentTestDataGuid)).ID;

            var person = new
            {
                FirstName = "FirstName" + UpdatedTestDataGuid,
                LastName = "LastName" + UpdatedTestDataGuid,
                Zip = "Zip" + UpdatedTestDataGuid,
                City = "City" + UpdatedTestDataGuid,
                oper = "edit",
                ID = id,
            };

            Uri url = null;

            if (Uri.TryCreate(new Uri(Vars.DemoSiteWebHost), "/Home/UpdateForJqGrid", out url))
            {
                Console.WriteLine("url is {0}", url);
            }

            HttpClient client = new HttpClient();
            // Add an Accept header for JSON format.            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Uri finalurl = null;
            // List all products.
            var response = client.PostAsJsonAsync(url, person).Result;  // Blocking call! 
            if (response.IsSuccessStatusCode)
            {
                finalurl = response.Headers.Location;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [Then(@"the record with the updated value should be listed at the result with reading record")]
        public void ThenTheRecordWithTheUpdatedValueShouldBeListedAtTheResultWithReadingRecord()
        {
            VerifyIfExistatDB(UpdatedTestDataGuid, true);
        }

        public string UpdatedTestDataGuid
        {
            get
            {
                return ScenarioContext.Current["UpdatedTestDataGuid"] as string;
            }
            set
            {
                ScenarioContext.Current["UpdatedTestDataGuid"] = value;
            }
        }

        [Then(@"the record with the original value should NOT be listed at the result with reading record")]
        public void ThenTheRecordWithTheOriginalValueShouldNOTBeListedAtTheResultWithReadingRecord()
        {
            VerifyIfExistatDB(CurrentTestDataGuid, false);
        }

        [When(@"I delete the record via jqGrid invoking API")]
        public void WhenIDeleteTheRecordViaJqGridInvokingAPI()
        {
            WhenIReadRecordsViaJqGridInvokingAPI();
            var persons = JqGridReadingJsonData.rows as IEnumerable<Person>;
            var id = persons.FirstOrDefault(p => p.FirstName.Contains(CurrentTestDataGuid)).ID;

            var person = new
            {
                oper = "del",
                id = id,
            };

            Uri url = null;

            if (Uri.TryCreate(new Uri(Vars.DemoSiteWebHost), "/Home/UpdateForJqGrid", out url))
            {
                Console.WriteLine("url is {0}", url);
            }

            HttpClient client = new HttpClient();
            // Add an Accept header for JSON format.            
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            Uri finalurl = null;
            // List all products.
            var response = client.PostAsJsonAsync(url, person).Result;  // Blocking call! 
            if (response.IsSuccessStatusCode)
            {
                finalurl = response.Headers.Location;
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        [When(@"I search by FirstName as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByFirstNameAsViaJqGridInvokingAPI(string firstname)
        {
            var queryurl = "/Home/IndexJsonList?_search=true&nd=1418720609240&rows=10&page=1&sidx=invid&sord=desc&filters=%7B%22groupOp%22%3A%22AND%22%2C%22rules%22%3A%5B%7B%22field%22%3A%22FirstName%22%2C%22op%22%3A%22true%22%2C%22data%22%3A%22we%22%7D%5D%7D";

            GetPersonListByUri(queryurl);
        }

        [Then(@"the returned record should all contains '(.*)' at FirstName")]
        public void ThenTheReturnedRecordShouldAllContainsAtFirstName(string firstname)
        {
            var persons = ReturnandOutputPersons();
            
            foreach (var p in persons)
            {
                p.FirstName.Contains(firstname).Should().BeTrue();
            }
        }

        [Then(@"the record that  at FirstName is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtFirstNameIsShouldNOTBeListedAtTheResultWithReadingRecord(string firstname)
        {
            var persons = ReturnandOutputPersons();
            persons.FirstOrDefault(p => p.FirstName == firstname).Should().BeNull();
        }

        [When(@"I search by LastName as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByLastNameAsViaJqGridInvokingAPI(string lastname)
        {
            var queryurl = "/Home/IndexJsonList?_search=true&nd=1418721238205&rows=10&page=1&sidx=invid&sord=desc&filters=%7B%22groupOp%22%3A%22AND%22%2C%22rules%22%3A%5B%7B%22field%22%3A%22LastName%22%2C%22op%22%3A%22true%22%2C%22data%22%3A%22Y%22%7D%5D%7D";

            GetPersonListByUri(queryurl);
        }

        [Then(@"the returned record should all contains '(.*)' at LastName")]
        public void ThenTheReturnedRecordShouldAllContainsAtLastName(string lastname)
        {
            var persons = ReturnandOutputPersons();

            foreach (var p in persons)
            {
                p.LastName.Contains(lastname).Should().BeTrue();
            }
        }

        [Then(@"the record that  at LastName is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtLastNameIsShouldNOTBeListedAtTheResultWithReadingRecord(string lastname)
        {
            var persons = ReturnandOutputPersons();
            persons.FirstOrDefault(p => p.LastName == lastname).Should().BeNull();
        }

        [When(@"I search by City as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByCityAsViaJqGridInvokingAPI(string city)
        {
            var queryurl = "/Home/IndexJsonList?_search=true&nd=1418722379194&rows=10&page=1&sidx=invid&sord=desc&filters=%7B%22groupOp%22%3A%22AND%22%2C%22rules%22%3A%5B%7B%22field%22%3A%22City%22%2C%22op%22%3A%22true%22%2C%22data%22%3A%22san%22%7D%5D%7D";

            GetPersonListByUri(queryurl);
        }

        [Then(@"the returned record should all contains '(.*)' at City")]
        public void ThenTheReturnedRecordShouldAllContainsAtCity(string city)
        {
            var persons = ReturnandOutputPersons();

            foreach (var p in persons)
            {
                p.City.Contains(city).Should().BeTrue();
            }
        }

        [Then(@"the record that  at City is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtCityIsShouldNOTBeListedAtTheResultWithReadingRecord(string city)
        {
            var persons = ReturnandOutputPersons();
            persons.FirstOrDefault(p => p.City == city).Should().BeNull();
        }

        [When(@"I search by Zip as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByZipAsViaJqGridInvokingAPI(string zip)
        {
            var queryurl = "/Home/IndexJsonList?_search=true&nd=1418722215016&rows=10&page=1&sidx=invid&sord=desc&filters=%7B%22groupOp%22%3A%22AND%22%2C%22rules%22%3A%5B%7B%22field%22%3A%22Zip%22%2C%22op%22%3A%22true%22%2C%22data%22%3A%2294%22%7D%5D%7D";

            GetPersonListByUri(queryurl);
        }

        [Then(@"the returned record should all contains '(.*)' at Zip")]
        public void ThenTheReturnedRecordShouldAllContainsAtZip(string zip)
        {
            var persons = ReturnandOutputPersons();

            foreach (var p in persons)
            {
                p.Zip.Contains(zip).Should().BeTrue();
            }
        }

        [Then(@"the record that  at Zip is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtZipIsShouldNOTBeListedAtTheResultWithReadingRecord(string zip)
        {
            var persons = ReturnandOutputPersons();
            persons.FirstOrDefault(p => p.Zip == zip).Should().BeNull();
        }

        [When(@"I search by FirstName as '(.*)' and LastName as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByFirstNameAsAndLastNameAsViaJqGridInvokingAPI(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the returned record should all contains '(.*)' at FirstName and '(.*)' at LastName")]
        public void ThenTheReturnedRecordShouldAllContainsAtFirstNameAndAtLastName(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the record that  at FistName is '(.*)' and LastName is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtFistNameIsAndLastNameIsShouldNOTBeListedAtTheResultWithReadingRecord(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I search by FirstName as '(.*)' and City as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByFirstNameAsAndCityAsViaJqGridInvokingAPI(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the returned record should all contains '(.*)' at City and '(.*)' at LastName")]
        public void ThenTheReturnedRecordShouldAllContainsAtCityAndAtLastName(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the record that  at FistName is '(.*)' and City is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtFistNameIsAndCityIsShouldNOTBeListedAtTheResultWithReadingRecord(string p0, string p1)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I search by LastName as '(.*)' and Zip as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByLastNameAsAndZipAsViaJqGridInvokingAPI(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the returned record should all contains '(.*)' at LastName and '(.*)' at Zip")]
        public void ThenTheReturnedRecordShouldAllContainsAtLastNameAndAtZip(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the record that  at LastName is '(.*)' and Zip is '(.*)' should NOT be listed at the result with reading record")]
        public void ThenTheRecordThatAtLastNameIsAndZipIsShouldNOTBeListedAtTheResultWithReadingRecord(string p0, int p1)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I search by FirstName as '(.*)' and LastName as '(.*)' and City as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByFirstNameAsAndLastNameAsAndCityAsViaJqGridInvokingAPI(string p0, string p1, string p2)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the returned record should all contains '(.*)' at FirstName and '(.*)' at LastName and '(.*)' at City")]
        public void ThenTheReturnedRecordShouldAllContainsAtFirstNameAndAtLastNameAndAtCity(string p0, string p1, string p2)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I search by FirstName as '(.*)' and LastName as '(.*)' and City as '(.*)' and Zip as '(.*)' via jqGrid invoking API")]
        public void WhenISearchByFirstNameAsAndLastNameAsAndCityAsAndZipAsViaJqGridInvokingAPI(string p0, string p1, string p2, int p3)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the returned record should all contains '(.*)' at FirstName and '(.*)' at LastName and '(.*)' and '(.*)' at Zip at City")]
        public void ThenTheReturnedRecordShouldAllContainsAtFirstNameAndAtLastNameAndAndAtZipAtCity(string p0, string p1, string p2, int p3)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
