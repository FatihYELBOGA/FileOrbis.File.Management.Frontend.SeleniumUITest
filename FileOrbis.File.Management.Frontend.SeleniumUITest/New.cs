using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;
using System.Threading;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Interactions;

namespace FileOrbis.File.Management.Frontend.SeleniumUITest
{
    [TestClass]
    public class New
    {
        private readonly Login login;
        private readonly IWebDriver driver;

        public New () 
        {
            login = new Login();

            // successfully login
            login.SigninPositive();

            driver = login.getDriver();
        }

        [TestMethod]
        public void CreateFolderPositive()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            Thread.Sleep(1000);
            IWebElement table = driver.FindElement(By.XPath("//table"));
            int rowCountBefore = table.FindElements(By.TagName("tr")).Count();

            // New navbar
            IWebElement newNavbar = driver.FindElement(By.Id("new"));
            newNavbar.Click();

            // New Folder Item of New Navbar
            IWebElement newFolderItem = driver.FindElement(By.Id("new-folder"));
            newFolderItem.Click();

            // New Folder Item of New Navbar
            IWebElement newFolderText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("new-folder-text")));
            newFolderText.SendKeys(Keys.Control + "a");
            newFolderText.SendKeys(Keys.Delete);
            newFolderText.SendKeys("test-folder");

            // OK button
            IWebElement OKbutton = driver.FindElement(By.Id("OK-button"));
            OKbutton.Click();

            // Wait for alert for 3 seconds
            wait.Until(ExpectedConditions.AlertIsPresent());

            // Get alert text
            IAlert alert = driver.SwitchTo().Alert();

            string alertText = alert.Text;

            string existAlert = "the folder name: 'test-folder' is already exist!";
            string creationAlert = "folder created succesfully";

            // Accept alert
            alert.Accept(); 

            Thread.Sleep(1000);
            table = driver.FindElement(By.XPath("//table"));
            int rowCountAfter = table.FindElements(By.TagName("tr")).Count();
            if (rowCountAfter == rowCountBefore)
            {
                Assert.AreEqual(true, rowCountAfter == rowCountBefore);
                Assert.AreEqual(alertText, existAlert);
                Console.WriteLine("folder is already exist");
            }
            else
            {
                Assert.AreEqual(true, rowCountAfter - 1 == rowCountBefore);
                Assert.AreEqual(alertText, creationAlert);
                Console.WriteLine("folder created");
            }
        }

        [TestMethod]
        public void CreateFolderNegative()
        {
            // New navbar
            IWebElement newNavbar = driver.FindElement(By.Id("new"));
            newNavbar.Click();

            // New Folder Item of New Navbar
            IWebElement newFolderItem = driver.FindElement(By.Id("new-folder"));
            newFolderItem.Click();

            // Wait for alert for 3 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            // New Folder Item of New Navbar
            IWebElement newFolderText = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("new-folder-text")));

            CreateFolderNegative(wait, newFolderText, "   ", "the folder name can not be empty!"); 
            CreateFolderNegative(wait, newFolderText, "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "the folder name can not exceed 255 characters!");
            CreateFolderNegative(wait, newFolderText, "?", "The folder name can not contain the following characters: / \\ * ? \" < > |");
        }

        private void CreateFolderNegative(WebDriverWait wait, IWebElement newFolderText, string text, string message)
        {
            Thread.Sleep(1000);
            newFolderText.SendKeys(Keys.Control + "a");
            newFolderText.SendKeys(Keys.Delete);
            newFolderText.SendKeys(text);

            // OK button
            IWebElement OKbutton = driver.FindElement(By.Id("OK-button"));
            OKbutton.Click();

            // Wait for alert for 3 seconds
            wait.Until(ExpectedConditions.AlertIsPresent());

            // Get alert text
            IAlert alert = driver.SwitchTo().Alert();

            string alertText = alert.Text;

            Assert.AreEqual(alertText, message);
            Console.WriteLine(alertText);

            alert.Accept();
        }

        [TestMethod]
        public void UploadFile()
        {
            // Wait for alert for 3 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

            string expectedText = "";
            Thread.Sleep(1000);
            IWebElement table = driver.FindElement(By.XPath("//table"));
            ReadOnlyCollection<IWebElement> rows = table.FindElements(By.TagName("tr"));

            foreach (var row in rows)
            {
                if (row.Text.Split(' ')[0].Equals("a.txt"))
                {
                    expectedText = "the file name: 'a.txt' is already exist!";
                    break;
                }
            }

            // New navbar
            IWebElement newNavbar = driver.FindElement(By.Id("new"));
            newNavbar.Click();

            // New File Item of New Navbar
            IWebElement newFolderItem = driver.FindElement(By.Id("new-file"));
            newFolderItem.Click();

            // file input
            IWebElement fileInput = driver.FindElement(By.Id("file-input"));

            // file paths
            string filePath = @"C:\a.txt";
            fileInput.SendKeys(filePath);

            // Wait for alert for 3 seconds
            wait.Until(ExpectedConditions.AlertIsPresent());

            // Get alert text
            IAlert alert = driver.SwitchTo().Alert();

            string alertText = alert.Text;
            alert.Accept();

            if (expectedText.Equals(""))
            {
                expectedText = "file added succesfully";
            }

            Assert.AreEqual(alertText, expectedText);
            Console.WriteLine(expectedText);
        }

    }
}
