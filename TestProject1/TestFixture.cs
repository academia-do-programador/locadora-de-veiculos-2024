using Microsoft.Data.SqlClient;
using OpenQA.Selenium.Chrome;

namespace TestProject1;

[TestClass]
public class TestFixture 
{
    protected static ChromeDriver driver;

    [AssemblyInitialize]
    public static void ClassInitialize(TestContext ctx)
    {
        driver = new ChromeDriver();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        string enderecoBanco = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";

        using (SqlConnection conexao = new SqlConnection(enderecoBanco))
        {
            conexao.Open();
            SqlCommand comandoExcluir = conexao.CreateCommand();
            comandoExcluir.CommandText = "DELETE FROM [TBGRUPOVEICULOS]; DELETE FROM [ASPNETUSERS]";
            comandoExcluir.ExecuteNonQuery();
        }
    }

    [AssemblyCleanup]
    public static void TestCleanup()
    {
        driver.Quit();
    }
}