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
    public class Trash
    {
        private readonly Login login;
        private readonly New @new;
        private readonly MyFileOrbis myFileOrbis;
        private readonly IWebDriver driver;

        public Trash()
        {
            login = new Login();
            // successfully login
            login.SigninPositive();
            driver = login.getDriver();

            @new = new New(driver);

            myFileOrbis = new MyFileOrbis(driver);
        }

        [TestMethod]
        public void RestoreFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            @new.CreateFolderPositive();
            myFileOrbis.ThrowFolderInTrash();

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();

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

                    IWebElement restoreButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("restore-button")));
                    restoreButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    // Get alert text
                    alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the folder successfully restored!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void RestoreFile()
        {
            @new.UploadFile();
            myFileOrbis.ThrowFileInTrash();

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

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

                    IWebElement restoreButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("restore-button")));
                    restoreButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    alert.Accept();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    // Get alert text
                    alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the file successfully restored!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void DeleteForeverFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            @new.CreateFolderPositive();
            myFileOrbis.ThrowFolderInTrash();

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();

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

                    IWebElement restoreButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("delete-forever-button")));
                    restoreButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the folder successfully removed!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void DeleteForeverFile()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            @new.UploadFile();
            myFileOrbis.ThrowFileInTrash();

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();

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

                    IWebElement restoreButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("delete-forever-button")));
                    restoreButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the file successfully removed!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

    }

}
