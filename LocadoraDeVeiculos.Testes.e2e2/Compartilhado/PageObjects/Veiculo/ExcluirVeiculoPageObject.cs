using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Veiculo;

public class ExcluirVeiculoPageObject : ExcluirBasePageObject
{
    public ExcluirVeiculoPageObject(IWebDriver driver) : base(driver)
    {
    }

    public override string NomeModulo => "Veiculo";
}
