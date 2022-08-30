using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MoneyTransferController : Controller
    {
        private readonly AppDbContext _context;

        public MoneyTransferController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/MoneyTransfer
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MoneyTransfers.Include(m => m.Receiver).Include(m => m.Sender);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/MoneyTransfer/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.MoneyTransfers == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfers
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return View(moneyTransfer);
        }

        // GET: Admin/MoneyTransfer/Create
        public IActionResult Create()
        {
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "TelegrammId");
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "TelegrammId");
            return View();
        }

        // POST: Admin/MoneyTransfer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,SentTime,AcceptedTime,Type,SenderId,ReceiverId,ReceiverBankId,Id")] MoneyTransfer moneyTransfer)
        {
            if (ModelState.IsValid)
            {
                moneyTransfer.Id = Guid.NewGuid();
                _context.Add(moneyTransfer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.SenderId);
            return View(moneyTransfer);
        }

        // GET: Admin/MoneyTransfer/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.MoneyTransfers == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfers.FindAsync(id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.SenderId);
            return View(moneyTransfer);
        }

        // POST: Admin/MoneyTransfer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Amount,SentTime,AcceptedTime,Type,SenderId,ReceiverId,ReceiverBankId,Id")] MoneyTransfer moneyTransfer)
        {
            if (id != moneyTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moneyTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoneyTransferExists(moneyTransfer.Id))
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
            ViewData["ReceiverId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Users, "Id", "TelegrammId", moneyTransfer.SenderId);
            return View(moneyTransfer);
        }

        // GET: Admin/MoneyTransfer/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.MoneyTransfers == null)
            {
                return NotFound();
            }

            var moneyTransfer = await _context.MoneyTransfers
                .Include(m => m.Receiver)
                .Include(m => m.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moneyTransfer == null)
            {
                return NotFound();
            }

            return View(moneyTransfer);
        }

        // POST: Admin/MoneyTransfer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.MoneyTransfers == null)
            {
                return Problem("Entity set 'AppDbContext.MoneyTransfers'  is null.");
            }
            var moneyTransfer = await _context.MoneyTransfers.FindAsync(id);
            if (moneyTransfer != null)
            {
                _context.MoneyTransfers.Remove(moneyTransfer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoneyTransferExists(Guid id)
        {
          return (_context.MoneyTransfers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
