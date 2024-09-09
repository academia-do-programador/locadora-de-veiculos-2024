using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LocadoraDeVeiculos.Testes.e2e.Taxa;

[TestClass]
public class AoInserirTaxa
{


    [TestMethod]
    public void Dado_info_validas_deve_mostrar_Taxa_na_listagem()
    {
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl("http://localhost:5125/Autenticacao/Registrar");

        var inputUsuario = driver.FindElement(By.Id("Usuario"));
        inputUsuario.SendKeys("usuario");

        var inputEmail = driver.FindElement(By.Id("Email"));
        inputEmail.SendKeys("email@gmail.com");

        var inputSenha = driver.FindElement(By.Id("Senha"));
        inputSenha.SendKeys("senha");

        var inputConfirmarSenha = driver.FindElement(By.Id("ConfirmarSenha"));
        inputConfirmarSenha.SendKeys("senha");

        var botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));
        botaoRegistrar.Click();

        driver.Navigate().GoToUrl("http://localhost:5125/Taxa/Inserir");

        IWebElement inputNome = driver.FindElement(By.Id("Nome"));
        inputNome.Clear();
        inputNome.SendKeys("Limpeza");

        IWebElement inputValor = driver.FindElement(By.Id("Valor"));
        inputValor.Clear();
        inputValor.SendKeys("100");

        IWebElement botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));
        botaoGravar.Click();

        Assert.IsTrue(driver.PageSource.Contains("Listagem de Taxas"));
        Assert.IsTrue(driver.PageSource.Contains("Valor: R$ 100,00"));

        driver.Quit();
    }
}