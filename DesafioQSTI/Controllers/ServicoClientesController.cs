using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioQSTI.Data;
using DesafioQSTI.Data.Repository;
using DesafioQSTI.Helpers;
using DesafioQSTI.Models;

namespace DesafioQSTI.Controllers
{
    public class ServicoClientesController : Controller
    {
        private readonly MySqlDbContext _context;
        private readonly DesafioQSTI.Services.ServicoCliente _servicoCliente;

        public ServicoClientesController(MySqlDbContext context, DesafioQSTI.Services.ServicoCliente servicoCliente)
        {
            _context = context;
            _servicoCliente = servicoCliente;
        }

        // GET: ServicoClientes
        public async Task<IActionResult> Index()
        {
            var list = await _context.ServicoCliente.Include(s => s.Servico).Include(c => c.Cliente).OrderBy(x => x.Id).ToListAsync();


            return View(list);
        }

        // GET: ServicoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicoCliente = await _context.ServicoCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicoCliente == null)
            {
                return NotFound();
            }

            return View(servicoCliente);
        }

        // GET: ServicoClientes/Create
        public IActionResult Create()
        {
            var servicos = _servicoCliente.GetAllServicos();
            var clientes = _servicoCliente.GetAllClientes();
            var servicosclientesViewModel = new ServicoClientesViewModel {Servicos = servicos, Clientes = clientes};

            return View(servicosclientesViewModel);
        }

        // POST: ServicoClientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Versao")] ServicoClientesViewModel servicoClientesViewModel)
        public async Task<IActionResult> Create(ServicoClientesViewModel servicoClientesViewModel)
        {
            ServicoCliente servicoCliente = new ServicoCliente();

            if (servicoClientesViewModel.Id == 0)
            {
                servicoCliente.Id = _context.ServicoCliente.Max(x => x.Id) + 1;
            }

            servicoCliente.ClienteId = servicoClientesViewModel.ClienteId;
            servicoCliente.ServicoId = servicoClientesViewModel.ServicoId;
            servicoCliente.Versao = servicoClientesViewModel.Versao;

            if (ModelState.IsValid)
            {
                _context.Add(servicoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        // GET: ServicoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var vm = new ServicoClientesViewModel();

            if (id == null)
            {
                return NotFound();
            }

            var servicoCliente = await _context.ServicoCliente.Include(x => x.Servico).Include(c => c.Cliente).FirstOrDefaultAsync(f => f.Id == id);

            MapHelper.MapFrom(servicoCliente, vm);

            var clientes = _context.Cliente.ToList();
            var servicos = _context.Servico.ToList();

            vm.Servicos = servicos;
            vm.Clientes = clientes;

            if (servicoCliente == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: ServicoClientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Versao")] ServicoCliente servicoCliente)
        public async Task<IActionResult> Edit(int id, ServicoClientesViewModel servicoCliente)
        {
            var sc = _context.ServicoCliente.Include(c => c.Cliente).Include(s => s.Servico).FirstOrDefault(x => x.Id == servicoCliente.Id);

            if (sc == null)
            {
                return NotFound();
            }

            sc.Versao = servicoCliente.Versao;
            sc.ClienteId = servicoCliente.ClienteId;
            sc.ServicoId = servicoCliente.ServicoId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Attach(sc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicoClienteExists(servicoCliente.Id))
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
            return View();
        }

        // GET: ServicoClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicoCliente = await _context.ServicoCliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicoCliente == null)
            {
                return NotFound();
            }

            return View(servicoCliente);
        }

        // POST: ServicoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicoCliente = await _context.ServicoCliente.FindAsync(id);
            _context.ServicoCliente.Remove(servicoCliente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicoClienteExists(int id)
        {
            return _context.ServicoCliente.Any(e => e.Id == id);
        }
    }
}
