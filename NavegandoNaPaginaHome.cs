using Alura.ByteBank.WebApp.Testes.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome
    {

        private IWebDriver driver;

        public NavegandoNaPaginaHome()
        {
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void CarregadaPaginaHomeVerificaTitle()
        {
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/");

            Assert.Contains("WebApp", driver.Title);
        }

        [Fact]

        public void CarregadaPaginaHomeVerificaExistenciaLinkLoginEHome()
        {
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/");

            Assert.Contains("Login", driver.PageSource);
            Assert.Contains("Home", driver.PageSource);
        }

        [Fact]
        public void LogandoNoSistema()
        {
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/");
            driver.Manage().Window.Size = new System.Drawing.Size(1448, 656);
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("Senha")).SendKeys(Keys.Enter);
        }

        [Fact]

        public void ValidaLinkDeLoginNaHome()
        {
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/");

            var linkLogin = driver.FindElement(By.LinkText("Login"));

            linkLogin.Click();

            Assert.Contains("img", driver.PageSource);
        }

        [Fact]

        public void TentaAcessarPaginaSemEstarLogado()
        {
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/Agencia/Index");

            Assert.Contains("https://localhost:5001/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);
        }
    }


}
