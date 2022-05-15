using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EPassport.Data;
using EPassport.Models;

namespace EPassport.Controllers
{
    public class PassportOfficesController : Controller
    {
        private readonly EPassportContext _context;

        public PassportOfficesController(EPassportContext context)
        {
            _context = context;
        }

        // GET: PassportOffices
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.PassportOffice.Include(p => p.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: PassportOffices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PassportOffice == null)
            {
                return NotFound();
            }

            var passportOffice = await _context.PassportOffice
                .Include(p => p.Applicant)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (passportOffice == null)
            {
                return NotFound();
            }

            return View(passportOffice);
        }

        // GET: PassportOffices/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District");
            return View();
        }

        // POST: PassportOffices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfficeId,OfficeName,Jurisdiction,Address,PhoneNumber,ApplicantId")] PassportOffice passportOffice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passportOffice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", passportOffice.ApplicantId);
            return View(passportOffice);
        }

        // GET: PassportOffices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PassportOffice == null)
            {
                return NotFound();
            }

            var passportOffice = await _context.PassportOffice.FindAsync(id);
            if (passportOffice == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", passportOffice.ApplicantId);
            return View(passportOffice);
        }

        // POST: PassportOffices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfficeId,OfficeName,Jurisdiction,Address,PhoneNumber,ApplicantId")] PassportOffice passportOffice)
        {
            if (id != passportOffice.OfficeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passportOffice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassportOfficeExists(passportOffice.OfficeId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", passportOffice.ApplicantId);
            return View(passportOffice);
        }

        // GET: PassportOffices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PassportOffice == null)
            {
                return NotFound();
            }

            var passportOffice = await _context.PassportOffice
                .Include(p => p.Applicant)
                .FirstOrDefaultAsync(m => m.OfficeId == id);
            if (passportOffice == null)
            {
                return NotFound();
            }

            return View(passportOffice);
        }

        // POST: PassportOffices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PassportOffice == null)
            {
                return Problem("Entity set 'EPassportContext.PassportOffice'  is null.");
            }
            var passportOffice = await _context.PassportOffice.FindAsync(id);
            if (passportOffice != null)
            {
                _context.PassportOffice.Remove(passportOffice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassportOfficeExists(int id)
        {
          return (_context.PassportOffice?.Any(e => e.OfficeId == id)).GetValueOrDefault();
        }
    }
}
