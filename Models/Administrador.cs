using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PreRegistro_CadastroUsuario.Models
{
    public class Administrador
    {
        public int id { get; set; }
        [Required(ErrorMessage = "Usuario é obrigatório!")]
        public string usuario { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório!")]
        public string senha { get; set; }

        public bool ativo { get; set; }

    }
}