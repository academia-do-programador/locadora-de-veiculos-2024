using LocadoraDeVeiculos.Testes.e2e.Compartilhado;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.GrupoVeiculo;
using LocadoraDeVeiculos.Testes.e2e.Compartilhado.PageObjects.Veiculo;

namespace LocadoraDeVeiculos.Testes.e2e.Veiculo;

[TestClass]
public class AoEditarVeiculo : TestFixture
{
    FormularioGrupoVeiculoPageObject grupoVeiculoPage;
    FormularioVeiculoPageObject veiculoPageObject;

    public AoEditarVeiculo()
    {
        grupoVeiculoPage = new FormularioGrupoVeiculoPageObject(driver);
        veiculoPageObject = new FormularioVeiculoPageObject(driver);
    }

    [TestMethod]
    public void Dado_info_validas_deve_mostrar_Veiculo_atualizado_na_listagem()
    {
        //arrange
        grupoVeiculoPage.Visitar();
        string nomeGrupo = "SUV";
        grupoVeiculoPage.PreencherFormulario(nomeGrupo);
        grupoVeiculoPage.SubmeterFormulario();

        veiculoPageObject.Visitar();
        string foto = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"Veiculo\ap.jpg"));
        veiculoPageObject.PreencherFormulario(foto, "Kicks", "Nissan", "Gasolina", "48", nomeGrupo);
        veiculoPageObject.SubmeterFormulario();

        int id = veiculoPageObject.GetId("Kicks");

        veiculoPageObject.Visitar(id);
        veiculoPageObject.PreencherFormulario(foto, "Sentra", "Nissan", "Gasolina", "48", nomeGrupo);

        //action
        veiculoPageObject.SubmeterFormulario();

        Assert.IsTrue(
            driver.PageSource.Contains("Listagem de Veículos") &&
            driver.PageSource.Contains("Sentra")
            );
    }

}