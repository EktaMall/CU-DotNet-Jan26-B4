using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinTrackPro.Data;
using FinTrackPro.Models;

namespace FinTrackPro.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TransactionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var transactions = _context.Transactions
                .Include(t => t.Account)
                .ToList();

            return View(transactions);
        }
        public IActionResult Create()
        {
            ViewBag.Accounts = new SelectList(_context.Accounts, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();

                TempData["Success"] = "Transaction added successfully!";

                return RedirectToAction("Index");
            }

            ViewBag.Accounts = new SelectList(_context.Accounts, "Id", "Name", transaction.AccountId);
            return View(transaction);
        }

        public IActionResult Details(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        public IActionResult Edit(int id)
        {
            var transaction = _context.Transactions.Find(id);

            if (transaction == null)
                return NotFound();

            ViewBag.Accounts = new SelectList(_context.Accounts, "Id", "Name", transaction.AccountId);

            return View(transaction);
        }

        [HttpPost]
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Transactions.Update(transaction);
                _context.SaveChanges();

                TempData["Success"] = "Transaction updated successfully!";

                return RedirectToAction("Index");
            }

            ViewBag.Accounts = new SelectList(_context.Accounts, "Id", "Name", transaction.AccountId);
            return View(transaction);
        }

        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions
                .Include(t => t.Account)
                .FirstOrDefault(t => t.Id == id);

            if (transaction == null)
                return NotFound();

            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var transaction = _context.Transactions.Find(id);

            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                _context.SaveChanges();

                TempData["Success"] = "Transaction deleted successfully!";
            }

            return RedirectToAction("Index");
        }
    }
}