using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;

public class ForumularioGrupoVeiculoPageObject
{
    private readonly IWebDriver driver;

    public ForumularioGrupoVeiculoPageObject(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void Visitar()
    {
        driver.Navigate().GoToUrl("http://localhost:5125/GrupoVeiculos/Inserir");
    }

    public void PreencherFormulario(string nomeGrupo)
    {
        var inputUsuario = driver.FindElement(By.Id("Nome"));
        inputUsuario.SendKeys(nomeGrupo);
    }

    public void SubmeterFormulario()
    {
        var botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));

        //action
        botaoGravar.Click();
    }
}