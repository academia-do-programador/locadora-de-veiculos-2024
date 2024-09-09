using OpenQA.Selenium;

namespace TestProject1;

public class RegistrarUsuarioPageObject
{
    private IWebDriver driver;

    public RegistrarUsuarioPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/registrar");
    }

    public void PreencherFormulario(string usuario, string email, string senha, string confirmarSenha)
    {
        IWebElement inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
        inputUsuario.SendKeys(usuario);

        IWebElement inputEmail = driver.FindElement(By.CssSelector("input#Email"));
        inputEmail.SendKeys(email);

        IWebElement inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
        inputSenha.SendKeys(senha);

        IWebElement inputConfirmarSenha = driver.FindElement(By.CssSelector("input#ConfirmarSenha"));
        inputConfirmarSenha.SendKeys(confirmarSenha);
    }

    public void SubmeterFormulario()
    {
        IWebElement botaoRegistrar = driver.FindElement(By.CssSelector(".btn-primary"));

        botaoRegistrar.Click();
    }
}

public class FazerLogoutPageObject
{
    private IWebDriver driver;

    public FazerLogoutPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl("http://localhost:5125/home/index");
    }

    public void SubmeterFormulario()
    {
        IWebElement botaoUsuarioLogado = driver.FindElement(By.Id("dropdownUser1"));

        botaoUsuarioLogado.Click();

        //WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

        //IWebElement botaoLogout =
        //    driverWait.Until(driver => driver.FindElement(By.CssSelector("button.dropdown-item")));

        IWebElement botaoLogout = driver.FindElement(By.CssSelector("button.dropdown-item"));

        //action
        botaoLogout.Click();
    }
}

public class FazerLoginPageObject
{
    private IWebDriver driver;

    public FazerLoginPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl("http://localhost:5125/autenticacao/login");
    }

    public void PreencherFormulario(string usuario, string senha)
    {
        var inputUsuario = driver.FindElement(By.CssSelector("input#Usuario"));
        inputUsuario.SendKeys(usuario);

        var inputSenha = driver.FindElement(By.CssSelector("input#Senha"));
        inputSenha.SendKeys(senha);
    }

    public void SubmeterFormulario()
    {
        IWebElement botaoLogin = driver.FindElement(By.CssSelector(".btn-primary"));

        botaoLogin.Click();
    }
}

[TestClass]
public class AoFazerLogout : TestFixture
{
    [TestMethod]
    public void Dado_info_validas_deve_apresentar_a_tela_de_login()
    {
        //arrange
        var registroPage = new RegistrarUsuarioPageObject(driver);
        registroPage.Visitar();
        registroPage.PreencherFormulario("rech", "rech@gmail.com", "123", "123");
        registroPage.SubmeterFormulario();

        var logoutPage = new FazerLogoutPageObject(driver);
        logoutPage.Visitar();
        logoutPage.SubmeterFormulario();

        var loginPage = new FazerLoginPageObject(driver);
        loginPage.Visitar();
        loginPage.PreencherFormulario("rech", "123");
        loginPage.SubmeterFormulario();

        Thread.Sleep(2000);

        //assert
        Assert.IsTrue(driver.PageSource.Contains("Página Inicial"));
        Assert.IsTrue(driver.PageSource.Contains("Seja bem-vindo(a)!"));
    }
}