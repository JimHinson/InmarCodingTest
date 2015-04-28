using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace InmarCodingTest
{
    [TestClass]
    public class InmarUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }


        [TestMethod]
      public void MyTestMethod()
      {
          IWebDriver driver = new FirefoxDriver();
          driver.Navigate().GoToUrl("http://www.seleniumframework.com/Practiceform/");
          String originalWindow = driver.CurrentWindowHandle;
          driver.FindElements(By.TagName("button"))[2].Click();
          //driver.SwitchTo().Window()
          Console.WriteLine("New Window Title: " + driver.Title);
            Assert.IsTrue(driver.WindowHandles.Count == 2, "Should have two windows open!");
          driver.SwitchTo().Window((driver.WindowHandles.ElementAt(1)));
          Console.WriteLine("There were " + driver.WindowHandles.Count + " windows open.  Press any key to continue.");
          Console.ReadKey();
          driver.Close();
          driver.SwitchTo().Window(originalWindow);
          driver.Close();

          
      }
    }


}
