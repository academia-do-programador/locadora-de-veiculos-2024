using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloPlanoCobranca;
using LocadoraDeVeiculos.Aplicacao.ModuloVeiculo;
using LocadoraDeVeiculos.Dominio.ModuloLocacao;
using LocadoraDeVeiculos.WebApp.Mapping.Resolvers;
using LocadoraDeVeiculos.WebApp.Models;

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
                opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id)));

        // Check-in - Check-out
        CreateMap<Locacao, ConfirmarAberturaLocacaoViewModel>()
            .ForMember(l => l.ValorParcial, opt => opt.MapFrom<ValorParcialValueResolver>())
            .ForMember(l => l.Condutores, opt => opt.MapFrom<CondutoresValueResolver>())
            .ForMember(l => l.Veiculos, opt => opt.MapFrom<VeiculosValueResolver>())
            .ForMember(l => l.Taxas, opt => opt.MapFrom<TaxasValueResolver>())
            .ForMember(l => l.TaxasSelecionadas,
                opt => opt.MapFrom(src => src.TaxasSelecionadas.Select(tx => tx.Id)));

        CreateMap<ConfirmarAberturaLocacaoViewModel, Locacao>()
            .ForMember(l => l.TaxasSelecionadas, opt => opt.MapFrom<TaxasSelecionadasValueResolver>());
    }
}

public class ValorParcialValueResolver : IValueResolver<Locacao, ConfirmarAberturaLocacaoViewModel, decimal>
{
    private readonly ServicoVeiculo servicoVeiculo;
    private readonly ServicoPlanoCobranca servicoPlano;

    public ValorParcialValueResolver(ServicoVeiculo servicoVeiculo, ServicoPlanoCobranca servicoPlano)
    {
        this.servicoVeiculo = servicoVeiculo;
        this.servicoPlano = servicoPlano;
    }

    public decimal Resolve(
        Locacao source,
        ConfirmarAberturaLocacaoViewModel destination,
        decimal destMember,
        ResolutionContext context
    )
    {
        var veiculo = servicoVeiculo.SelecionarPorId(source.VeiculoId).Value;

        var planoSelecionado = servicoPlano.SelecionarPorIdGrupoVeiculos(veiculo.GrupoVeiculosId).Value;

        return source.CalcularValorParcial(planoSelecionado);
    }
}