using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GatherUp.Data;
using GatherUp.Models;

namespace GatherUp
{
    public class EventFollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventFollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventFollows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.EventFollow.Include(e => e.Event).Include(e => e.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EventFollows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventFollow = await _context.EventFollow
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventFollow == null)
            {
                return NotFound();
            }

            return View(eventFollow);
        }

        // GET: EventFollows/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: EventFollows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,EventId")] EventFollow eventFollow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventFollow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", eventFollow.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventFollow.UserId);
            return View(eventFollow);
        }

        // GET: EventFollows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventFollow = await _context.EventFollow.FindAsync(id);
            if (eventFollow == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", eventFollow.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventFollow.UserId);
            return View(eventFollow);
        }

        // POST: EventFollows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,EventId")] EventFollow eventFollow)
        {
            if (id != eventFollow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventFollow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventFollowExists(eventFollow.Id))
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
            ViewData["EventId"] = new SelectList(_context.Event, "Id", "Id", eventFollow.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", eventFollow.UserId);
            return View(eventFollow);
        }

        // GET: EventFollows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventFollow = await _context.EventFollow
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventFollow == null)
            {
                return NotFound();
            }

            return View(eventFollow);
        }

        // POST: EventFollows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventFollow = await _context.EventFollow.FindAsync(id);
            if (eventFollow != null)
            {
                _context.EventFollow.Remove(eventFollow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventFollowExists(int id)
        {
            return _context.EventFollow.Any(e => e.Id == id);
        }
    }
}
