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
    public class LeaveListController : Controller
    {
        private readonly ForzaDbContext _context;

        public LeaveListController(ForzaDbContext context)
        {
            _context = context;
        }

        // GET: LeaveList by name
        public async Task<IActionResult> Index(String SearchString)
        {
            var forzaDbContext = _context.LeaveLists.Include(l => l.Employees).Include(l => l.TypeOfLeaves);

            if (!String.IsNullOrEmpty(SearchString))
            {
                return View("Index", await forzaDbContext.Where(emp => emp.Employees.FirstName.Contains(SearchString)).ToListAsync());
            }
            return View(await forzaDbContext.ToListAsync());
        }

        // GET: LeaveList by month search, only admin
        public async Task<IActionResult> SearchMonth(DateTime? SearchMonth)
        {
            var leaveLists = from ll in _context.LeaveLists
                             where (!SearchMonth.HasValue || (ll.RegisteredDate.Year == SearchMonth.Value.Year && ll.RegisteredDate.Month == SearchMonth.Value.Month))
                             select ll;

            return View(await leaveLists.Include(emp => emp.Employees).Include(l => l.TypeOfLeaves).ToListAsync());
        }

        // GET: LeaveList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(emp => emp.Employees)
                .Include(l => l.TypeOfLeaves)
                .FirstOrDefaultAsync(m => m.LeaveListID == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // GET: LeaveList/Create
        public IActionResult Create()
        {
            ViewData["FK_EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName");
            ViewData["FK_LeaveTypeID"] = new SelectList(_context.TypeOfLeaves, "LeaveTypeID", "LeaveType");
            return View();
        }

        // POST: LeaveList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeaveListID,FK_LeaveTypeID,FK_EmployeeID,StartDate,EndDate")] LeaveList leaveList)
        {
            if (ModelState.IsValid)
            {
                leaveList.RegisteredDate = DateTime.Now; // Sets RegisteredDate to current date and time
                _context.Add(leaveList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FK_EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName", leaveList.FK_EmployeeID);
            ViewData["FK_LeaveTypeID"] = new SelectList(_context.TypeOfLeaves, "LeaveTypeID", "LeaveType", leaveList.FK_LeaveTypeID);
            return View(leaveList);
        }

        // GET: LeaveList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList == null)
            {
                return NotFound();
            }
            ViewData["FK_EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName", leaveList.FK_EmployeeID);
            ViewData["FK_LeaveTypeID"] = new SelectList(_context.TypeOfLeaves, "LeaveTypeID", "LeaveType", leaveList.FK_LeaveTypeID);
            return View(leaveList);
        }

        // POST: LeaveList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeaveListID,FK_LeaveTypeID,FK_EmployeeID,StartDate,EndDate,RegisteredDate")] LeaveList leaveList)
        {
            if (id != leaveList.LeaveListID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leaveList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveListExists(leaveList.LeaveListID))
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
            ViewData["FK_EmployeeID"] = new SelectList(_context.Employees, "EmployeeID", "FullName", leaveList.FK_EmployeeID);
            ViewData["FK_LeaveTypeID"] = new SelectList(_context.TypeOfLeaves, "LeaveTypeID", "LeaveType", leaveList.FK_LeaveTypeID);
            return View(leaveList);
        }

        // GET: LeaveList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LeaveLists == null)
            {
                return NotFound();
            }

            var leaveList = await _context.LeaveLists
                .Include(emp =>emp.Employees)
                .Include(l => l.TypeOfLeaves)
                .FirstOrDefaultAsync(m => m.LeaveListID == id);
            if (leaveList == null)
            {
                return NotFound();
            }

            return View(leaveList);
        }

        // POST: LeaveList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LeaveLists == null)
            {
                return Problem("Entity set 'ForzaDbContext.LeaveLists'  is null.");
            }
            var leaveList = await _context.LeaveLists.FindAsync(id);
            if (leaveList != null)
            {
                _context.LeaveLists.Remove(leaveList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveListExists(int id)
        {
          return (_context.LeaveLists?.Any(e => e.LeaveListID == id)).GetValueOrDefault();
        }
    }
}
