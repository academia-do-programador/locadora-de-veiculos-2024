using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;

public class RegistrarUsuarioPageObject
{
    private IWebDriver driver;

    public RegistrarUsuarioPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl("http://localhost:5125/Autenticacao/Registrar");
    }

    public void PreecherFormulario(string usuario, string email, string senha, string confirmarSenha)
    {
        var inputUsuario = driver.FindElement(By.Id("Usuario"));
        inputUsuario.SendKeys(usuario);

        var inputEmail = driver.FindElement(By.Id("Email"));
        inputEmail.SendKeys(email);

        var inputSenha = driver.FindElement(By.Id("Senha"));
        inputSenha.SendKeys(senha);

        var inputConfirmarSenha = driver.FindElement(By.Id("ConfirmarSenha"));
        inputConfirmarSenha.SendKeys(confirmarSenha);
    }

    public void SubmeterFormulario()
    {
        var botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

        //action
        botaoRegistrar.Click();
    }
}