using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
//JSON
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InmarCodingTest
{
    class Jim
    {
        
        private const string DATA = @"{""object"":{""name"":""Name""}}";

        static bool isSocialSecurityNumber(int[] testValue)
        {

            if (testValue.Length != 9) {
                return false;
            }
            else {
                for (int i = 0; i < testValue.Length; i++)
                {
                    if ((testValue[i] < 0) || testValue[i] > 9)
                        return false;
                }
                return true;    
            }
        }

        static String reverseString(String original)
        {
            // Using simple method.  If concerned about performance, see:  http://stackoverflow.com/questions/228038/best-way-to-reverse-a-string
            if (original == null)
                return "";
            return new String(original.Reverse().ToArray());
        }

        static void restTest() {
                        Console.Read();
            HttpClient client = new HttpClient();
            string URL = "http://www.thomas-bayer.com/sqlrest/CUSTOMER/0/";
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            
            HttpResponseHeaders headers;
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call!
            
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                //response.Content.ReadAsStringAsync
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                foreach (var d in dataObjects)
                {
                    Console.WriteLine("{0}", d.Name);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }  
        
            
            Console.WriteLine("Response Status: " + response.StatusCode.ToString());
            Console.WriteLine("Response Body: " + response.Content.ToString());
            //Console.WriteLine("respnose message content: " + response.RequestMessage.Content());
            
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

        }

        public class DataObject
        {
            public string Name { get; set; }
        }


        static void seleniumWindowTitle() {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.intuisoft.com/");
            Console.WriteLine("Window Title: " + driver.Title);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            driver.Close();
        }

        static void seleniumPrintAnchorTags()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.inmar.com");            
            IList<IWebElement> anchors = driver.FindElements(By.TagName("a"));
            for (int i = 0; i < anchors.Count; i++)
			{
			    Console.WriteLine("anchor text: " + ((IWebElement)anchors.ElementAt(i)).Text);
			}
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            driver.Close();
        }

        static void seleniumPracticeForm()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.practiceselenium.com/practice-form.html");
            driver.FindElement(By.Name("firstname")).SendKeys("George");
            driver.FindElement(By.Name("lastname")).SendKeys("Jetson");
            driver.FindElement(By.Id("submit")).Click();
            String url = driver.Url;
            Debug.Assert(url.Contains("welcome.html"), "We didn't go home!!");
            Console.WriteLine ("We went home!  Press any key to continue.");
            Console.ReadKey();
            driver.Close();
        }

        static void seleniumNewBrowserTab()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");            
            String originalWindow = driver.CurrentWindowHandle;
            driver.FindElements(By.TagName("button"))[2].Click();           
            //driver.SwitchTo().Window()
            Console.WriteLine("New Window Title: " + driver.Title);            
            Debug.Assert((driver.WindowHandles.Count == 2), "Should have 2 windows open!");
            driver.SwitchTo().Window((driver.WindowHandles.ElementAt(1)));
            Console.WriteLine("There were " + driver.WindowHandles.Count + " windows open.  Press any key to continue.");
            Console.ReadKey();
            driver.Close();           
            driver.SwitchTo().Window(originalWindow);
            driver.Close();
        }

        static void jdbcTest()
        {/*
            // Declare before try.
            Connection connection = null;
            PreparedStatement statement = null;
            ResultSet resultSet = null;
            try
            {
                Class.forName("com.imaginary.sql.msql.MsqlDriver");
                connection = DriverManager.getConnection("jdbc:msql://www.myserver.com:1114/....", "user1", "password");
                statement = connection.prepareStatement("SELECT * FROM FOO WHERE A=?", 1);
                resultSet = statement.executeQuery();
            }
            finally
            {
                // Close in reversed order in finally.
                if (resultSet != null) try { resultSet.close(); }
                    catch (SQLException logOrIgnore) { }
                if (statement != null) try { statement.close(); }
                    catch (SQLException logOrIgnore) { }
                if (connection != null) try { connection.close(); }
                    catch (SQLException logOrIgnore) { }
            }
            */
        }

        static public void parseJSONTest()
        {
            String simpleJSon = "{a:1,b:2,c:3}";
            JObject jo = JObject.Parse(simpleJSon);
            Console.WriteLine("value of 'c': " + jo["c"]);
            Console.WriteLine("Please press any key to continue.");
            Console.Read();
        }

        static void Main()
        {
            /*
            //#1: FizzBuzz
            for (int i = 1; i <= 100; i++)
            {
                if ((i % 3) == 0)
                {
                    System.Console.Write("fizz");
                }
                if ((i % 5) == 0)
                {
                    System.Console.Write("buzz");
                }
                System.Console.WriteLine();                                
            }
            System.Console.WriteLine("Press any key to continue.");
            System.Console.ReadKey();

            //#2:  Reverse String            
            String original = "abcdef";
            System.Console.WriteLine("Testing: " + original + "\nReversed: " + reverseString(original));
            original = null;
            System.Console.WriteLine("Testing: " + original + "\nReversed: " + reverseString(original));          
            System.Console.WriteLine("Press any key to continue");
            System.Console.ReadKey();

            //#3: Is SSN?
            System.Console.WriteLine("Testing 1, 2, 3, 4: " + isSocialSecurityNumber(new int[] { 1, 2, 3, 4 }));
            System.Console.WriteLine("Testing 1, 2, 3, 4, 5, 6, 7, 8, 9: " + isSocialSecurityNumber(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            System.Console.WriteLine("Testing -1, 2, 3, 4, 5, 6, 7, 8, 9: " + isSocialSecurityNumber(new int[] {-1, 2, 3, 4, 5, 6, 7, 8, 9 }));
            System.Console.WriteLine("Testing 12, 2, 3, 4, 5, 6, 7, 8, 9: " + isSocialSecurityNumber(new int[] { 12, 2, 3, 4, 5, 6, 7, 8, 9 }));
            System.Console.WriteLine("Testing -00000000: " + isSocialSecurityNumber(new int[] { -000000000 }));
            System.Console.WriteLine("Testing -0, 0, 0, 0, 0, 0, 0, 0: " + isSocialSecurityNumber(new int[] { -0, 0, 0, 0, 0, 0, 0, 0, 0 }));
            System.Console.WriteLine("Press any key to continue");
            System.Console.ReadKey();
            
            //#4: Adding
            addition myAddition = new addition();
            myAddition.setX(90);
            myAddition.setY(90);
            System.Console.WriteLine("90 + 90: " + myAddition.addXandY().ToString());
            myAddition.setY(Int32.MaxValue);           
            int results = myAddition.addXandY();
            Debug.Assert(results > 0, "Addition of two positive numbers should never be less than zero!");
            System.Console.WriteLine("90 + maxInt: " + results.ToString());
            System.Console.ReadKey();
            myAddition.setX(Int32.MaxValue * -1);
            results = myAddition.addXandY();
            Debug.Assert(results == 0, "Adding maxint and negative maxint should result in 0!");
            System.Console.WriteLine("Result: " + results.ToString());
            System.Console.ReadKey();
            */
            //restTest();
            //#6:  Open a window and print the title
            //seleniumWindowTitle();
            //#7: Cucumber
            //#8: Print Anchor Tags
            //seleniumPrintAnchorTags();
            //#9: PracticeForm
            //seleniumPracticeForm();
            //#10:  new browser tab
            //seleniumNewBrowserTab();
            //#11:  JDBC pseudocode
            //jdbcTest();
            //#12: Parse a JSON string
            parseJSONTest();

        }
    }
}
