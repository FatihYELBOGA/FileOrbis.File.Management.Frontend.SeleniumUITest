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
    public class Trash
    {
        private readonly Login login;
        private readonly IWebDriver driver;

        public Trash()
        {
            login = new Login();

            // successfully login
            login.SigninPositive();

            driver = login.getDriver();
        }

        [TestMethod]
        public void RestoreFolder()
        {

        }

        [TestMethod]
        public void RestoreFile()
        {

        }

        [TestMethod]
        public void DeleteForeverFolder()
        {

        }

        [TestMethod]
        public void DeleteForeverFile()
        {

        }

    }

}
