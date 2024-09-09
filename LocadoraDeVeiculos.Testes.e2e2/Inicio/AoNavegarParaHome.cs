using LocadoraDeVeiculos.Testes.e2e.Compartilhado;

namespace LocadoraDeVeiculos.Testes.e2e.Inicio;

[TestClass]
public class AoNavegarParaHome : TestFixture
{
    [TestMethod]
    public void Deve_mostrar_LocadoraDeVeiculos_no_titulo()
    {
        driver.Navigate().GoToUrl($"{EnderecoBase}");

        Assert.IsTrue(driver.Title.Contains("Locadora De Veículos"));
    }

    [TestMethod]
    public void Deve_mostrar_EmConstrucao_na_pagina()
    {
        driver.Navigate().GoToUrl($"{EnderecoBase}");

        Assert.IsTrue(driver.PageSource.Contains("Seja bem-vindo(a)!"));
    }
}