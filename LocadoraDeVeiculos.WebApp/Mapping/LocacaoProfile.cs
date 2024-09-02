using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloCondutor;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.Dominio.ModuloTaxa;
using LocadoraDeVeiculos.WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LocadoraDeVeiculos.WebApp.Mapping;


public class LocacaoProfile : Profile
{
    public LocacaoProfile()
    {
        CreateMap<InserirLocacaoViewModel, Locacao>()
            .ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());

        CreateMap<RealizarDevolucaoViewModel, Locacao>()
            .ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());

        CreateMap<Locacao, ListarLocacaoViewModel>()
            .ForMember(l => l.Veiculo, opt => opt.MapFrom(src => src.Veiculo!.Modelo))
            .ForMember(l => l.Condutor, opt => opt.MapFrom(src => src.Condutor!.Nome))
            .ForMember(l => l.TipoPlano, opt => opt.MapFrom(src => src.TipoPlano.ToString()));

        CreateMap<Locacao, RealizarDevolucaoViewModel>()
            .ForMember(l => l.Condutores, opt => opt.MapFrom<CondutoresValueResolver>())
            .ForMember(l => l.Veiculos, opt => opt.MapFrom<VeiculosValueResolver>())
            .ForMember(l => l.Taxas, opt => opt.MapFrom<TaxasValueResolver>())
            .ForMember(l => l.TaxasSelecionadas,
                opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id))); ;
    }
}

public class CondutoresValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
    private readonly IRepositorioCondutor _repositorioCondutor;

    public CondutoresValueResolver(IRepositorioCondutor repositorioCondutor)
    {
        _repositorioCondutor = repositorioCondutor;
    }

    public IEnumerable<SelectListItem>? Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {
        return _repositorioCondutor
            .SelecionarTodos()
            .Select(c => new SelectListItem(c.Nome, c.Id.ToString()));
    }
}

public class VeiculosValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
    private readonly ServicoVeiculo _servicoVeiculo;

    public VeiculosValueResolver(ServicoVeiculo servicoVeiculo)
    {
        _servicoVeiculo = servicoVeiculo;
    }

    public IEnumerable<SelectListItem>? Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {
        if (destination is RealizarDevolucaoViewModel)
        {
            var veiculoSelecionado = source.Veiculo;

            return [new SelectListItem(veiculoSelecionado!.Modelo, veiculoSelecionado.Id.ToString())];
        }

        return _servicoVeiculo
            .SelecionarTodos()
            .Value
            .Select(v => new SelectListItem(v.Modelo, v.Id.ToString()));
    }
}

public class TaxasValueResolver : IValueResolver<Locacao, FormularioLocacaoViewModel, IEnumerable<SelectListItem>?>
{
    private readonly IRepositorioTaxa repositorioTaxa;

    public TaxasValueResolver(IRepositorioTaxa repositorioTaxa)
    {
        this.repositorioTaxa = repositorioTaxa;
    }

    public IEnumerable<SelectListItem>? Resolve(Locacao source, FormularioLocacaoViewModel destination, IEnumerable<SelectListItem>? destMember,
        ResolutionContext context)
    {

        return repositorioTaxa
            .SelecionarTodos()
            .Select(t => new SelectListItem(t.ToString(), t.Id.ToString()));
    }
}

public class TaxasSelecionadasValueResolver : IValueResolver<FormularioLocacaoViewModel, Locacao, List<Taxa>>
{
    private readonly IRepositorioTaxa repositorioTaxa;

    public TaxasSelecionadasValueResolver(IRepositorioTaxa repositorioTaxa)
    {
        this.repositorioTaxa = repositorioTaxa;
    }

    public List<Taxa> Resolve(
        FormularioLocacaoViewModel source,
        Locacao destination,
        List<Taxa> destMember,
        ResolutionContext context
    )
    {
        var idsTaxasSelecionadas = source.TaxasSelecionadas.ToList();

        return repositorioTaxa.SelecionarMuitos(idsTaxasSelecionadas);
    }
}
