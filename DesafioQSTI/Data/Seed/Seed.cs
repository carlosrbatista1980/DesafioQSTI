using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioQSTI.Data.Repository;

namespace DesafioQSTI.Data.Seed
{
    public class Seed
    {
        public MySqlDbContext _context { get; set; }

        public Seed(MySqlDbContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Cliente.Any() && !_context.Servico.Any())
            {
                Console.WriteLine("O banco de dados está rodando o Seed");

                Cliente cli1 = new Cliente() { Id = 1, Nome = "Talita Tassila", Senha = "2121" };
                Cliente cli2 = new Cliente() { Id = 2, Nome = "Carlos Rodrigues", Senha = "1212" };
                Cliente cli3 = new Cliente() { Id = 3, Nome = "Rafael Rosa Batista", Senha = "2112" };
                Cliente cli4 = new Cliente() { Id = 4, Nome = "Natasha F. Batista", Senha = "1209" };
                Cliente cli5 = new Cliente() { Id = 5, Nome = "admin", Senha = "123" };

                Servico srv1 = new Servico() { Id = 1, Nome = "Pé e Mão", Descricao = "descrição1" };
                Servico srv2 = new Servico() { Id = 2, Nome = "Luzes", Descricao = "descrição2" };
                Servico srv3 = new Servico() { Id = 3, Nome = "MegaHair", Descricao = "descrição3" };
                Servico srv4 = new Servico() { Id = 4, Nome = "Aplique", Descricao = "descrição4" };
                Servico srv5 = new Servico() { Id = 5, Nome = "Massagem", Descricao = "descrição5" };

                ServicoCliente srvCliente1 = new ServicoCliente() { Id = 1, Cliente = cli1, Servico = srv2, Versao = "5.6.7" };
                ServicoCliente srvCliente2 = new ServicoCliente() { Id = 2, Cliente = cli3, Servico = srv1, Versao = "5.44.7" };
                ServicoCliente srvCliente3 = new ServicoCliente() { Id = 3, Cliente = cli5, Servico = srv5, Versao = "5.99.7" };
                
                ExecucaoServico execucaoSrv1 = new ExecucaoServico() { Id = 1, ServicoCliente = srvCliente3, DataHora = DateTime.Now};
                ExecucaoServico execucaoSrv2 = new ExecucaoServico() { Id = 2, ServicoCliente = srvCliente1, DataHora = DateTime.Now };

                try
                {
                    _context.Cliente.AddRange(cli1, cli2, cli3, cli4, cli5);
                    _context.Servico.AddRange(srv1, srv2, srv3, srv4, srv5);
                    _context.ServicoCliente.AddRange(srvCliente1, srvCliente2, srvCliente3);
                    _context.ExecucaoServico.AddRange(execucaoSrv1, execucaoSrv2);

                    _context.SaveChanges();

                    Console.WriteLine("### Database Seeded ###:   \n");

                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("### ERROR ###:   \n" + ex.Message);
                }
            }

            Console.WriteLine("### Database is not clean ###:   \n");
        }
    }
}
