using System.ComponentModel.DataAnnotations;

namespace VendasLanches.ViewModels; 

public class LoginViewModel {

    [Required(ErrorMessage ="Digite o nome")]
    [Display(Name = "Usuário")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Digite a senha")]
    [Display(Name = "Senha")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string ReturnUrl { get; set; } = string.Empty;
}
