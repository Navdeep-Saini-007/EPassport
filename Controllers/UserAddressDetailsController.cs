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
    public class UserAddressDetailsController : Controller
    {
        private readonly EPassportContext _context;

        public UserAddressDetailsController(EPassportContext context)
        {
            _context = context;
        }

        // GET: UserAddressDetails
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.AddressDetail.Include(a => a.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: UserAddressDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AddressDetail == null)
            {
                return NotFound();
            }

            var addressDetail = await _context.AddressDetail
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressDetail == null)
            {
                return NotFound();
            }

            return View(addressDetail);
        }

        // GET: UserAddressDetails/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District");
            return View();
        }

        // POST: UserAddressDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HouseNo,StreetName,City,State,District,Pincode,TelephoneNumber,EmailId,ApplicantId")] AddressDetail addressDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(addressDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", addressDetail.ApplicantId);
            return View(addressDetail);
        }

        // GET: UserAddressDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AddressDetail == null)
            {
                return NotFound();
            }

            var addressDetail = await _context.AddressDetail.FindAsync(id);
            if (addressDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", addressDetail.ApplicantId);
            return View(addressDetail);
        }

        // POST: UserAddressDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HouseNo,StreetName,City,State,District,Pincode,TelephoneNumber,EmailId,ApplicantId")] AddressDetail addressDetail)
        {
            if (id != addressDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addressDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressDetailExists(addressDetail.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", addressDetail.ApplicantId);
            return View(addressDetail);
        }

        // GET: UserAddressDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AddressDetail == null)
            {
                return NotFound();
            }

            var addressDetail = await _context.AddressDetail
                .Include(a => a.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (addressDetail == null)
            {
                return NotFound();
            }

            return View(addressDetail);
        }

        // POST: UserAddressDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AddressDetail == null)
            {
                return Problem("Entity set 'EPassportContext.AddressDetail'  is null.");
            }
            var addressDetail = await _context.AddressDetail.FindAsync(id);
            if (addressDetail != null)
            {
                _context.AddressDetail.Remove(addressDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressDetailExists(int id)
        {
          return _context.AddressDetail.Any(e => e.Id == id);
        }
    }
}
