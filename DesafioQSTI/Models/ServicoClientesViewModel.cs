using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioQSTI.Data.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesafioQSTI.Models
{
    public class ServicoClientesViewModel
    {
        public int Id { get; set; }
        public string Versao { get; set; }

        public Servico Servico { get; set; }
        public Cliente Cliente { get; set; }

        public int ServicoId { get; set; }
        public int ClienteId { get; set; }

        public ICollection<Cliente> Clientes  { get; set; } = new List<Cliente>();
        public ICollection<Servico> Servicos { get; set; } = new List<Servico>();

        public ServicoClientesViewModel()
        {
            
        }
    }
}
