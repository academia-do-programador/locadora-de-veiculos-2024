using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;

public class ExcluirGrupoVeiculoPageObject
{
    private readonly IWebDriver driver;
    private readonly By byBotaoConfirmar;

    public ExcluirGrupoVeiculoPageObject(IWebDriver driver)
    {
        this.driver = driver;

        byBotaoConfirmar = By.CssSelector(".btn-danger");
    }

    public void Visitar(int id)
    {
        var endereco = $"{TextFixture.EnderecoBase}/GrupoVeiculos/Excluir/{id}";

        driver.Navigate().GoToUrl(endereco);
    }

    public void ConfirmarExclusao()
    {
        driver.FindElement(byBotaoConfirmar).Click();
    }
}
