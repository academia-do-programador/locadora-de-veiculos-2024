using Dapper;
using OpenQA.Selenium;

namespace TestProject1;

[TestClass]
public class AoRegistrarUsuario : TestFixture
{

    [TestMethod]
    public void Dado_info_validas_deve_apresentar_a_tela_inicial()
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

        //action
        botaoRegistrar.Click();

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Página Inicial"));
        Assert.IsTrue(driver.PageSource.Contains("Seja bem-vindo(a)!"));
    }

    [TestMethod]
    public void Dado_info_invalidas_deve_permanecer_na_tela_de_registro()
    {
        //arrange
        driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");

        IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
        inputUsuario.SendKeys("");

        IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
        inputEmail.SendKeys("rech@gmail.com");

        IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
        inputSenha.SendKeys("rech@123");

        IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
        inputConfirmarSenha.SendKeys("rech@123");

        IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

        //action
        botaoRegistrar.Click();

        //assert

        IWebElement spanErro = driver.FindElement(By.CssSelector("span.field-validation-error[data-valmsg-for='Usuario']"));

        Assert.AreEqual("O usuário é obrigatório", spanErro.Text);

        Assert.IsTrue(driver.PageSource.Contains("Registro de Usuário"));
    }

   
}