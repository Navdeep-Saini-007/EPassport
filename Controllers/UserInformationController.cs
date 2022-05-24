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
    public class UserInformationController : Controller
    {
        private readonly EPassportContext _context;

        public UserInformationController(EPassportContext context)
        {
            _context = context;
        }

        // GET: ApplicationDetails
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.ApplicationDetail.Include(a => a.Login);
            return View(await ePassportContext.ToListAsync());
        }

        public async Task<IActionResult> Personal()
        {
            var ePassportContext = _context.ApplicationDetail.Include(a => a.Login);
            return View(await ePassportContext.ToListAsync());
        }

        public async Task<IActionResult> Update()
        {
            //var ePassportContext = _context.ApplicationDetail.Include(a => a.Login);
            //return View(await ePassportContext.ToListAsync());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([Bind("ApplicantId")] ApplicationDetail applicationDetail)
        {
            return RedirectToAction(actionName: "Edit", new { id = applicationDetail.ApplicantId });
        }


        // GET: ApplicationDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail
                .Include(a => a.Login)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            return View(applicationDetail);
        }

        // GET: ApplicationDetails/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail.FindAsync(id);
            if (applicationDetail == null)
            {
                return NotFound();
            }
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", applicationDetail.LoginId);
            return View(applicationDetail);
        }

        // POST: ApplicationDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,FirstName,LastName,Dob,PlaceOfBirth,Gender,State,District,MaritalStatus,Pan,EmploymentType,EducationalQualification,LoginId,PassportStatus")] ApplicationDetail applicationDetail)
        {
            if (id != applicationDetail.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationDetailExists(applicationDetail.ApplicantId))
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
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", applicationDetail.LoginId);
            return View(applicationDetail);
        }

        private bool ApplicationDetailExists(int id)
        {
            return _context.ApplicationDetail.Any(e => e.ApplicantId == id);
        }

        // GET: ApplicationDetails/Create
        //public IActionResult Create()
        //{
        //    ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId");
        //    return View();
        //}

        // POST: ApplicationDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("ApplicantId,FirstName,LastName,Dob,PlaceOfBirth,Gender,State,District,MaritalStatus,Pan,EmploymentType,EducationalQualification,LoginId")] ApplicationDetail applicationDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(applicationDetail);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", applicationDetail.LoginId);
        //    return View(applicationDetail);
        //}

        // GET: ApplicationDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationDetail == null)
            {
                return NotFound();
            }

            var applicationDetail = await _context.ApplicationDetail
                .Include(a => a.Login)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicationDetail == null)
            {
                return NotFound();
            }

            return View(applicationDetail);
        }

        // POST: ApplicationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ApplicationDetail == null)
            {
                return Problem("Entity set 'EPassportContext.ApplicationDetail'  is null.");
            }
            var applicationDetail = await _context.ApplicationDetail.FindAsync(id);
            if (applicationDetail != null)
            {
                _context.ApplicationDetail.Remove(applicationDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
