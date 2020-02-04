using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioQSTI.Data;
using DesafioQSTI.Data.Repository;

namespace DesafioQSTI.Services
{
    public class ServicoCliente
    {
        public MySqlDbContext _context;

        public ServicoCliente(MySqlDbContext context)
        {
            _context = context;
        }

        public List<Servico> GetAllServicos()
        {
            return _context.Servico.ToList();
        }

        public List<Cliente> GetAllClientes()
        {
            return _context.Cliente.ToList();
        }
    }
}
