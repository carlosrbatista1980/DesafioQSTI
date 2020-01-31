using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioQSTI.Data;
using DesafioQSTI.Data.Repository;
using DesafioQSTI.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace DesafioQSTI.Controllers
{
    public class ClientesController : Controller
    {
        private readonly MySqlDbContext _context;

        public ClientesController(MySqlDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public IActionResult Index()
        {
            //< !--Serviço | Nome do Cliente | DataHora da Execução | Versão-- >

            var serviceList = _context.ExecucaoServico
                    .Include(sc => sc.ServicoCliente)
                        .ThenInclude(c => c.Cliente)
                    .Include(scc => scc.ServicoCliente)
                        .ThenInclude(s => s.Servico);

            return View(serviceList);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Senha")] Cliente cliente)
        {
            if (cliente.Id == 0)
            {
                cliente.Id = _context.Cliente.Max(x => x.Id) + 1;
            }

            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var cliente = await _context.ExecucaoServico
                    .Include(sc => sc.ServicoCliente)
                        .ThenInclude(c => c.Cliente)
                    .Include(scc => scc.ServicoCliente)
                        .ThenInclude(s => s.Servico).FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Senha")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
