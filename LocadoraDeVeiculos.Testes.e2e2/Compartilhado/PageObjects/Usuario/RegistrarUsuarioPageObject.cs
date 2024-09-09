using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Usuario;

public class RegistrarUsuarioPageObject
{
    private IWebDriver driver;

    public RegistrarUsuarioPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl($"{TestFixture.EnderecoBase}/Autenticacao/Registrar");
    }

    public void PreencherFormulario(string usuario, string email, string senha, string confirmarSenha)
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
        botaoRegistrar.Click();
    }
}
