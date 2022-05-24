using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EPassport.Data;
using EPassport.Models;
using EPassport.ViewModel;

namespace EPassport.Controllers
{
    public class SupportingDocumentsController : Controller
    {
        private readonly EPassportContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;

        public SupportingDocumentsController(EPassportContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: SupportingDocuments
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.SupportingDocumentDetail.Include(s => s.Applicant);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: SupportingDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SupportingDocumentDetail == null)
            {
                return NotFound();
            }

            var supportingDocumentDetail = await _context.SupportingDocumentDetail
                .Include(s => s.Applicant)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (supportingDocumentDetail == null)
            {
                return NotFound();
            }

            return View(supportingDocumentDetail);
        }

        // GET: SupportingDocuments/Create
        public IActionResult Create()
        {
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District");
            return View();
        }

        // POST: SupportingDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            DocumentCreateViewModel supportingDocs)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                string uniqueFileName1 = null;
                if (supportingDocs.Document1 != null)
                {
                    string uploadsFolder=Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName=Guid.NewGuid().ToString() + "_" + supportingDocs.Document1.FileName;
                    string filePath=Path.Combine(uploadsFolder, uniqueFileName);
                    supportingDocs.Document1.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                if (supportingDocs.Document2 != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName1 = Guid.NewGuid().ToString() + "_" + supportingDocs.Document2.FileName;
                    string filePath1 = Path.Combine(uploadsFolder, uniqueFileName1);
                    supportingDocs.Document2.CopyTo(new FileStream(filePath1, FileMode.Create));
                }
                SupportingDocumentDetail supportingDocumentDetail = new SupportingDocumentDetail
                {
                    Document1 = uniqueFileName,
                    Document2 = uniqueFileName1,
                    ApplicantId = supportingDocs.ApplicantId
                };
                _context.Add(supportingDocumentDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("user","home");
            }
            //ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", supportingDocumentDetail.ApplicantId);
            return View();
        }

        // GET: SupportingDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SupportingDocumentDetail == null)
            {
                return NotFound();
            }

            var supportingDocumentDetail = await _context.SupportingDocumentDetail.FindAsync(id);
            if (supportingDocumentDetail == null)
            {
                return NotFound();
            }
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", supportingDocumentDetail.ApplicantId);
            return View(supportingDocumentDetail);
        }

        // POST: SupportingDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Document1,Document2,ApplicantId")] SupportingDocumentDetail supportingDocumentDetail)
        {
            if (id != supportingDocumentDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supportingDocumentDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportingDocumentDetailExists(supportingDocumentDetail.Id))
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
            ViewData["ApplicantId"] = new SelectList(_context.ApplicationDetail, "ApplicantId", "District", supportingDocumentDetail.ApplicantId);
            return View(supportingDocumentDetail);
        }

        // GET: SupportingDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SupportingDocumentDetail == null)
            {
                return NotFound();
            }

            var supportingDocumentDetail = await _context.SupportingDocumentDetail
                .Include(s => s.Applicant)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supportingDocumentDetail == null)
            {
                return NotFound();
            }

            return View(supportingDocumentDetail);
        }

        // POST: SupportingDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SupportingDocumentDetail == null)
            {
                return Problem("Entity set 'EPassportContext.SupportingDocumentDetail'  is null.");
            }
            var supportingDocumentDetail = await _context.SupportingDocumentDetail.FindAsync(id);
            if (supportingDocumentDetail != null)
            {
                _context.SupportingDocumentDetail.Remove(supportingDocumentDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportingDocumentDetailExists(int id)
        {
          return _context.SupportingDocumentDetail.Any(e => e.Id == id);
        }
    }
}
