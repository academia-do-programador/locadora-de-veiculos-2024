using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects;

public abstract class ExcluirBasePageObject
{
    private readonly IWebDriver driver;
    private readonly By byBotaoConfirmar;

    public ExcluirBasePageObject(IWebDriver driver)
    {
        this.driver = driver;

        byBotaoConfirmar = By.CssSelector(".btn-danger");
    }

    public abstract string NomeModulo
    {
        get;
    }

    public void Visitar(int id)
    {
        var endereco = $"{TestFixture.EnderecoBase}/{NomeModulo}/Excluir/{id}";

        driver.Navigate().GoToUrl(endereco);
    }

    public void ConfirmarExclusao()
    {
        driver.FindElement(byBotaoConfirmar).Click();
    }


}
