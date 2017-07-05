using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Vebinar4._7
{
    [TestFixture]
    public class findTitle
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void FindTitle()
        {
            driver.Url = "http://localhost/litecart/admin";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("remember_me")).Click();
            driver.FindElement(By.Name("login")).Click();
            var m = driver.FindElement(By.Id("box-apps-menu-wrapper")).FindElements(By.TagName("li"));
            for (int i = 0; i < m.Count; i++)
            {
                string title = m[i].Text;
                m[i].Click();

                var mm = driver.FindElements(By.XPath("//li//ul//li"));

                if (mm.Count == 0 || title == "Settings")
                {
                    if (!driver.FindElement(By.TagName("h1")).Text.Contains(title))
                        NUnit.Framework.Assert.Fail(title +" " + driver.FindElement(By.TagName("h1")).Text);
                }
                else
                {
                    for (int j = 0; j < mm.Count; j++)
                    {
                        string _title = mm[j].Text;
                        mm[j].Click();

                        if (_title == "Background Jobs") _title = "Job Modules";

                        if (!driver.FindElement(By.TagName("h1")).Text.Contains(_title))                          
                            NUnit.Framework.Assert.Fail(_title + driver.FindElement(By.TagName("h1")).Text);
                        mm = driver.FindElements(By.XPath("//li//ul//li"));
                    }
                }
                driver.FindElement(By.ClassName("logotype")).Click();
                m = driver.FindElement(By.Id("box-apps-menu-wrapper")).FindElements(By.TagName("li"));
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
