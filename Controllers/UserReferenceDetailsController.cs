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
    public class UserReferenceDetailsController : Controller
    {
        private readonly EPassportContext _context;

        public UserReferenceDetailsController(EPassportContext context)
        {
            _context = context;
        }

        // GET: UserReferenceDetails
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.ReferenceDetail.Include(r => r.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: UserReferenceDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReferenceDetail == null)
            {
                return NotFound();
            }

            var referenceDetail = await _context.ReferenceDetail
                .Include(r => r.Applicant)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (referenceDetail == null)
            {
                return NotFound();
            }

            return View(referenceDetail);
        }

        // GET: UserReferenceDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District");
            return View();
        }

        // POST: UserReferenceDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReferenceName,Address,TelephoneNumber,ApplicantId")] ReferenceDetail referenceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referenceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", referenceDetail.ApplicantId);
            return View(referenceDetail);
        }

        // GET: UserReferenceDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReferenceDetail == null)
            {
                return NotFound();
            }

            var referenceDetail = await _context.ReferenceDetail.FindAsync(id);
            if (referenceDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", referenceDetail.ApplicantId);
            return View(referenceDetail);
        }

        // POST: UserReferenceDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ReferenceName,Address,TelephoneNumber,ApplicantId")] ReferenceDetail referenceDetail)
        {
            if (id != referenceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referenceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReferenceDetailExists(referenceDetail.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", referenceDetail.ApplicantId);
            return View(referenceDetail);
        }

        // GET: UserReferenceDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReferenceDetail == null)
            {
                return NotFound();
            }

            var referenceDetail = await _context.ReferenceDetail
                .Include(r => r.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (referenceDetail == null)
            {
                return NotFound();
            }

            return View(referenceDetail);
        }

        // POST: UserReferenceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReferenceDetail == null)
            {
                return Problem("Entity set 'EPassportContext.ReferenceDetail'  is null.");
            }
            var referenceDetail = await _context.ReferenceDetail.FindAsync(id);
            if (referenceDetail != null)
            {
                _context.ReferenceDetail.Remove(referenceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReferenceDetailExists(int id)
        {
          return _context.ReferenceDetail.Any(e => e.Id == id);
        }
    }
}
