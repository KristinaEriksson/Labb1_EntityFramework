using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Labb1_EntityFramework.Data;
using Labb1_EntityFramework.Models;

namespace Labb1_EntityFramework.Controllers
{
    public class TypeOfLeaveController : Controller
    {
        private readonly ForzaDbContext _context;

        public TypeOfLeaveController(ForzaDbContext context)
        {
            _context = context;
        }

        // GET: TypeOfLeave
        public async Task<IActionResult> Index()
        {
              return _context.TypeOfLeaves != null ? 
                          View(await _context.TypeOfLeaves.ToListAsync()) :
                          Problem("Entity set 'ForzaDbContext.TypeOfLeaves'  is null.");
        }

        // GET: TypeOfLeave/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TypeOfLeaves == null)
            {
                return NotFound();
            }

            var typeOfLeave = await _context.TypeOfLeaves
                .FirstOrDefaultAsync(m => m.LeaveTypeID == id);
            if (typeOfLeave == null)
            {
                return NotFound();
            }

            return View(typeOfLeave);
        }

        // GET: TypeOfLeave/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeOfLeave/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveTypeID,LeaveType")] TypeOfLeave typeOfLeave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeOfLeave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfLeave);
        }

        // GET: TypeOfLeave/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TypeOfLeaves == null)
            {
                return NotFound();
            }

            var typeOfLeave = await _context.TypeOfLeaves.FindAsync(id);
            if (typeOfLeave == null)
            {
                return NotFound();
            }
            return View(typeOfLeave);
        }

        // POST: TypeOfLeave/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveTypeID,LeaveType")] TypeOfLeave typeOfLeave)
        {
            if (id != typeOfLeave.LeaveTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeOfLeave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeOfLeaveExists(typeOfLeave.LeaveTypeID))
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
            return View(typeOfLeave);
        }

        // GET: TypeOfLeave/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TypeOfLeaves == null)
            {
                return NotFound();
            }

            var typeOfLeave = await _context.TypeOfLeaves
                .FirstOrDefaultAsync(m => m.LeaveTypeID == id);
            if (typeOfLeave == null)
            {
                return NotFound();
            }

            return View(typeOfLeave);
        }

        // POST: TypeOfLeave/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TypeOfLeaves == null)
            {
                return Problem("Entity set 'ForzaDbContext.TypeOfLeaves'  is null.");
            }
            var typeOfLeave = await _context.TypeOfLeaves.FindAsync(id);
            if (typeOfLeave != null)
            {
                _context.TypeOfLeaves.Remove(typeOfLeave);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeOfLeaveExists(int id)
        {
          return (_context.TypeOfLeaves?.Any(e => e.LeaveTypeID == id)).GetValueOrDefault();
        }
    }
}
