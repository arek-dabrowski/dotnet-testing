using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using System;

namespace ASPMVC.SeleniumTests
{
    [TestFixture]
    public class PatientsViewTest
    {
        private static string NAME = "Lucznik";
        private static string COUNTRY = "Poland";
        private static string HQ = "Poland";
        private static string FOUNDDATE = "21-03-1995";

        IWebDriver driver;
        INavigation navigation;
        String url;

        [SetUp]
        public void Setup()
        {
            driver = new InternetExplorerDriver();
            navigation = driver.Navigate();
            url = "http://localhost:5000/";

            navigation.GoToUrl(url);
            LoginAsAdmin();
        }

        [Test]
        public void CreateNewManufacturer_SeleniumTest_ShouldPass()
        {
            CreateManufacturer(NAME);

            var value = driver.FindElement(By.XPath("//tr/td[contains(text(),'" + NAME + "')]")).Text;
            StringAssert.Contains(value, NAME);

            DeleteManufacturer(NAME);
        }

        [Test]
        public void CreateNewManufacturer_SeleniumTest_ShouldFailIfFirstLetterIsNotCapital()
        {
            var wrongName = "lucznik";
            CreateManufacturer(wrongName);

            var value = driver.FindElement(By.XPath("/html/body/div[2]/main/div[1]/div/form/div[1]/span")).Text;
            StringAssert.Contains(value, "Name first letter have to be capital.");
        }

        [Test]
        public void EditNewManufacturer_SeleniumTest_ShouldPass()
        {
            CreateManufacturer(NAME);

            var editButton = GetButtonByName("Edit", NAME);
            editButton.Click();

            var newName = "Loocznik";
            EditManufacturer(newName);

            var value = driver.FindElement(By.XPath("//tr/td[contains(text(),'" + newName + "')]")).Text;
            StringAssert.Contains(value, newName);

            DeleteManufacturer(newName);
        }

        [Test]
        public void AccountSettings_SeleniumTest_ShouldPass()
        {
            var account = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[1]/li[1]/a"));
            account.Click();

            var currentPage = driver.Url;
            StringAssert.AreEqualIgnoringCase("https://localhost:5001/Identity/Account/Manage", currentPage);
        }

        [TearDown]
        public void Teardown()
        {
            var logout = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[1]/li[2]/form/button"));
            logout.Click();
            driver.Close();
        }

        #region Helpers

        private void LoginAsAdmin()
        {
            var login = driver.FindElement(By.LinkText("Login"));
            login.Click();

            var emailField = driver.FindElement(By.Id("Input_Email"));
            emailField.Click();
            emailField.SendKeys("admin@asp.net");

            var passwordField = driver.FindElement(By.Id("Input_Password"));
            passwordField.Click();
            passwordField.SendKeys("Zaq123$%^");

            var loginButton = driver.FindElement(By.XPath("//*[@id=\"account\"]/div[5]/button"));
            loginButton.Click();
        }

        private void CreateManufacturer(string name)
        {
            var manufacturers = driver.FindElement(By.XPath("/html/body/div[1]/a[3]"));
            manufacturers.Click();

            var createNewButton = driver.FindElement(By.XPath("/html/body/div[2]/main/p/a"));
            createNewButton.Click();

            var nameField = driver.FindElement(By.Id("Name"));
            nameField.Click();
            nameField.SendKeys(name);

            var countryField = driver.FindElement(By.Id("Country"));
            countryField.Click();
            countryField.SendKeys(COUNTRY);

            var headquartersField = driver.FindElement(By.Id("Headquarters"));
            headquartersField.Click();
            headquartersField.SendKeys(HQ);

            var founddateField = driver.FindElement(By.Id("FoundDate"));
            founddateField.Click();
            founddateField.SendKeys(FOUNDDATE);

            var createButton = driver.FindElement(By.XPath("/html/body/div[2]/main/div[1]/div/form/div[5]/input"));
            createButton.Click();
        }

        private void EditManufacturer(string name)
        {
            var nameField = driver.FindElement(By.Id("Name"));
            nameField.Clear();
            nameField.SendKeys(name);

            var save = driver.FindElement(By.XPath("/html/body/div[2]/main/div[1]/div/form/div[5]/input"));
            save.Click();
        }

        private void DeleteManufacturer(string name)
        {
            var deleteButton = GetButtonByName("Delete", name);
            deleteButton.Click();

            var delete = driver.FindElement(By.XPath("/html/body/div[2]/main/div/form/input[2]"));
            delete.Click();
        }

        private IWebElement GetButtonByName(string operation, string name)
        {
            return driver.FindElement(By.XPath("//tr/td[contains(text(),'" + name + "')]" +
                "/following-sibling::td/a[contains(text(),'" + operation + "')]"));
        }

        #endregion
    }
}
