using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlickrClone.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FlickrClone.Controllers
{
    //the [Authorize] attribute on UserController allows access to the controller only if a user is logged in
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            return View(_db.Pictures.Where(x => x.User.Id == currentUser.Id));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Picture picture)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);
            picture.User = currentUser;
            _db.Pictures.Add(picture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.Id == id);
            return View(thisPicture);
        }

        [HttpPost]
        public IActionResult Edit(Picture picture)
        {
            _db.Entry(picture).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.Id == id);
            return View(thisPicture);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPicture = _db.Pictures.FirstOrDefault(pictures => pictures.Id == id);
            _db.Pictures.Remove(thisPicture);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}