using Dapper;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.PlanoCobranca;

public class FormularioPlanoCobrancaPageObject
{
    public Dictionary<string, IWebElement> Erros = new();

    private readonly IWebDriver driver;

    private readonly By byInputGrupoVeiculo;

    private readonly By byInputPrecoDiarioPlanoDiario;
    private readonly By byInputPrecoQuilometroPlanoDiario;

    private readonly By byInputQuilometrosDisponiveisPlanoControlado;
    private readonly By byInputPrecoDiarioPlanoControlado;
    private readonly By byInputPrecoQuilometroExtrapoladoPlanoControlado;


    private readonly By byInputPrecoDiarioPlanoLivre;


    private readonly By byBotaoGravar;


    public FormularioPlanoCobrancaPageObject(IWebDriver driver)
    {
        this.driver = driver;

        byInputGrupoVeiculo = By.Id("GrupoVeiculosId");

        byInputPrecoDiarioPlanoDiario = By.Id("PrecoDiarioPlanoDiario");
        byInputPrecoQuilometroPlanoDiario = By.Id("PrecoQuilometroPlanoDiario");

        byInputQuilometrosDisponiveisPlanoControlado = By.Id("QuilometrosDisponiveisPlanoControlado");
        byInputPrecoDiarioPlanoControlado = By.Id("PrecoDiarioPlanoControlado");
        byInputPrecoQuilometroExtrapoladoPlanoControlado = By.Id("PrecoQuilometroExtrapoladoPlanoControlado");

        byInputPrecoDiarioPlanoLivre = By.Id("PrecoDiarioPlanoLivre");

        byBotaoGravar = By.CssSelector("button.btn-primary");
    }

    public void Visitar(int id = 0)
    {
        var endereco = $"{TextFixture.EnderecoBase}/PlanoCobranca/" + (id == 0 ? "Inserir" : $"Editar/{id}");

        driver.Navigate().GoToUrl(endereco);
    }

    public void PreencherFormulario(string grupoVeiculo, 
        string precoDiarioPlanoDiario, 
        string precoQuilometroPlanoDiario,
        string quilometrosDisponiveisPlanoControlado, 
        string precoDiarioPlanoControlado, 
        string precoQuilometroExtrapoladoPlanoControlado,
        string precoDiarioPlanoLivre)
    {
        driver.FindElement(byInputGrupoVeiculo).SendKeys(grupoVeiculo);

        //Plano Diario
        driver.FindElement(byInputPrecoDiarioPlanoDiario).Clear();
        driver.FindElement(byInputPrecoDiarioPlanoDiario).SendKeys(precoDiarioPlanoDiario);

        driver.FindElement(byInputPrecoQuilometroPlanoDiario).Clear();
        driver.FindElement(byInputPrecoQuilometroPlanoDiario).SendKeys(precoQuilometroPlanoDiario);

        //Plano controlado
        driver.FindElement(byInputQuilometrosDisponiveisPlanoControlado).Clear();
        driver.FindElement(byInputQuilometrosDisponiveisPlanoControlado).SendKeys(quilometrosDisponiveisPlanoControlado);

        driver.FindElement(byInputPrecoDiarioPlanoControlado).Clear();
        driver.FindElement(byInputPrecoDiarioPlanoControlado).SendKeys(precoDiarioPlanoControlado);

        driver.FindElement(byInputPrecoQuilometroExtrapoladoPlanoControlado).Clear();
        driver.FindElement(byInputPrecoQuilometroExtrapoladoPlanoControlado).SendKeys(precoQuilometroExtrapoladoPlanoControlado);

        //Plano Livre
        driver.FindElement(byInputPrecoDiarioPlanoLivre).Clear();
        driver.FindElement(byInputPrecoDiarioPlanoLivre).SendKeys(precoDiarioPlanoLivre);
    }

    public void SubmeterFormulario()
    {
        var botaoGravar = driver.FindElement(byBotaoGravar);

        new Actions(driver)
            .ScrollToElement(botaoGravar)
            .Perform();
        
        botaoGravar.Click();

        string[] campos = [
            "GrupoVeiculosId", 
            "PrecoDiarioPlanoDiario", 
            "PrecoQuilometroPlanoDiario",
            "QuilometrosDisponiveisPlanoControlado", 
            "PrecoDiarioPlanoControlado",
            "PrecoQuilometroExtrapoladoPlanoControlado", 
            "PrecoDiarioPlanoLivre"
        ];

        foreach (var campo in campos)
        {
            var bySpanErro = By.CssSelector($"span.field-validation-error[data-valmsg-for='{campo}']");

            try
            {
                Erros[campo] = driver.FindElement(bySpanErro);
            }
            catch (NoSuchElementException) { }
        }
    }

    public int GetId(string precoDiarioPlanoDiario)
    {
        using (var connection = new SqlConnection(TextFixture.ConnectionString))
        {
            connection.Open();
            var query = "SELECT TOP 1 ID FROM TBPLANOCOBRANCA WHERE PRECODIARIOPLANODIARIO = @PRECODIARIOPLANODIARIO";
            return connection.QuerySingleOrDefault<int>(query, new { PrecoDiarioPlanoDiario = precoDiarioPlanoDiario });
        }
    }
}

