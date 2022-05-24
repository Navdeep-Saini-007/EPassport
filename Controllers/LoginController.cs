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
    public class LoginController : Controller
    {
        private readonly EPassportContext _context;

        public LoginController(EPassportContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            var ePassportContext = _context.LoginCredential.Include(l => l.Login);
            return View(await ePassportContext.ToListAsync());
        }

        // GET: Login/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoginCredential == null)
            {
                return NotFound();
            }

            var loginCredential = await _context.LoginCredential
                .Include(l => l.Login)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginCredential == null)
            {
                return NotFound();
            }

            return View(loginCredential);
        }

        // GET: Login/Create
        public IActionResult Create()
        {
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId");
            return View();
        }

        // POST: Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserType,LoginId,Password,RememberMe")] LoginCredential loginCredential)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginCredential);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", loginCredential.LoginId);
            return View(loginCredential);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoginCredential == null)
            {
                return NotFound();
            }

            var loginCredential = await _context.LoginCredential.FindAsync(id);
            if (loginCredential == null)
            {
                return NotFound();
            }
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", loginCredential.LoginId);
            return View(loginCredential);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserType,LoginId,Password,RememberMe")] LoginCredential loginCredential)
        {
            if (id != loginCredential.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginCredential);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoginCredentialExists(loginCredential.Id))
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
            ViewData["LoginId"] = new SelectList(_context.Set<RegistrationDetail>(), "LoginId", "LoginId", loginCredential.LoginId);
            return View(loginCredential);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoginCredential == null)
            {
                return NotFound();
            }

            var loginCredential = await _context.LoginCredential
                .Include(l => l.Login)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loginCredential == null)
            {
                return NotFound();
            }

            return View(loginCredential);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoginCredential == null)
            {
                return Problem("Entity set 'EPassportContext.LoginCredential'  is null.");
            }
            var loginCredential = await _context.LoginCredential.FindAsync(id);
            if (loginCredential != null)
            {
                _context.LoginCredential.Remove(loginCredential);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoginCredentialExists(int id)
        {
          return _context.LoginCredential.Any(e => e.Id == id);
        }
    }
}
