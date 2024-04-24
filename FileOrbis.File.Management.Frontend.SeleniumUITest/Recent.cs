using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        [TestMethod]
        public void GotoFile()
        {

        }

    }

}
