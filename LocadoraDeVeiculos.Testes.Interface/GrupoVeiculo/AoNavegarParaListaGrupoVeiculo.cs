using LocadoraDeVeiculos.Testes.Interface.Compartilhado;
using LocadoraDeVeiculos.Testes.Interface.Compartilhado.PageObjects;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LocadoraDeVeiculos.Testes.Interface.GrupoVeiculo;

[TestClass]
public class AoNavegarParaListaGrupoVeiculo : TestFixture
{
    [TestMethod]
    public void Deve_apresentar_Listagem_Grupos_Veiculos()
    {
        //action
        driver.Navigate().GoToUrl("http://localhost:5125/GrupoVeiculos/Listar");

        //assert
        Assert.IsTrue( driver.PageSource.Contains("Listagem de Grupos de Veículos") );

        driver.Quit();
    }
}