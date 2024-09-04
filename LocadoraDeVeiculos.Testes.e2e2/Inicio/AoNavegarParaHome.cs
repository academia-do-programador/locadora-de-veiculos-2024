using LocadoraDeVeiculos.Testes.e2e.Compartilhado;

namespace LocadoraDeVeiculos.Testes.e2e.Inicio;

[TestClass]
public class AoNavegarParaHome : TextFixture
{
    [TestMethod]
    public void Deve_mostrar_LocadoraDeVeiculos_no_titulo()
    {
        driver.Navigate().GoToUrl("https://localhost:9100");

        Assert.IsTrue(driver.Title.Contains("Locadora De Veículos"));
    }

    [TestMethod]
    public void Deve_mostrar_EmConstrucao_na_pagina()
    {
        driver.Navigate().GoToUrl("https://localhost:9100/home/index");

        Assert.IsTrue(driver.PageSource.Contains("Em construção"));
    }

}

//pacotes selenium webdriver
//abrir site academia do programador
//testar localhost
//nomenclatura
