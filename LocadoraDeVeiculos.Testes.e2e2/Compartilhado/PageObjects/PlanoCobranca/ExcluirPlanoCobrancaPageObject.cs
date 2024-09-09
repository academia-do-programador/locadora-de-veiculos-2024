using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;

public class ExcluirPlanoCobrancaPageObject : ExcluirBasePageObject
{
    public ExcluirPlanoCobrancaPageObject(IWebDriver driver) : base(driver)
    {
    }

    public override string NomeModulo => "PlanoCobranca";
}