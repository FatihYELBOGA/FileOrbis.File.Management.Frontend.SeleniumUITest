using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FileOrbis.File.Management.Frontend.SeleniumUITest
{
    [TestClass]
    public class Recent
    {
        private readonly Login login;
        private readonly IWebDriver driver;

        public Recent()
        {
            login = new Login();

            // successfully login
            login.SigninPositive();

            driver = login.getDriver();
        }

        [TestMethod]
        public void OpenFile()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement newNavbar = driver.FindElement(By.Id("Recent"));
            newNavbar.Click();

            string mainWindowHandle = driver.CurrentWindowHandle;

            Thread.Sleep(1000);
            IWebElement file = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    file = row;
                    break;
                }

                if (file != null)
                {
                    file.Click();

                    IWebElement openButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("open-button")));
                    openButton.Click();

                    Thread.Sleep(2000);
                    bool isNewTabOpened = IsNewTabOpened(driver, mainWindowHandle);

                    if (isNewTabOpened)
                    {
                        Console.WriteLine("the file is opened!");
                    }
                    else
                    {
                        // Wait for alert for 3 seconds
                        wait.Until(ExpectedConditions.AlertIsPresent());

                        // Get alert text
                        IAlert alert = driver.SwitchTo().Alert();
                        string alertText = alert.Text;
                        alert.Accept();

                        string expectedText = "only pdf and images can be viewed on the browser!";
                        Assert.AreEqual(expectedText, alertText);
                        Console.WriteLine(expectedText);
                    }
                }
            }
        }

        private static bool IsNewTabOpened(IWebDriver driver, string mainWindowHandle)
        {
            foreach (string handle in driver.WindowHandles)
            {
                if (handle != mainWindowHandle)
                {
                    return true; 
                }
            }
            return false; 
        }

        [TestMethod]
        public void GotoFile()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement newNavbar = driver.FindElement(By.Id("Recent"));
            newNavbar.Click();

            Thread.Sleep(1000);
            IWebElement file = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    file = row;
                    break;
                }

                if (file != null)
                {
                    file.Click();

                    IWebElement locationButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("location-button")));
                    locationButton.Click();

                    try
                    {
                        // Wait for alert for 3 seconds
                        wait.Until(ExpectedConditions.AlertIsPresent());

                        // Get alert text
                        IAlert alert = driver.SwitchTo().Alert();
                        string alertText = alert.Text;
                        alert.Accept();

                        string expectedText = "can not be navigated to the file because the file places in the trash!";
                        Assert.AreEqual(expectedText, alertText);
                        Console.WriteLine(expectedText);
                    }
                    catch (Exception ex)
                    {
                        IWebElement breadCrumb = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("my-fileorbis")));
                        Console.WriteLine("the location is viewed!");
                    }
                }
            }
        }

    }

}
