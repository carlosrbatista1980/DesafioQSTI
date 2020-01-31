using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DesafioQSTI.Data.Repository;

namespace DesafioQSTI.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        
        public string Nome { get; set; }
        
        public string Senha { get; set; }

        //execucao servico
        public ExecucaoServico ExecucaoServicos { get; set; }

        public ServicoCliente ServicoClientes { get; set; }

        //servico
        public Servico Servicos { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Versao { get; set; }

        public DateTime? DataHora { get; set; }
    }
}
