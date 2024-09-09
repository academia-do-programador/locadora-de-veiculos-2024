using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;

namespace LocadoraDeVeiculos.Testes.e2e.GrupoVeiculo;

[TestClass]
public class AoInserirGrupoVeiculo : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;

    public AoInserirGrupoVeiculo()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
    }

    [TestMethod]
    [DataRow("Carros de Luxo")]
    public void Dado_info_validas_deve_mostrar_grupo_na_listagem(string nomeGrupo)
    {
        //arrange
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);

        //action
        grupoVeiculoPage.SubmeterFormulario();

        //assert
        Assert.IsTrue(driver.PageSource.Contains(nomeGrupo));
    }

    [TestMethod]
    [DataRow("", "O nome é obrigatório")]
    [DataRow("a", "O nome deve conter ao menos 3 caracteres")]
    public void Dado_info_invalidas_deve_permanecer_no_formulario_de_GrupoVeiculos(string nomeGrupo, string mensagemErro)
    {
        grupoVeiculoPage.Visitar();
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);

        grupoVeiculoPage.SubmeterFormulario();

        var elemento = grupoVeiculoPage.Erros["Nome"];

        Assert.AreEqual(mensagemErro, elemento.Text);
        Assert.IsTrue(elemento.Displayed);
    }
}