using LocadoraDeVeiculos.Testes.e2e.Compartilhado;

namespace LocadoraDeVeiculos.Testes.e2e.PlanoCobranca;

[TestClass]
public class AoNavegarParaListaPlanoCobranca : TestFixture
{

    [TestMethod]
    public void Deve_mostrar_PlanoCobranca_no_titulo()
    {
        driver.Navigate().GoToUrl($"{EnderecoBase}/GrupoVeiculos/Listar");

        Assert.IsTrue(driver.Title.Contains("Listagem de Grupos de Veículos"));
    }



}