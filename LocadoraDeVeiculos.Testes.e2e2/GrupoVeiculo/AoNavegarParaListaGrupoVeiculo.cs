using LocadoraDeVeiculos.Testes.e2e.Compartilhado;

namespace LocadoraDeVeiculos.Testes.e2e.GrupoVeiculo;

[TestClass]
public class AoNavegarParaListaGrupoVeiculo : TestFixture
{

    [TestMethod]
    public void Deve_mostrar_GrupoDeVeiculos_no_titulo()
    {
        driver.Navigate().GoToUrl($"{EnderecoBase}/GrupoVeiculos/Listar");

        Assert.IsTrue(driver.Title.Contains("Listagem de Grupos de Veículos"));
    }



}