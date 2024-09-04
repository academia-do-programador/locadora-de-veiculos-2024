using Dapper;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;

namespace LocadoraDeVeiculos.Testes.e2e.Exemplo;

[TestClass]
public class AoInserirGrupoVeiculoExemplo : TextFixture, IDisposable
{

    private string connectionString = @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=LocadoraDeVeiculosOrm;Integrated Security=True;Pooling=True";


    [TestMethod]
    [DataRow("Carros de Luxo")]
    public void Dado_info_validas_deve_mostrar_grupo_na_listagem(string nomeGrupo)
    {
        driver.Navigate().GoToUrl("https://localhost:9100/GrupoVeiculos/Inserir");

        var inputNome = driver.FindElement(By.Id("Nome"));
        inputNome.SendKeys(nomeGrupo);

        var botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));

        botaoGravar.Click();

        Assert.IsTrue(driver.PageSource.Contains(nomeGrupo));
    }

    [TestMethod]
    [DataRow("", "O nome é obrigatório")]
    [DataRow("a", "O nome deve conter ao menos 3 caracteres")]
    public void Dado_info_invalidas_deve_permanecer_na_pagina(string nomeGrupo, string mensagemErro)
    {
        driver.Navigate().GoToUrl("https://localhost:9100/GrupoVeiculos/Inserir");

        var inputNome = driver.FindElement(By.Id("Nome"));
        inputNome.SendKeys(nomeGrupo);

        var botaoGravar = driver.FindElement(By.CssSelector(".btn-primary"));

        botaoGravar.Click();

        IWebElement elemento = driver.FindElement(By.CssSelector("span.field-validation-error[data-valmsg-for='Nome']"));

        Assert.AreEqual(mensagemErro, elemento.Text);

        Assert.IsTrue(elemento.Displayed);
    }


    public void Dispose()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            connection.Execute("DELETE FROM TBGRUPOVEICULOS WHERE NOME LIKE @NOME", new { Nome = "Carros de Luxo" });
        }
    }
}