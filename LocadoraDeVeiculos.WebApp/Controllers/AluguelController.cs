﻿using AutoMapper;
using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Servicos;
using LocadoraDeVeiculos.Dominio.ModuloAluguel;
using LocadoraDeVeiculos.Dominio.ModuloPlanoDeCobranca;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Extensions;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LocadoraDeAluguel.WebApp.Controllers;
public class AluguelController(
        AluguelService servicoAluguel,
        CondutorService servicoCondutor,
        ClienteService servicoCliente,
        PlanoDeCobrancaService servicoPlano,
        GrupoDeAutomoveisService servicoGrupo,
        VeiculoService servicoVeiculo,
        TaxaService servicoTaxa,
        ConfiguracaoService servicoConfiguracao,
        IMapper mapeador) : WebControllerBase
{
    public IActionResult Listar()
    {
        var resultado =
            servicoAluguel.SelecionarTodos(UsuarioId.GetValueOrDefault());

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());
            return RedirectToAction("Index", "Inicio");
        }

        var registros = resultado.Value;

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        if (registros.Count == 0 && ViewBag.Mensagem is null)
            ApresentarMensagemSemRegistros();

        var listarAluguelVm = mapeador.Map<IEnumerable<ListarAluguelViewModel>>(registros);

        ViewBag.Mensagem = TempData.DesserializarMensagemViewModel();

        return View(listarAluguelVm);
    }


    public IActionResult Inserir()
    {
        if (ValidacaoSemDependencias())
            return RedirectToAction(nameof(Listar));

        return View(CarregarInformacoes(new InserirAluguelViewModel()));
    }
    [HttpPost]
    public IActionResult Inserir(InserirAluguelViewModel inserirRegistroVm)
    {
        if (!ModelState.IsValid)
        {
            if (inserirRegistroVm.CategoriaPlano != CategoriaDePlanoEnum.Diário)
            return View(CarregarInformacoes(inserirRegistroVm));
        }

        inserirRegistroVm.TaxasSelecionadasId ??= "";

        var novoRegistro = mapeador.Map<Aluguel>(inserirRegistroVm);

        //novoRegistro.UsuarioId = UsuarioId.GetValueOrDefault();

        var resultado = servicoAluguel.Inserir(novoRegistro, inserirRegistroVm.CondutorId, inserirRegistroVm.ClienteId, inserirRegistroVm.GrupoId, inserirRegistroVm.VeiculoId);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        servicoVeiculo.AlugarVeiculo(inserirRegistroVm.VeiculoId);

        ApresentarMensagemSucesso($"O registro \"{novoRegistro}\" foi inserido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }


    public IActionResult Editar(int id)
    {
        var resultado = servicoAluguel.SelecionarPorId(id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        var registro = resultado.Value;

        if (!registro.Ativo)
        {
            ApresentarMensagemImpossivelEditar();
            return RedirectToAction(nameof(Listar));
        }

        var editarPlanoVm = mapeador.Map<EditarAluguelViewModel>(registro);

        editarPlanoVm.GrupoId = registro.GrupoDeAutomoveis.Id;

        return View(CarregarInformacoes(editarPlanoVm));
    }
    [HttpPost]
    public IActionResult Editar(EditarAluguelViewModel editarRegistroVm)
    {
        if (!ModelState.IsValid)
        {
            if (editarRegistroVm.CategoriaPlano != CategoriaDePlanoEnum.Diário)
            return View(CarregarInformacoes(editarRegistroVm));
        }

        servicoVeiculo.LiberarVeiculo(servicoAluguel.SelecionarPorId(editarRegistroVm.Id).Value.Veiculo.Id);

        var registro = mapeador.Map<Aluguel>(editarRegistroVm);

        var resultado = servicoAluguel.Editar(registro, editarRegistroVm.CondutorId, editarRegistroVm.ClienteId, editarRegistroVm.GrupoId, editarRegistroVm.VeiculoId);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        servicoVeiculo.AlugarVeiculo(editarRegistroVm.VeiculoId);

        var nome = servicoAluguel.SelecionarPorId(editarRegistroVm.Id).Value;

        ApresentarMensagemSucesso($"O registro \"{nome}\" foi editado com sucesso!");

        return RedirectToAction(nameof(Listar));
    }


    public IActionResult Excluir(int id)
    {
        var resultado = servicoAluguel.SelecionarPorId(id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        var registro = resultado.Value;

        if (registro.Ativo)
        {
            ApresentarMensagemImpossivelExcluir();
            return RedirectToAction(nameof(Listar));
        }

        var detalhesRegistroVm = mapeador.Map<DetalhesAluguelViewModel>(registro);

        return View(detalhesRegistroVm);
    }
    [HttpPost]
    public IActionResult Excluir(DetalhesAluguelViewModel detalhesRegistroVm)
    {
        var nome = servicoAluguel.SelecionarPorId(detalhesRegistroVm.Id).Value;

        servicoVeiculo.LiberarVeiculo(detalhesRegistroVm.Veiculo!.Id);

        var resultado = servicoAluguel.Excluir(detalhesRegistroVm.Id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        ApresentarMensagemSucesso($"O registro \"{nome}\" foi excluído com sucesso!");

        return RedirectToAction(nameof(Listar));
    }


    public IActionResult Devolver(int id)
    {
        var resultado = servicoAluguel.SelecionarPorId(id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        var registro = resultado.Value;

        var devolverVm = mapeador.Map<DevolverAluguelViewModel>(registro);

        return View(CarregarInformacoes(devolverVm));
    }
    [HttpPost]
    public IActionResult Devolver(DevolverAluguelViewModel devolverRegistroVm)
    {
        if (!ModelState.IsValid)
            return View(CarregarInformacoes(devolverRegistroVm));

        var resultado = servicoAluguel.Devolver(devolverRegistroVm.Id, devolverRegistroVm.ValorTotal, devolverRegistroVm.DataRetornoReal);

        servicoVeiculo.LiberarVeiculo(servicoAluguel.SelecionarPorId(devolverRegistroVm.Id).Value.Veiculo.Id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        var nome = servicoAluguel.SelecionarPorId(devolverRegistroVm.Id).Value;

        ApresentarMensagemSucesso($"O registro \"{nome}\" foi devolvido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }


    public IActionResult Detalhes(int id)
    {
        var resultado = servicoAluguel.SelecionarPorId(id);

        if (ValidacaoDeFalha(resultado))
            return RedirectToAction(nameof(Listar));

        var registro = resultado.Value;

        var detalhesRegistroVm = mapeador.Map<DetalhesAluguelViewModel>(registro);

        return View(detalhesRegistroVm);
    }

    #region Auxiliares
    private InserirAluguelViewModel? CarregarInformacoes(InserirAluguelViewModel inserirRegistroVm)
    {
        var resultadoCondutores = servicoCondutor.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoClientes = servicoCliente.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoGrupos = servicoGrupo.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoVeiculos = servicoVeiculo.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoTaxas = servicoTaxa.SelecionarTodos(UsuarioId.GetValueOrDefault());

        if (resultadoCondutores.IsFailed || resultadoClientes.IsFailed || resultadoGrupos.IsFailed || resultadoVeiculos.IsFailed || resultadoTaxas.IsFailed)
        {
            ApresentarMensagemFalha(Result.Fail("Falha ao encontrar dados necessários!"));
            return null;
        }

        var condutores = resultadoCondutores.Value;
        var clientes = resultadoClientes.Value;
        var grupos = resultadoGrupos.Value;
        var veiculos = resultadoVeiculos.Value;
        var taxas = resultadoTaxas.Value;
        var seguros = resultadoTaxas.Value.FindAll(t => t.Seguro);

        inserirRegistroVm.Condutores = condutores;
        inserirRegistroVm.Clientes = clientes.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
        inserirRegistroVm.Grupos = grupos;
        inserirRegistroVm.Veiculos = veiculos;
        inserirRegistroVm.Categorias = Enum.GetNames(typeof(CategoriaDePlanoEnum)).Select(c => new SelectListItem(c, c));
        inserirRegistroVm.Taxas = taxas;
        inserirRegistroVm.Seguros = seguros;

        return inserirRegistroVm;
    }
    private EditarAluguelViewModel? CarregarInformacoes(EditarAluguelViewModel editarRegistroVm)
    {
        var resultadoCondutores = servicoCondutor.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoClientes = servicoCliente.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoGrupos = servicoGrupo.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoVeiculos = servicoVeiculo.SelecionarTodos(UsuarioId.GetValueOrDefault());
        var resultadoTaxas = servicoTaxa.SelecionarTodos(UsuarioId.GetValueOrDefault());

        if (resultadoCondutores.IsFailed || resultadoClientes.IsFailed || resultadoGrupos.IsFailed || resultadoVeiculos.IsFailed || resultadoTaxas.IsFailed)
        {
            ApresentarMensagemFalha(Result.Fail("Falha ao encontrar dados necessários!"));
            return null;
        }

        var condutores = resultadoCondutores.Value;
        var clientes = resultadoClientes.Value;
        var grupos = resultadoGrupos.Value;
        var veiculos = resultadoVeiculos.Value;
        var taxas = resultadoTaxas.Value;
        var seguros = resultadoTaxas.Value.FindAll(t => t.Seguro);

        editarRegistroVm.Condutores = condutores;
        editarRegistroVm.Clientes = clientes.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
        editarRegistroVm.Grupos = grupos;
        editarRegistroVm.Veiculos = veiculos;
        editarRegistroVm.Categorias = Enum.GetNames(typeof(CategoriaDePlanoEnum)).Select(c => new SelectListItem(c, c));
        editarRegistroVm.Taxas = taxas;
        editarRegistroVm.Seguros = seguros;
        return editarRegistroVm;
    }
    private DevolverAluguelViewModel? CarregarInformacoes(DevolverAluguelViewModel encerrarRegistroVm)
    {
        List<Taxa> taxas = [];

        var registro = servicoAluguel.SelecionarPorId(encerrarRegistroVm.Id).Value;
        var config = servicoConfiguracao.Selecionar();

        encerrarRegistroVm.Cliente = registro.Cliente;
        encerrarRegistroVm.Condutor = registro.Condutor;
        encerrarRegistroVm.GrupoNome = registro.GrupoNome;
        encerrarRegistroVm.Veiculo = registro.Veiculo;
        encerrarRegistroVm.DataSaida = registro.DataSaida;
        encerrarRegistroVm.DataRetornoPrevista = registro.DataRetornoPrevista;
        encerrarRegistroVm.PlanoDeCobranca = registro.PlanoDeCobranca;
        encerrarRegistroVm.TaxasSelecionadasId = registro.TaxasSelecionadasId;
        encerrarRegistroVm.Configuracao = config;

        if (encerrarRegistroVm.TaxasSelecionadasId != "")
            foreach(var taxaId in encerrarRegistroVm.TaxasSelecionadasId!.Split(','))
                taxas.Add(servicoTaxa.SelecionarPorId(Convert.ToInt32(taxaId)).Value);

        var resultadoTaxas = servicoTaxa.SelecionarTodos(UsuarioId.GetValueOrDefault());

        if (resultadoTaxas.IsFailed)
        {
            ApresentarMensagemFalha(Result.Fail("Falha ao encontrar dados necessários!"));
            return null;
        }

        encerrarRegistroVm.Taxas = taxas;
        return encerrarRegistroVm;
    }
    protected bool ValidacaoDeFalha(Result<Aluguel> resultado)
    {
        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());
            return true;
        }
        return false;
    }
    private bool ValidacaoSemDependencias()
    {
        if (servicoCliente.SelecionarTodos(UsuarioId.GetValueOrDefault()).Value.Count == 0)
        {
            ApresentarMensagemSemDependencias("Clientes");
            return true;
        }

        if (servicoCondutor.SelecionarTodos(UsuarioId.GetValueOrDefault()).Value.Count == 0)
        {
            ApresentarMensagemSemDependencias("Condutores");
            return true;
        }

        if (servicoGrupo.SelecionarTodos(UsuarioId.GetValueOrDefault()).Value.Count == 0)
        {
            ApresentarMensagemSemDependencias("Grupos de Automóveis");
            return true;
        }

        if (servicoVeiculo.SelecionarTodos(UsuarioId.GetValueOrDefault()).Value.Count == 0)
        {
            ApresentarMensagemSemDependencias("Veículos");
            return true;
        }

        if (servicoPlano.SelecionarTodos(UsuarioId.GetValueOrDefault()).Value.Count == 0)
        {
            ApresentarMensagemSemDependencias("Planos de Aluguel");
            return true;
        }

        return false;
    }
    #endregion
}