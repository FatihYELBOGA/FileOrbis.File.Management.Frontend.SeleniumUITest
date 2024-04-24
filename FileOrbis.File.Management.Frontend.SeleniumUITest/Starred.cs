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

        }

        [TestMethod]
        public void RemoveFileInStarred()
        {

        }

        [TestMethod]
        public void GotoFolder()
        {

        }

        [TestMethod]
        public void GotoFile()
        {

        }

    }

}
