using Dapper;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Veiculo;

public class FormularioVeiculoPageObject
{
    IWebDriver driver;

    private By byInputFoto;
    private By byInputModelo;
    private By byInputMarca;
    private By byInputTipoCombustivel;
    private By byInputCapacidadeTanque;
    private By byInputGrupoVeiculosId;

    private By byBotaoGravar;


    public FormularioVeiculoPageObject(IWebDriver driver)
    {
        byInputFoto = By.Id("Foto");
        byInputModelo = By.Id("Modelo");
        byInputMarca = By.Id("Marca");
        byInputTipoCombustivel = By.Id("TipoCombustivel");
        byInputCapacidadeTanque = By.Id("CapacidadeTanque");
        byInputGrupoVeiculosId = By.Id("GrupoVeiculosId");

        byBotaoGravar = By.CssSelector(".btn-primary");

        this.driver = driver;
    }

    public void Visitar(int id = 0)
    {
        driver.Navigate().GoToUrl($"{TestFixture.EnderecoBase}/Veiculo/" + (id == 0 ? "Inserir" : "Editar/" + id));
    }

    public void PreencherFormulario(
        string foto,
        string modelo,
        string marca,
        string tipoCombustivel,
        string capacidadeTanque,
        string nomeGrupoVeiculo)
    {
        driver.FindElement(byInputFoto).SendKeys(foto);
        driver.FindElement(byInputModelo).SendKeys(modelo);
        driver.FindElement(byInputMarca).SendKeys(marca);
        driver.FindElement(byInputTipoCombustivel).SendKeys(tipoCombustivel);
        driver.FindElement(byInputCapacidadeTanque).SendKeys(capacidadeTanque);
        driver.FindElement(byInputGrupoVeiculosId).SendKeys(nomeGrupoVeiculo);
    }

    public void SubmeterFormulario()
    {
        driver.FindElement(byBotaoGravar).Click();
    }

    public int GetId(string modelo)
    {
        using (var connection = new SqlConnection(TestFixture.ConnectionString))
        {
            connection.Open();
            string sql = "SELECT TOP 1 ID FROM TBVEICULO WHERE MODELO = @MODELO";
            return connection.QueryFirstOrDefault<int>(sql, new { Modelo = modelo });
        }
    }
}