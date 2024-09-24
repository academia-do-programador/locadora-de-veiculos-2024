using LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace LocadoraDeVeiculos.Testes.Interface.Compartilhado;

[TestClass]
public class TestFixture
{
    protected static ChromeDriver driver;

    //[ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext context)
    {
        driver = new ChromeDriver();

        RegistrarUsuarioPageObject registrarUsuarioPage = new RegistrarUsuarioPageObject(driver);
        registrarUsuarioPage.Visitar();
        registrarUsuarioPage.PreecherFormulario("rech", "rech@gmail.com", "123", "123");
        registrarUsuarioPage.SubmeterFormulario();

        Debug.Print("AssemblyInitialize");
    }

    [TestCleanup]
    public void TestCleanup()
    {
        LimparTabelas("DELETE FROM [TBGRUPOVEICULOS];");

        Debug.Print("TestInitialize");
    }

    private static void LimparTabelas(string sql)
    {
        string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

        SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);
        conexaoComBanco.Open();
        SqlCommand comandoLimparTabelas = conexaoComBanco.CreateCommand();
        comandoLimparTabelas.CommandText = sql;
        comandoLimparTabelas.ExecuteNonQuery();
        conexaoComBanco.Close();
    }

    //[ClassCleanup(ClassCleanupBehavior.EndOfClass)]
    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        LimparTabelas("DELETE FROM [ASPNETROLES]; DELETE FROM [ASPNETUSERS];");

        driver.Quit();

        Debug.Print("AssemblyCleanup");
    }

}