using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace FileOrbis.File.Management.Frontend.SeleniumUITest
{

    [TestClass]
    public class Login
    {
        private readonly IWebDriver driver;

        public Login()
        {
            driver = new EdgeDriver();

            // Front-end test URL
            driver.Navigate().GoToUrl("http://localhost:3000");
        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        [TestMethod]
        public void SigninPositive()
        {
            // Username and password
            IWebElement usernameInput = driver.FindElement(By.Id("email"));
            usernameInput.SendKeys("korhankonaray@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("korhan123");

            // Sign in button
            IWebElement loginButton = driver.FindElement(By.Id("signin"));
            loginButton.Click();

            // Wait for alert for 3 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.AlertIsPresent());

            // Get alert text
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Console.WriteLine(alertText);

            string successAlert = "succesfully login";

            // Check if success alert and alert text are equal
            Assert.AreEqual(alertText, successAlert);

            // Accept alert
            alert.Accept();
        }

        [TestMethod]
        public void SigninNegative()
        {
            // Username and password
            IWebElement usernameInput = driver.FindElement(By.Id("email"));
            usernameInput.SendKeys("korhankonaray@gmail.com");

            IWebElement passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("korhan12345");

            // Sign in button
            IWebElement loginButton = driver.FindElement(By.Id("signin"));
            loginButton.Click();

            // Wait for alert for 3 seconds
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
            wait.Until(ExpectedConditions.AlertIsPresent());

            // Get alert text
            IAlert alert = driver.SwitchTo().Alert();
            string alertText = alert.Text;
            Console.WriteLine(alertText);

            string failAlert = "username or password is mistake!";

            // Check if success alert and alert text are equal
            Assert.AreEqual(alertText, failAlert);

            // Accept alert
            alert.Accept();
        }
    }
}