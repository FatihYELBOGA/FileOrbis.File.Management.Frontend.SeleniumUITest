using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileOrbis.File.Management.Frontend.SeleniumUITest
{
    [TestClass]
    public class Starred
    {
        private readonly Login login;
        private readonly IWebDriver driver;

        public Starred()
        {
            login = new Login();

            // successfully login
            login.SigninPositive();

            driver = login.getDriver();
        }

        [TestMethod]
        public void RemoveFolderInStarred()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement starredNavbar = driver.FindElement(By.Id("Starred"));
            starredNavbar.Click();

            Thread.Sleep(1000);
            IWebElement folder = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    if (row.FindElements(By.TagName("td"))[3].Text.Equals("-"))
                    {
                        folder = row;
                        break;
                    }
                }

                if (folder != null)
                {
                    folder.Click();

                    IWebElement removeStarButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remove-star-button")));
                    removeStarButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the starred folder succesfully removed!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void RemoveFileInStarred()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement starredNavbar = driver.FindElement(By.Id("Starred"));
            starredNavbar.Click();

            Thread.Sleep(1000);
            IWebElement file = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    if (!row.FindElements(By.TagName("td"))[3].Text.Equals("-"))
                    {
                        file = row;
                        break;
                    }
                }

                if (file != null)
                {
                    file.Click();

                    IWebElement removeStarButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("remove-star-button")));
                    removeStarButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the starred file succesfully removed!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void GotoFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement starredNavbar = driver.FindElement(By.Id("Starred"));
            starredNavbar.Click();

            Thread.Sleep(1000);
            IWebElement folder = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    if (row.FindElements(By.TagName("td"))[3].Text.Equals("-"))
                    {
                        folder = row;
                        break;
                    }
                }

                if (folder != null)
                {
                    folder.Click();

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

                        string expectedText = "can not be navigated to the folder because the folder places in the trash!";
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

        [TestMethod]
        public void GotoFile()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement starredNavbar = driver.FindElement(By.Id("Starred"));
            starredNavbar.Click();

            Thread.Sleep(1000);
            IWebElement file = null;
            IReadOnlyCollection<IWebElement> rows = driver.FindElements(By.XPath("//table/tbody/tr"));

            if (rows.Count > 0)
            {
                foreach (IWebElement row in rows)
                {
                    if (!row.FindElements(By.TagName("td"))[3].Text.Equals("-"))
                    {
                        file = row;
                        break;
                    }
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
