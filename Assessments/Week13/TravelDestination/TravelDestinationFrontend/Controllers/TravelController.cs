using Microsoft.AspNetCore.Mvc;
using TravelDestinationFrontend.Models;
using TravelDestinationFrontend.Services;

namespace TravelDestinationFrontend.Controllers
{
    public class TravelController : Controller
    {
        private readonly IDestinationService _destinationService;

        public TravelController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public async Task<IActionResult> Index()
        {
            var destinations = await _destinationService.GetAllAsync();
            return View(destinations);
        }

        public async Task<IActionResult> Details(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DestinationViewModel destination)
        {
            if (!ModelState.IsValid)
            {
                return View(destination);
            }

            var success = await _destinationService.CreateAsync(destination);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to create destination.");
                return View(destination);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DestinationViewModel destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(destination);
            }

            var success = await _destinationService.UpdateAsync(destination);

            if (!success)
            {
                ModelState.AddModelError(string.Empty, "Failed to update destination.");
                return View(destination);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var destination = await _destinationService.GetByIdAsync(id);

            if (destination == null)
            {
                return NotFound();
            }

            return View(destination);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _destinationService.DeleteAsync(id);

            if (!success)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}