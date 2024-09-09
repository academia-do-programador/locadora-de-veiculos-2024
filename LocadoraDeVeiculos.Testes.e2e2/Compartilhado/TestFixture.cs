using Dapper;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Usuario;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado;

[TestClass]
public class TestFixture : IDisposable
{
    protected static IWebDriver driver;
    protected static TestContext context;

    public static string EnderecoBase = "http://localhost:5125";
    public static string ConnectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

    //[AssemblyInitialize]
    public static void AssemblyInitialize(TestContext ctx)
    {
        context = ctx;
        var opt = new ChromeOptions();
        opt.AddArguments("--headless=new");

        driver = new ChromeDriver();

        var registrarUsuario = new RegistrarUsuarioPageObject(driver);
        registrarUsuario.Visitar();
        registrarUsuario.PreencherFormulario("rech", "rech@gmail.com", "rech@123", "rech@123");
        registrarUsuario.SubmeterFormulario();

        Debug.Print("Running AssemblyInitialize");
    }

    //[AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        Debug.Print("Running AssemblyCleanup");

        LimparTabelaUsuario();

        driver.Quit();
    }

    public void Dispose()
    {
        //LimparTabelas();
    }

    private static void LimparTabelaUsuario()
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            connection.Execute(
                @"DELETE FROM [DBO].[ASPNETUSERS];");
        }
    }

    public void LimparTabelas()
    {
        using (var connection = new SqlConnection(ConnectionString))
        {
            connection.Open();

            connection.Execute(
                @"DELETE FROM [dbo].[TBLOCACAOTAXA];
                  DELETE FROM [dbo].[TBLOCACAO];
                  DELETE FROM [dbo].[TBCONDUTOR];                                                      
                  DELETE FROM [DBO].[TBCLIENTE];
                  DELETE FROM [DBO].[TBTAXA]
                  DELETE FROM [DBO].[TBVEICULO]
                  DELETE FROM [DBO].[TBPLANOCOBRANCA]
                  DELETE FROM [DBO].[TBGRUPOVEICULOS]
                  DELETE FROM [DBO].[ASPNETUSERROLES]
                  DELETE FROM [dbo].[ASPNETROLES]");
        }
    }
}
