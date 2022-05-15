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
    public class AppointmentDetailsController : Controller
    {
        private readonly EPassportContext _context;

        public AppointmentDetailsController(EPassportContext context)
        {
            _context = context;
        }

        // GET: AppointmentDetails
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.AppointmentDetail.Include(a => a.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: AppointmentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppointmentDetail == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.MonthId == id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }

            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District");
            return View();
        }

        // POST: AppointmentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonthId,MonthName,AvailableDays,TimeSlots,ApplicantId")] AppointmentDetail appointmentDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", appointmentDetail.ApplicantId);
            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppointmentDetail == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail.FindAsync(id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", appointmentDetail.ApplicantId);
            return View(appointmentDetail);
        }

        // POST: AppointmentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MonthId,MonthName,AvailableDays,TimeSlots,ApplicantId")] AppointmentDetail appointmentDetail)
        {
            if (id != appointmentDetail.MonthId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentDetailExists(appointmentDetail.MonthId))
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
            ViewData["ApplicantId"] = new SelectList(_context.Set<ApplicationDetail>(), "ApplicantId", "District", appointmentDetail.ApplicantId);
            return View(appointmentDetail);
        }

        // GET: AppointmentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppointmentDetail == null)
            {
                return NotFound();
            }

            var appointmentDetail = await _context.AppointmentDetail
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.MonthId == id);
            if (appointmentDetail == null)
            {
                return NotFound();
            }

            return View(appointmentDetail);
        }

        // POST: AppointmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppointmentDetail == null)
            {
                return Problem("Entity set 'EPassportContext.AppointmentDetail'  is null.");
            }
            var appointmentDetail = await _context.AppointmentDetail.FindAsync(id);
            if (appointmentDetail != null)
            {
                _context.AppointmentDetail.Remove(appointmentDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentDetailExists(int id)
        {
          return _context.AppointmentDetail.Any(e => e.MonthId == id);
        }
    }
}
