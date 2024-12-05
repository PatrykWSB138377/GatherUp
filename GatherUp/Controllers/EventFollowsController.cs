using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GatherUp.Data;
using GatherUp.Models;

namespace GatherUp.Controllers
{
    public class EventFollowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventFollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EventFollows
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.EventFollow.Include(e => e.Event).Include(e => e.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}


        // POST: EventFollows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventFollow eventFollow)
        {

            var followAlreadyExist = _context.EventFollow.Where(ef => ef.EventId == eventFollow.EventId && ef.UserId == eventFollow.UserId).Any();
            Console.WriteLine(followAlreadyExist);
            if (ModelState.IsValid && !followAlreadyExist)
            {
                _context.Add(eventFollow);
                await _context.SaveChangesAsync();

                return Json(new { success = true, followId = eventFollow.Id });
            }

            return Json(new { success = false, message = "There was an error during follow action" });
        }


        public class DeleteFollowRequest
        {
            public int Id { get; set; }
        }

        // POST: EventFollows/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed([FromBody] DeleteFollowRequest request)
        {
            Console.WriteLine(request.Id);

            var eventFollow = await _context.EventFollow.FindAsync(request.Id);
            Console.WriteLine("HEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
            Console.WriteLine(eventFollow);
            if (eventFollow != null)
            {
                _context.EventFollow.Remove(eventFollow);
            }

            await _context.SaveChangesAsync();

            return Json(new { });
        }

        private bool EventFollowExists(int id)
        {
            return _context.EventFollow.Any(e => e.Id == id);
        }
    }
}
