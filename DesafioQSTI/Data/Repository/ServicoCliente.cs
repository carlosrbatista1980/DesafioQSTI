using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioQSTI.Data.Repository
{
    public class ServicoCliente
    {
        public int Id { get; set; }
        public string Versao { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public Servico Servico { get; set; }
        public int ServicoId { get; set; }
    }
}
