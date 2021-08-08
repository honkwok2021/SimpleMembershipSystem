using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMembershipSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleMembershipSystem.Controllers
{
    public class MembershipController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MembershipController(ApplicationDbContext context)
        {
            _context = context;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var member = await _context.Members.ToListAsync();

        //    return View(member);
        //}

        public async Task<IActionResult> Index(string search)
        {
            var member = from m in _context.Members
                         select m;
            if (!String.IsNullOrEmpty(search))
            {
                member = member.Where(s => s.LastName.Contains(search) );

            }
            
            return View(await member.ToListAsync());
        }

        public IActionResult Create()
        {
            var m = new Models.Membership.Create();
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Membership.Create m)
        {
            var member = new Member
            {
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth,
                PhoneNumber = m.PhoneNumber,
                Email = m.Email
            };
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            var m = new Models.Membership.Detail
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                DateOfBirth = member.DateOfBirth,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email
            };
            return View(m);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            var m = new Models.Membership.Edit
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                DateOfBirth = member.DateOfBirth,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email
            };
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Models.Membership.Edit e)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var member = await _context.Members.FindAsync(e.Id);
                    if (member == null)
                    {
                        return NotFound();
                    }

                    member.Id = e.Id;
                    member.FirstName = e.FirstName;
                    member.LastName = e.LastName;
                    member.DateOfBirth = e.DateOfBirth;
                    member.PhoneNumber = e.PhoneNumber;
                    member.Email = e.Email;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    ModelState.AddModelError("Exception", "Error of edit");
                }

            }

            return View(e);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }
            var m = new Models.Membership.Delete
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                DateOfBirth = member.DateOfBirth,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email
            };
            return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Models.Membership.Delete d)
        {
            try
            {
                var member = await _context.Members.FirstOrDefaultAsync(m => m.Id == d.Id);
                if (member == null) 
                {
                    return NotFound(); 
                }
                _context.Members.Remove(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                ModelState.AddModelError("Exception", "something is wrong");
            }

            return View(d);
        }
    }
}
