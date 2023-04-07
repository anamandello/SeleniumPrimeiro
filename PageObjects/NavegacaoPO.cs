using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ByteBank.WebApp.Testes.PageObjects
{
    public class NavegacaoPO
    {
        private IWebDriver driver;

        public NavegacaoPO(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navegar(string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}
