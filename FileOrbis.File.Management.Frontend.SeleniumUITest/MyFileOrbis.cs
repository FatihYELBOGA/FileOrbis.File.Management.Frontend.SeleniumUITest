using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace FileOrbis.File.Management.Frontend.SeleniumUITest
{
    [TestClass]
    public class MyFileOrbis
    {
        private readonly Login login;
        private readonly IWebDriver driver;

        public MyFileOrbis()
        {
            login = new Login();

            // successfully login
            login.SigninPositive();

            driver = login.getDriver();
        }

        [TestMethod]
        public void RenameFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
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
                    string oldFolderName = folder.FindElements(By.TagName("td"))[0].Text;

                    folder.Click();
                    IWebElement renameButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rename-button")));
                    renameButton.Click();
                    IWebElement renameText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rename-text")));
                    renameText.SendKeys("new name for folder");

                    string expectedText = "the folder name: 'new name for folder' is already exist!";

                    IWebElement OKbutton = driver.FindElement(By.Id("OK-button"));
                    OKbutton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    if (!oldFolderName.Equals("new name for folder"))
                    {
                        expectedText = "folder name changed succesfully!";
                    }

                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void RenameFile()
        {
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
                    string oldFileNameWithExtension = file.FindElements(By.TagName("td"))[0].Text;
                    string[] fileNames = oldFileNameWithExtension.Split('.');
                    string oldFileName = "";

                    if (fileNames.Length > 1)
                    {
                        oldFileName = string.Join(".", fileNames, 0, fileNames.Length - 1);
                    }

                    file.Click();
                    IWebElement renameButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rename-button")));
                    renameButton.Click();
                    IWebElement renameText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("rename-text")));
                    renameText.SendKeys("new name for file");

                    string expectedText = "the file name: '" + oldFileNameWithExtension + "' is already exist!";

                    IWebElement OKbutton = driver.FindElement(By.Id("OK-button"));
                    OKbutton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    if (!oldFileName.Equals("new name for file"))
                    {
                        expectedText = "file name changed succesfully!";
                    }

                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void DownloadFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
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
                    IWebElement downloadButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("download-button")));
                    downloadButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "folder downloaded successfully!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void DownloadFile()
        {
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
                    IWebElement downloadButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("download-button")));
                    downloadButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "file downloaded successfully!";
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }


        [TestMethod]
        public void StarFolder()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
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
                    IWebElement starButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("star-button")));
                    starButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the file&folder is already starred!";

                    try
                    {
                        folder.FindElements(By.Id("star-icon"));
                    }
                    catch (Exception ex)
                    {
                        expectedText = "folder succesfully starred!";
                    }

                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void StarFile()
        {
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
                    IWebElement starButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("star-button")));
                    starButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the file&folder is already starred!";

                    try
                    {
                        file.FindElements(By.Id("star-icon"));
                    }
                    catch (Exception ex)
                    {
                        expectedText = "file succesfully starred!";
                    }

                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void ThrowFolderInTrash()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();
            Thread.Sleep(1000);
            IReadOnlyCollection<IWebElement> trashRows = driver.FindElements(By.XPath("//table/tbody/tr"));
            int rowCountBefore = trashRows.Count;

            IWebElement myFileOrbisNavbar = driver.FindElement(By.Id("My FileOrbis"));
            myFileOrbisNavbar.Click();
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
                    IWebElement starButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("trash-button")));
                    starButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the folder succesfully trashed!";

                    trashNavbar.Click();
                    Thread.Sleep(1000);
                    IReadOnlyCollection<IWebElement> trashRowsAfter = driver.FindElements(By.XPath("//table/tbody/tr"));
                    int rowCountAfter = trashRowsAfter.Count;

                    Assert.AreEqual(rowCountBefore + 1, rowCountAfter);
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

        [TestMethod]
        public void ThrowFileInTrash()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            IWebElement trashNavbar = driver.FindElement(By.Id("Trash"));
            trashNavbar.Click();
            Thread.Sleep(1000);
            IReadOnlyCollection<IWebElement> trashRows = driver.FindElements(By.XPath("//table/tbody/tr"));
            int rowCountBefore = trashRows.Count;

            IWebElement myFileOrbisNavbar = driver.FindElement(By.Id("My FileOrbis"));
            myFileOrbisNavbar.Click();
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
                    IWebElement starButton = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("trash-button")));
                    starButton.Click();

                    // Wait for alert for 3 seconds
                    wait.Until(ExpectedConditions.AlertIsPresent());

                    // Get alert text
                    IAlert alert = driver.SwitchTo().Alert();
                    string alertText = alert.Text;
                    alert.Accept();

                    string expectedText = "the file succesfully trashed!";

                    trashNavbar.Click();
                    Thread.Sleep(1000);
                    IReadOnlyCollection<IWebElement> trashRowsAfter = driver.FindElements(By.XPath("//table/tbody/tr"));
                    int rowCountAfter = trashRowsAfter.Count;

                    Assert.AreEqual(rowCountBefore + 1, rowCountAfter);
                    Assert.AreEqual(expectedText, alertText);
                    Console.WriteLine(expectedText);
                }
            }
        }

    }

}
