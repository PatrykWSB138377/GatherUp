using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GatherUp.Data;
using GatherUp.Models;
using GatherUp.Data.Migrations;

namespace GatherUp
{
    public class EventJoinRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventJoinRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventJoinRequests
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventJoinRequest.ToListAsync());
        }

        // GET: EventJoinRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJoinRequest = await _context.EventJoinRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJoinRequest == null)
            {
                return NotFound();
            }

            return View(eventJoinRequest);
        }

        // GET: EventJoinRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventJoinRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventJoinRequest eventJoinRequest)
        {
            Console.WriteLine("HEREEEEEEEEEEEEEEEEEEEEEEE");
            var eventJoinRequestAlreadyExists = _context.EventJoinRequest.Where(ejr => ejr.EventId == eventJoinRequest.EventId && ejr.SenderUserId == eventJoinRequest.SenderUserId).Any();

            if (eventJoinRequestAlreadyExists)
            {
                return Json(new { success = false, message = "Join request already exists" });
            }

            var eventData = _context.Event.FirstOrDefault(e => e.Id == eventJoinRequest.EventId);
            if (eventData == null)
            {
                return NotFound();
            }

            Console.WriteLine(eventData.UserId!);
            eventJoinRequest.ReceiverUserId = eventData.UserId!;
            eventJoinRequest.CreatedDate = DateTime.Now;
            eventJoinRequest.Status = InvitationStatus.Pending;

            ModelState.Remove(nameof(eventJoinRequest.ReceiverUserId)); // for some reason validation keeps failing on receiver id field...
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(eventJoinRequest);
                await _context.SaveChangesAsync();
                return Json(new { success = true, joinRequestId = eventJoinRequest.Id });
            }

            return Json(new { success = false, message = "There was an error during join request action" });
        }

        // GET: EventJoinRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJoinRequest = await _context.EventJoinRequest.FindAsync(id);
            if (eventJoinRequest == null)
            {
                return NotFound();
            }
            return View(eventJoinRequest);
        }

        // POST: EventJoinRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventId,SenderUserId,ReceiverUserId,CreatedDate,ResolvedDate,Status")] EventJoinRequest eventJoinRequest)
        {
            if (id != eventJoinRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventJoinRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventJoinRequestExists(eventJoinRequest.Id))
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
            return View(eventJoinRequest);
        }

        // GET: EventJoinRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventJoinRequest = await _context.EventJoinRequest
                .FirstOrDefaultAsync(m => m.Id == id);
            if (eventJoinRequest == null)
            {
                return NotFound();
            }

            return View(eventJoinRequest);
        }


        public class DeleteJoinRequestRequest
        {
            public int Id { get; set; }
        }

        // POST: EventJoinRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromBody] DeleteJoinRequestRequest request)
        {
            var eventJoinRequest = await _context.EventJoinRequest.FindAsync(request.Id);
            if (eventJoinRequest != null)
            {
                _context.EventJoinRequest.Remove(eventJoinRequest);
            }

            await _context.SaveChangesAsync();
            return Json(new { });
        }

        private bool EventJoinRequestExists(int id)
        {
            return _context.EventJoinRequest.Any(e => e.Id == id);
        }
    }
}
