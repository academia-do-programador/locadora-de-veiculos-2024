using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Compartilhado;

[TestClass]
public class TextFixture
{
    protected static IWebDriver driver;
    protected static TestContext context;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext ctx)
    {
        context = ctx;
        var opt = new ChromeOptions();
        opt.AddArguments("--headless=new");

        driver = new ChromeDriver(opt);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        driver.Quit();
    }
}
