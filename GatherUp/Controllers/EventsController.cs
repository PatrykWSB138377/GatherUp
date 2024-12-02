using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GatherUp.Data;
using GatherUp.Models;
using GatherUp.Utils;
using GatherUp.Models.ViewModels;
using Microsoft.EntityFrameworkCore; 

namespace GatherUp.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10) 
        {
            int totalEvents = await _context.Event.CountAsync(); 
            var events = await _context.Event
                .OrderBy(e => e.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(); 

            var viewModel = new PagedListViewModel<Event>
            {
                Items = events,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize),
                PageSize = pageSize,
                TotalCount = totalEvents
            };

            return View(viewModel);
        }

// GET: Events/Details/5
public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var ImageSelectList = ImageMappings.LabelToFilename.Select(mapping => new SelectListItem
            {
                Value = mapping.Key,
                Text = mapping.Value
            }).ToList();

            ViewData["imageItems"] = ImageSelectList;

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,Date,Image")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var ImageSelectList = ImageMappings.LabelToFilename.Select(mapping => new SelectListItem
            {
                Value = mapping.Key,
                Text = mapping.Value
            }).ToList();

            ViewData["imageItems"] = ImageSelectList;

            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var ImageSelectList = ImageMappings.LabelToFilename.Select(mapping => new SelectListItem
            {
                Value = mapping.Key,
                Text = mapping.Value
            }).ToList();

            ViewData["imageItems"] = ImageSelectList;


            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Date,Image")] Event @event)
        {
            var ImageSelectList = ImageMappings.LabelToFilename.Select(mapping => new SelectListItem
            {
                Value = mapping.Key,
                Text = mapping.Value
            }).ToList();

            ViewData["imageItems"] = ImageSelectList;


            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
