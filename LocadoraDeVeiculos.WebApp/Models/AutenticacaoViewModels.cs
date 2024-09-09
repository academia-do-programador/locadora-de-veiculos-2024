using System.ComponentModel.DataAnnotations;

namespace LocadoraDeVeiculos.WebApp.Models;

public class RegistrarViewModel
{
    [Required(ErrorMessage = "O usuário é obrigatório")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }

    [Required(ErrorMessage = "A comparação da senha é obrigatória")]
    [DataType(DataType.Password)]
    [Compare("Senha", ErrorMessage = "As senhas não conferem")]
    public string ConfirmarSenha { get; set; }
}

public class LoginViewModel
{
    [Required(ErrorMessage = "O usuário é obrigatório")]
    public string Usuario { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
}