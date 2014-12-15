using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace jqgrid_asp.net_mvc.Tests.BDD.UI
{
    public class WebBrowser
    {
        public static Browser Current
        {
            get
            {
                if (!ScenarioContext.Current.ContainsKey("browser"))
                {
                    var ie = new IE();
                    ie.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
                    ScenarioContext.Current["browser"] = ie;
                }
                return (Browser)ScenarioContext.Current["browser"];

                /* for featurecontext begin*/
                //if (!FeatureContext.Current.ContainsKey("browser"))
                //{
                //    var ie = new IE();
                //    ie.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.ShowMaximized);
                //    FeatureContext.Current["browser"] = ie;
                //}
                //return (Browser)FeatureContext.Current["browser"];
                /* for featurecontext end*/
            }
        }

        public static void CloseThenOpen(string url)
        {
            Current.Close();
            ScenarioContext.Current.Remove("browser");
            Current.GoTo(url);
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            if (ScenarioContext.Current.ContainsKey("browser"))
            {
                Current.Close();
            }
        }

        /* for featurecontext begin*/
        //[AfterFeature]
        //public static void AfterFeature()
        //{
        //    if (FeatureContext.Current.ContainsKey("browser"))
        //    {
        //        Current.Close();
        //    }
        //}
        /* for featurecontext end*/

        #region for ajax page
        public static void WaitForAsyncPostbackComplete()
        {
            WaitForAsyncPostbackComplete(WebBrowser.Current, 1000);
        }

        //http://www.codeproject.com/Articles/99838/WatinN-to-Automate-Browser-and-Test-Sophisticated
        public static bool WaitForAsyncPostbackComplete(Browser browser, int timeout)
        {
            int timeWaitedInMilliseconds = 0;
            var maxWaitTimeInMilliseconds = Settings.WaitForCompleteTimeOut * 1000;
            var scriptToCheck =
            "Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();";

            while (bool.Parse(browser.Eval(scriptToCheck)) == true
                    && timeWaitedInMilliseconds < maxWaitTimeInMilliseconds)
            {
                Thread.Sleep(Settings.SleepTime);
                timeWaitedInMilliseconds += Settings.SleepTime;
            }

            return bool.Parse(browser.Eval(scriptToCheck));
        }

        //http://www.developerfusion.com/article/134437/web-testing-with-mbunit-and-watin-part-3-testing-asynchronous-ajax-calls/
        //public static void WaitForAsyncPostBackComplete()
        //{
        //    switch (BrowserContext.BrowserConfiguration.BrowserType)
        //    {
        //        case BrowserType.IE:
        //            {
        //                Wait(() => !Convert.ToBoolean(Browser.Eval
        //                   ("Sys.WebForms.PageRequestManager.getInstance().get_isInAsyncPostBack();")));
        //                break;
        //            }
        //    }
        //}

        #endregion
    }
}
