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
    public class UserFamilyDetailsController : Controller
    {
        private readonly EPassportContext _context;

        public UserFamilyDetailsController(EPassportContext context)
        {
            _context = context;
        }

        // GET: UserFamilyDetails
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.FamilyDetail.Include(f => f.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: UserFamilyDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FamilyDetail == null)
            {
                return NotFound();
            }

            var familyDetail = await _context.FamilyDetail
                .Include(f => f.Applicant)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (familyDetail == null)
            {
                return NotFound();
            }

            return View(familyDetail);
        }

        // GET: UserFamilyDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District");
            return View();
        }

        // POST: UserFamilyDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FathersName,MothersName,ApplicantId")] FamilyDetail familyDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(familyDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", familyDetail.ApplicantId);
            return View(familyDetail);
        }

        // GET: UserFamilyDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FamilyDetail == null)
            {
                return NotFound();
            }

            var familyDetail = await _context.FamilyDetail.FindAsync(id);
            if (familyDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", familyDetail.ApplicantId);
            return View(familyDetail);
        }

        // POST: UserFamilyDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FathersName,MothersName,ApplicantId")] FamilyDetail familyDetail)
        {
            if (id != familyDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(familyDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilyDetailExists(familyDetail.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", familyDetail.ApplicantId);
            return View(familyDetail);
        }

        // GET: UserFamilyDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FamilyDetail == null)
            {
                return NotFound();
            }

            var familyDetail = await _context.FamilyDetail
                .Include(f => f.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (familyDetail == null)
            {
                return NotFound();
            }

            return View(familyDetail);
        }

        // POST: UserFamilyDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FamilyDetail == null)
            {
                return Problem("Entity set 'EPassportContext.FamilyDetail'  is null.");
            }
            var familyDetail = await _context.FamilyDetail.FindAsync(id);
            if (familyDetail != null)
            {
                _context.FamilyDetail.Remove(familyDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilyDetailExists(int id)
        {
          return _context.FamilyDetail.Any(e => e.Id == id);
        }
    }
}
