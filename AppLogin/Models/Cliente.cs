using System.ComponentModel.DataAnnotations;

namespace AppLogin.Models
{
    public class Cliente
    {
        [Display(Name = "Codigo", Description = "Codigo.")]
        public int Id { get; set; }

        [Display(Name = "Nome Completo", Description = "Nome e Sobrenome")]
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Nascimento")]
        public DateTime Nascimento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "O Sexo brigatório.")]
        [StringLength(1, ErrorMessage = "Deve Conter 1 caracter")]
        public string Sexo { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string CPF { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O Celular é obrigatório")]
        public string Telefone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O Email não é Valido")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string Email { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 10 caracteres")]
        public string Senha { get; set; }


        [Display(Name = "Situação")]
        [Required(ErrorMessage = "A situação é obrigatória")]
        public string Situacao { get; set; }
    }
}
