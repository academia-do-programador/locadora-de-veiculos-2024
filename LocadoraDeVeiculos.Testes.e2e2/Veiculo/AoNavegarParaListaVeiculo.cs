using LocadoraDeVeiculos.Testes.e2e.Compartilhado;

namespace LocadoraDeVeiculos.Testes.e2e.Veiculo;

[TestClass]
public class AoNavegarParaListaVeiculo : TestFixture
{
    [TestMethod]
    public void Deve_mostrar_Veiculos_no_titulo()
    {
        driver.Navigate().GoToUrl($"{EnderecoBase}/Veiculo/Listar");

        Assert.IsTrue(driver.Title.Contains("Listagem de Veículos"));
    }
}