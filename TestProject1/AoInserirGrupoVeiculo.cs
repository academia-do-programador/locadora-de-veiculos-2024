using OpenQA.Selenium;

namespace TestProject1;

[TestClass]
public class AoInserirGrupoVeiculo : TestFixture
{
    [TestMethod]
    public void Dado_info_validas_deve_apresentar_GrupoVeiculo_na_listagem()
    {
        //arrange
        driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");

        IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
        inputUsuario.SendKeys("rech");

        IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
        inputEmail.SendKeys("rech@gmail.com");

        IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
        inputSenha.SendKeys("rech@123");

        IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
        inputConfirmarSenha.SendKeys("rech@123");

        IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

        botaoRegistrar.Click();

        driver.Navigate().GoToUrl("http://localhost:5125/GrupoVeiculos/inserir");

        IWebElement inputNome = driver.FindElement(By.CssSelector("input#Nome"));
        inputNome.Clear();
        inputNome.SendKeys("Esportivo");

        IWebElement botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));

        //action
        botaoGravar.Click();

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Listagem de Grupos de Veículos"));
        Assert.IsTrue(driver.PageSource.Contains("Esportivo"));
    }

}