using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Data;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class VenuesController : Controller
    {
        private readonly EventEaseContext _context;

        public VenuesController(EventEaseContext context)
        {
            _context = context;
        }

        // GET: Venues
        public async Task<IActionResult> Index()
        {
            // Get all venues and pass them to the view
            return View(await _context.Venue.ToListAsync());
        }

        // GET: Venues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.VenueId == id);

            if (venue == null)
            {
                return NotFound(); // Return NotFound if venue doesn't exist
            }

            return View(venue); // Pass the venue to the view
        }

        // GET: Venues/Create
        public IActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: Venues/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,Location,Capacity")] Venue venue)
        {
            if (ModelState.IsValid) // Ensure the data is valid
            {
                _context.Add(venue); // Add the venue to the database context
                await _context.SaveChangesAsync(); // Save changes to the database
                return RedirectToAction(nameof(Index)); // Redirect to the index view
            }

            return View(venue); // If the model is invalid, return the Create view with the current venue data
        }

        // GET: Venues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            var venue = await _context.Venue.FindAsync(id); // Find the venue by id
            if (venue == null)
            {
                return NotFound(); // Return NotFound if venue doesn't exist
            }

            return View(venue); // Pass the venue to the view for editing
        }

        // POST: Venues/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,Location,Capacity")] Venue venue)
        {
            if (id != venue.VenueId)
            {
                return NotFound(); // Return NotFound if the id doesn't match the venue
            }

            if (ModelState.IsValid) // Ensure the data is valid
            {
                try
                {
                    _context.Update(venue); // Update the venue in the database
                    await _context.SaveChangesAsync(); // Save changes to the database
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueId)) // Check if venue still exists
                    {
                        return NotFound(); // Return NotFound if venue doesn't exist
                    }
                    else
                    {
                        throw; // Rethrow exception if a concurrency issue occurs
                    }
                }
                return RedirectToAction(nameof(Index)); // Redirect to the index view after a successful edit
            }

            return View(venue); // If the model is invalid, return the Edit view with the current venue data
        }

        // GET: Venues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); // Return NotFound if id is null
            }

            var venue = await _context.Venue
                .FirstOrDefaultAsync(m => m.VenueId == id);

            if (venue == null)
            {
                return NotFound(); // Return NotFound if venue doesn't exist
            }

            return View(venue); // Pass the venue to the view for deletion confirmation
        }

        // POST: Venues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venue = await _context.Venue.FindAsync(id); // Find the venue by id
            if (venue != null)
            {
                _context.Venue.Remove(venue); // Remove the venue from the database
            }

            await _context.SaveChangesAsync(); // Save changes to the database
            return RedirectToAction(nameof(Index)); // Redirect to the index view after deletion
        }

        // Helper method to check if venue exists
        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueId == id); // Check if venue exists in the database
        }
    }
}
