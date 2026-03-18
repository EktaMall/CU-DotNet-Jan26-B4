using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WealthTrack.Data;
using WealthTrack.Models;
using WealthTrack.Models.ViewModel;

namespace WealthTrack.Controllers
{
    public class InvestmentsController : Controller
    {
        private readonly WealthTrackContext _context;

        public InvestmentsController(WealthTrackContext context)
        {
            _context = context;
        }

        // INDEX
        public async Task<IActionResult> Index()
        {
            return View(await _context.Investment.ToListAsync());
        }

        // DETAILS 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var investment = await _context.Investment
                .FirstOrDefaultAsync(m => m.Id == id);

            if (investment == null)
                return NotFound();

            return View(investment);
        }

        //  CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InvestmentCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var investment = new Investment
                {
                    TickerSymbol = vm.TickerSymbol,
                    AssetName = vm.AssetName,
                    PurchasePrice = vm.Price,
                    Quantity = vm.Quantity,
                    PurchaseDate = DateTime.Now
                };

                _context.Add(investment);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(vm);
        }

        //  EDIT GET 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var investment = await _context.Investment.FindAsync(id);

            if (investment == null)
                return NotFound();

            return View(investment);
        }

        //  EDIT POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TickerSymbol,AssetName,PurchasePrice,Quantity,PurchaseDate")] Investment investment)
        {
            if (id != investment.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(investment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvestmentExists(investment.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(investment);
        }

        //  DELETE GET 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var investment = await _context.Investment
                .FirstOrDefaultAsync(m => m.Id == id);

            if (investment == null)
                return NotFound();

            return View(investment);
        }

        //  DELETE POST 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var investment = await _context.Investment.FindAsync(id);

            if (investment != null)
                _context.Investment.Remove(investment);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //  HELPER 
        private bool InvestmentExists(int id)
        {
            return _context.Investment.Any(e => e.Id == id);
        }
    }
}