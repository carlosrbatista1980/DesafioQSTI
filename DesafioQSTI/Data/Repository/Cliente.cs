using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace DesafioQSTI.Data.Repository
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}
