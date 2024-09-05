using Dapper;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;

public class FormularioGrupoVeiculoPageObject
{
    private IWebDriver driver;
    private By byInputNome;
    private By byBotaoGravar;

    public Dictionary<string, IWebElement> Erros = new();

    public FormularioGrupoVeiculoPageObject(IWebDriver driver)
    {
        this.driver = driver;

        byInputNome = By.Id("Nome");
        byBotaoGravar = By.CssSelector(".btn-primary");
    }

    public void Visitar(int id = 0)
    {
        var endereco = $"{TextFixture.EnderecoBase}/GrupoVeiculos/" + (id == 0 ? "Inserir" : $"Editar/{id}");

        driver.Navigate().GoToUrl(endereco);
    }

    public void PreencherFormulario(string nome)
    {
        driver.FindElement(byInputNome).Clear();
        driver.FindElement(byInputNome).SendKeys(nome);
    }

    public void SubmeterFormulario()
    {
        driver.FindElement(byBotaoGravar).Click();

        string[] campos = ["Nome"];

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

    public int GetId(string nome)
    {
        using (var connection = new SqlConnection(TextFixture.ConnectionString))
        {
            connection.Open();
            var query = "SELECT TOP 1 ID FROM TBGRUPOVEICULOS WHERE Nome = @Nome";
            return connection.QuerySingleOrDefault<int>(query, new { Nome = nome });
        }
    }
}

