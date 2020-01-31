using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioQSTI.Data.Repository
{
    public class ExecucaoServico
    {
        public int Id { get; set; }
        public DateTime? DataHora { get; set; }
        public ServicoCliente ServicoCliente { get; set; }
    }
}
