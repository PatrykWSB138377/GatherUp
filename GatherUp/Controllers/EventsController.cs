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
using Microsoft.AspNetCore.Authorization;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace GatherUp.Controllers
{
    public class BaseController : Controller
    {
        protected string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
        protected List<SelectListItem>? ImageSelectList = ImageMappings.LabelToFilename.Select(mapping => new SelectListItem
        {
            Value = mapping.Key,
            Text = mapping.Value
        }).ToList();
    }


    public class EventsController : BaseController
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
                .OrderBy(e => e.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Location = e.Location,
                    Date = e.Date,
                    Image = e.Image,
                    UserId = e.UserId,
                    UserName = e.User.UserName,
                    IsUserEvent = e.UserId == CurrentUserId,
                    UserFollow = _context.EventFollow
                        .Where(ef => ef.EventId == e.Id && ef.UserId == CurrentUserId)
                        .FirstOrDefault(),
                    UserJoinRequest = _context.EventJoinRequest
                        .Where(ejr => ejr.EventId == e.Id && ejr.SenderUserId == CurrentUserId)
                        .FirstOrDefault()
                })
                .ToListAsync();


            var viewModel = new PagedListViewModel<EventViewModel>
            {
                Items = events,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize),
                PageSize = pageSize,
                TotalCount = totalEvents
            };

            return View(viewModel);
        }

        public async Task<IActionResult> MyEvents(int pageNumber = 1, int pageSize = 10)
        {
            var events = await _context.Event
            .Where(e => e.UserId == CurrentUserId)
            .OrderBy(e => e.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                Date = e.Date,
                Image = e.Image,
                UserId = e.UserId,
                UserName = e.User.UserName,
                IsUserEvent = e.UserId == CurrentUserId,
            })
            .ToListAsync();
            

            int totalEvents = events.Count();

            var viewModel = new PagedListViewModel<EventViewModel>
            {
                Items = events,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize),
                PageSize = pageSize,
                TotalCount = totalEvents
            };

            return View("MyEvents", viewModel);
        }

        public async Task<IActionResult> FollowedEvents(int pageNumber = 1, int pageSize = 10)
        {
            var events = await _context.Event
            .Where(e => _context.EventFollow.Any(ef => ef.EventId == e.Id && ef.UserId == CurrentUserId))
            .OrderBy(e => e.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(e => new EventViewModel
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                Date = e.Date,
                Image = e.Image,
                UserId = e.UserId,
                UserName = e.User.UserName,
                IsUserEvent = e.UserId == CurrentUserId,
                UserFollow = _context.EventFollow
                        .Where(ef => ef.EventId == e.Id && ef.UserId == CurrentUserId)
                        .FirstOrDefault(),
                UserJoinRequest = _context.EventJoinRequest
                        .Where(ejr => ejr.EventId == e.Id && ejr.SenderUserId == CurrentUserId)
                        .FirstOrDefault()
            })
            .ToListAsync();


            int totalEvents = events.Count();

            var viewModel = new PagedListViewModel<EventViewModel>
            {
                Items = events,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalEvents / (double)pageSize),
                PageSize = pageSize,
                TotalCount = totalEvents
            };

            return View("FollowedEvents", viewModel);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.Include(e => e.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@event == null)
            {
                return NotFound();
            }

            var follow = await _context.EventFollow.FirstOrDefaultAsync(ef =>  ef.UserId == CurrentUserId && ef.EventId == @event.Id);
            var joinRequest = await _context.EventJoinRequest.FirstOrDefaultAsync(ejr => ejr.SenderUserId == CurrentUserId && ejr.EventId == @event.Id);

            var isUserEvent = @event.UserId == CurrentUserId;

            var participantIds = await _context.EventJoinRequest.Where(ejr => ejr.Status == InvitationStatus.Accepted && ejr.EventId == @event.Id).Select(ejr => ejr.SenderUserId).ToListAsync();
            var participantUsernames = await _context.Users
            .Where(user => participantIds.Contains(user.Id))
            .Select(user => user.UserName)
            .ToListAsync();

            participantUsernames.Insert(0, _context.Users.FirstOrDefault(u => u.Id == @event.UserId).UserName); // add event's creator

            EventViewModel eventViewModel = new EventViewModel
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                Location = @event.Location,
                Date = @event.Date,
                Image = @event.Image,
                UserName = @event.User.UserName,
                UserId = @event.UserId,
                IsUserEvent = isUserEvent,
                UserFollow = follow,
                UserJoinRequest = joinRequest,
                Participants = participantUsernames,
            };

            return View(eventViewModel);
        }

        // GET: Events/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["imageItems"] = ImageSelectList;

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Location,Date,Image")] Event @event)
        {
            @event.UserId = CurrentUserId;

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["imageItems"] = ImageSelectList;

            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
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

            if (@event.UserId != CurrentUserId)
            {
                return Forbid();
            };

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Location,Date,Image")] Event @event)
        {
            ViewData["imageItems"] = ImageSelectList;

            if (id != @event.Id)
            {
                return NotFound();
            }

   

            var existingEventUserId = await _context.Event
              .Where(e => e.Id == id)
              .Select(e => e.UserId)
              .FirstOrDefaultAsync();

            if (existingEventUserId != CurrentUserId)
            {
                return Forbid();
            };

            @event.UserId = CurrentUserId;

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


        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event != null)
            {
                _context.Event.Remove(@event);
            }

            if (@event.UserId != CurrentUserId)
            {
                return Forbid();
            };

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
