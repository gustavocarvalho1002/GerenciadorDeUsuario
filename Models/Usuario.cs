using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreRegistro_CadastroUsuario.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório!")]
        public string nome { get; set; }

        [MaxLength(11, ErrorMessage = "CPF deve ter 11 digitos!")]
        [MinLength(11, ErrorMessage = "CPF deve ter 11 digitos!")]
        [Required(ErrorMessage = "CPF é obrigatório!")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Email é obrigatório!")]
        [EmailAddress(ErrorMessage = "Email informado não é válido!")]
        public string email { get; set; }

        public bool ativo { get; set; }


        public string GerarSenha()
        {
            int tamanho = 6;
            // Caracteres permitidos para o gerador de senha
            string caracteresValidos = "abcdefghjkmnpqrstuvwxyz123456789";
            Random random = new Random();

            char[] chars = new char[tamanho];

            // Selecione um caracteres a cada loop do for 
            for (int i = 0; i < tamanho; i++)
            {
                chars[i] = caracteresValidos[random.Next(0, caracteresValidos.Length)];
            }
            return new string(chars);
        }
    }
}