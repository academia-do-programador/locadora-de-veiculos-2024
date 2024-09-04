using AutoMapper;
using LocadoraDeVeiculos.Aplicacao.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.WebApp.Models;

namespace LocadoraDeVeiculos.WebApp.Mapping;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<InserirFuncionarioViewModel, Funcionario>()
            .ForMember(dest => dest.EmpresaId, opt => opt.MapFrom<EmpresaIdValueResolver>());

        CreateMap<Funcionario, ListarFuncionarioViewModel>();
    }
}

public class EmpresaIdValueResolver : IValueResolver<object, object, int>
{
    private readonly ServicoAutenticacao servicoAutenticacao;
    private readonly IHttpContextAccessor httpContextAccessor;

    public EmpresaIdValueResolver(ServicoAutenticacao servicoAutenticacao, IHttpContextAccessor httpContextAccessor)
    {
        this.servicoAutenticacao = servicoAutenticacao;
        this.httpContextAccessor = httpContextAccessor;
    }

    public int Resolve(object source, object destination, int destMember, ResolutionContext context)
    {
        var usuarioClaim = httpContextAccessor.HttpContext?.User;

        var empresa = servicoAutenticacao.ObterUsuarioAsync(usuarioClaim!).Result;

        return empresa?.Id ?? 0;
    }
}
