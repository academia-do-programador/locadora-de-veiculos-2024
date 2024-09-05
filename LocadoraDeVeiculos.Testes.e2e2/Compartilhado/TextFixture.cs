using Dapper;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado;

[TestClass]
public class TextFixture : IDisposable
{
    protected static IWebDriver driver;
    protected static TestContext context;

    public static string EnderecoBase = "http://localhost:5125";
    public static string ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";
    
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext ctx)
    {
        context = ctx;
        var opt = new ChromeOptions();
        opt.AddArguments("--headless=new");

        driver = new ChromeDriver();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        driver.Quit();
    }

    public void Dispose()
    {
        LimparTabelas();
    }

    public void LimparTabelas()
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            connection.Execute(
                @"DELETE FROM [DBO].[TBCLIENTE];
                  DELETE FROM [DBO].[TBTAXA]
                  DELETE FROM [DBO].[TBVEICULO]
                  DELETE FROM [DBO].[TBPLANOCOBRANCA]
                  DELETE FROM [DBO].[TBGRUPOVEICULOS]");
        }
    }
}
