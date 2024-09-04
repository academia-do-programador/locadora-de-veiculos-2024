using Dapper;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.PageObjects;

public class FormularioGrupoVeiculoPageObject
{
    private string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

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
        var endereco = "http://localhost:5125/GrupoVeiculos/" + (id == 0 ? "Inserir" : $"Editar/{id}");

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

    public int GetGrupoVeiculoId(string nome)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            var query = "SELECT TOP 1 ID FROM TBGRUPOVEICULOS WHERE Nome = @Nome";
            return connection.QuerySingleOrDefault<int>(query, new { Nome = nome });
        }
    }

    public void ExcluirRegistro(string nome)
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            connection.Execute("DELETE FROM TBGRUPOVEICULOS WHERE NOME LIKE @NOME", new { Nome = nome });
        }
    }
}

