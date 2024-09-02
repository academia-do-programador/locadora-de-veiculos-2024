using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloCondutor;
using LocadoraDeVeiculos.Aplicacao.ModuloLocacao;
using LocadoraDeVeiculos.Aplicacao.ModuloTaxa;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Controllers;

public class LocacaoController : WebControllerBase
{
    private readonly ServicoLocacao servicoLocacao;
    private readonly ServicoVeiculo servicoVeiculo;
    private readonly ServicoCondutor servicoCondutor;
    private readonly ServicoTaxa servicoTaxa;
    private readonly IMapper mapeador;

    public LocacaoController(
        ServicoLocacao servicoLocacao,
        ServicoVeiculo servicoVeiculo,
        ServicoCondutor servicoCondutor,
        ServicoTaxa servicoTaxa,
        IMapper mapeador
    )
    {
        this.servicoLocacao = servicoLocacao;
        this.servicoVeiculo = servicoVeiculo;
        this.servicoCondutor = servicoCondutor;
        this.servicoTaxa = servicoTaxa;
        this.mapeador = mapeador;
    }

    public IActionResult Listar()
    {
        var resultado = servicoLocacao.SelecionarTodos();

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var locacoes = resultado.Value;

        var listarLocacoesVm = mapeador.Map<IEnumerable<ListarLocacaoViewModel>>(locacoes);

        return View(listarLocacoesVm);
    }

    public IActionResult Inserir()
    {
        return View(CarregarDadosFormulario());
    }

    [HttpPost]
    public IActionResult Inserir(InserirLocacaoViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(CarregarDadosFormulario(inserirVm));

        var locacao = mapeador.Map<Locacao>(inserirVm);

        var resultado = servicoLocacao.Inserir(locacao);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O registro ID [{locacao.Id}] foi inserido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    public IActionResult RealizarDevolucao(int id)
    {
        var resultado = servicoLocacao.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        var locacao = resultado.Value;

        var devolucaoVm = mapeador.Map<RealizarDevolucaoViewModel>(locacao);

        return View(devolucaoVm);
    }

    [HttpPost]
    public IActionResult RealizarDevolucao(RealizarDevolucaoViewModel devolucaoVm)
    {
        var locacaoOriginal = servicoLocacao.SelecionarPorId(devolucaoVm.Id).Value;

        var locacaoAtualizada = mapeador.Map<RealizarDevolucaoViewModel, Locacao>(devolucaoVm, locacaoOriginal);

        var resultado = servicoLocacao.RealizarDevolucao(locacaoAtualizada);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"A locação ID [{locacaoAtualizada.Id}] foi concluída com sucesso!");

        return RedirectToAction(nameof(Listar));
    }

    private InserirLocacaoViewModel CarregarDadosFormulario(InserirLocacaoViewModel? formularioVm = null)
    {
        var condutores = servicoCondutor.SelecionarTodos().Value;
        var veiculos = servicoVeiculo.SelecionarTodos().Value;
        var taxas = servicoTaxa.SelecionarTodos().Value;

        if (formularioVm is null)
            formularioVm = new InserirLocacaoViewModel();

        formularioVm.Condutores =
            condutores.Select(c => new SelectListItem(c.Nome, c.Id.ToString()));

        formularioVm.Veiculos =
            veiculos.Select(c => new SelectListItem(c.Modelo, c.Id.ToString()));

        formularioVm.Taxas =
            taxas.Select(c => new SelectListItem(c.ToString(), c.Id.ToString()));

        return formularioVm;
    }
}
