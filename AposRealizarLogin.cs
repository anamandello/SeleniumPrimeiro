using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Alura.ByteBank.WebApp.Testes.PageObjects;
using Alura.ByteBank.WebApp.Testes.Utilitarios;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class AposRealizarLogin:IClassFixture<Gerenciador>
    {
        private IWebDriver driver;
        public ITestOutputHelper SaidaConsoleTeste;

        public AposRealizarLogin(Gerenciador gerenciador, ITestOutputHelper _saidaConsoleTeste)
        {
            driver = gerenciador.Driver;
            SaidaConsoleTeste = _saidaConsoleTeste;
        }

        [Fact]

        public void AposRealizarLoginVerificaSeExisteOpcaoAgenciaMenu()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            Assert.Contains("Agência", driver.PageSource);
        }

        [Fact]

        public void TentaRealizarLoginSemDados()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("", "");
            loginPO.btnClick();

            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
        }

        [Fact]

        public void TentaRealizarLoginComSenhaInvalida()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("andre@email.com", "senha021");
            loginPO.btnClick();

            Assert.Contains("Login", driver.PageSource);
        }

        [Fact]

        public void RealizarLoginAcessaMenuECadastraCliente()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            driver.FindElement(By.LinkText("Cliente")).Click();

            driver.FindElement(By.LinkText("Adicionar Cliente")).Click();

            driver.FindElement(By.Name("Identificador")).Click();
            driver.FindElement(By.Name("Identificador")).SendKeys("5884c0bc-f97b-4e26-94b4-dfd81889227f");
            driver.FindElement(By.Name("CPF")).Click();
            driver.FindElement(By.Name("CPF")).SendKeys("27146934071");
            driver.FindElement(By.Name("Nome")).Click();
            driver.FindElement(By.Name("Nome")).SendKeys("Alfredo de Souza");
            driver.FindElement(By.Name("Profissao")).Click();
            driver.FindElement(By.Name("Profissao")).SendKeys("Cientista de Dados");

            driver.FindElement(By.CssSelector(".btn-primary")).Click();
            driver.FindElement(By.LinkText("Home")).Click();
            

            Assert.Contains("Logout", driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContas()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);
            var homePO = new HomePO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            homePO.LinkContaCorrenteClick();

            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("a"));


            var elemento = (from webElemento in elements
                            where webElemento.Text.Contains("Detalhes")
                            select webElemento).First();

            elemento.Click();

            Assert.Contains("Voltar", driver.PageSource);
            //foreach(IWebElement e in elements)
            //{
            //    SaidaConsoleTeste.WriteLine(e.Text);
            //}
        }

        [Fact]
        public void RealizarLoginAcessaListagemDeContasBuscaTexto()
        {
            var loginPO = new LoginPO(driver);
            var navegacaoPO = new NavegacaoPO(driver);
            var homePO = new HomePO(driver);

            navegacaoPO.Navegar("https://localhost:5001/UsuarioApps/Login");

            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.btnClick();

            homePO.LinkContaCorrenteClick();

            Assert.Contains("Adicionar Conta-Corrente", driver.PageSource);
        }
    }
}
