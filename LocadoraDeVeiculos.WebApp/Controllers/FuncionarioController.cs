using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.WebApp.Controllers.Compartilhado;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa")]
public class FuncionarioController : WebControllerBase
{
    private readonly ServicoFuncionario servicoFuncionario;
    private readonly ServicoAutenticacao servicoAutenticacao;
    private readonly IMapper mapeador;

    public FuncionarioController(
        ServicoFuncionario servicoFuncionario,
        ServicoAutenticacao servicoAutenticacao,
        IMapper mapeador
    )
    {
        this.servicoFuncionario = servicoFuncionario;
        this.servicoAutenticacao = servicoAutenticacao;
        this.mapeador = mapeador;
    }

    public async Task<IActionResult> Listar()
    {
        var empresa = await servicoAutenticacao.ObterUsuarioAsync(User);

        var resultado = servicoFuncionario.SelecionarFuncionariosDaEmpresa(empresa!.Id);

        if (resultado.IsFailed)
        {
            ApresentarMensagemFalha(resultado.ToResult());

            return RedirectToAction("Index", "Home");
        }

        var funcionarios = resultado.Value;

        var listarFuncionariosVm = mapeador.Map<IEnumerable<ListarFuncionarioViewModel>>(funcionarios);

        return View(listarFuncionariosVm);
    }

    public IActionResult Inserir()
    {
        return View(new InserirFuncionarioViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Inserir(InserirFuncionarioViewModel inserirVm)
    {
        if (!ModelState.IsValid)
            return View(inserirVm);

        var funcionario = mapeador.Map<Funcionario>(inserirVm);

        var resultadoFuncionario = await servicoFuncionario.Inserir(
            funcionario,
            inserirVm.NomeUsuario,
            inserirVm.Senha
        );

        if (resultadoFuncionario.IsFailed)
        {
            ApresentarMensagemFalha(resultadoFuncionario.ToResult());

            return RedirectToAction(nameof(Listar));
        }

        ApresentarMensagemSucesso($"O funcionário ID [{funcionario.Id}] foi inserido com sucesso!");

        return RedirectToAction(nameof(Listar));
    }
}
