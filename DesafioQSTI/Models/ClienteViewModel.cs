using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioQSTI.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatorio")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A senha é obrigatoria")]
        public string Senha { get; set; }
    }
}
