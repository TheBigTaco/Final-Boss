using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalBoss.Models;
using FinalBoss.ViewModels;
using System.IO;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FinalBoss.Controllers
{
    public class BossController : Controller
    {
        private IBossRepository bossRepo;
        public BossController(IBossRepository repo = null)
        {
            if(repo == null)
            {
                this.bossRepo = new EFBossRepository();
            }
            else
            {
                this.bossRepo = repo;    
            }
        }

        public IActionResult Index()
        {
            return View(bossRepo.Bosses.ToList());
        }

        // filter by immediate threat
        public IActionResult IndexThreat(bool threat)
        {
            
            return View(bossRepo.Bosses.Where(t => t.ImmediateThreat == threat).ToList());
        }

        public ViewResult Create()
        {
            var viewModel = new BossHeroes();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(Boss boss, IFormFile image)
        {
            byte[] newImage = new byte[0];
            if(image != null) 
            {
                using (Stream fileStream = image.OpenReadStream())
                using (MemoryStream ms = new MemoryStream())
                { 
                    fileStream.CopyTo(ms);
                    newImage = ms.ToArray();
                }
                boss.Picture = newImage;
            }
            else
            {
                Console.WriteLine("No Image");    
            }

            bossRepo.Save(boss);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Boss thisBoss = bossRepo.Bosses.Include(boss => boss.Hero).FirstOrDefault(x => x.BossId == id);
            return View(thisBoss);
        }

        public IActionResult Edit(int id)
        {
            Boss thisBoss = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id);
            return View(thisBoss);
        }

        [HttpPost]
        public IActionResult Edit(Boss boss)
        {
            bossRepo.Edit(boss);

            return RedirectToAction("Index");
        }

		public ActionResult Delete(int id)
		{
			Boss thisBoss = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id);
			return View(thisBoss);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
            Boss thisBoss = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id);
			bossRepo.Remove(thisBoss);   // Updated!
										 // Removed db.SaveChanges() call
			return RedirectToAction("Index");
		}

        // Toggles immediate threat level.
        [HttpPost]
        public IActionResult ToggleThreat(int id)
        {
            Boss thisBoss = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id);
            thisBoss.ImmediateThreat = !thisBoss.ImmediateThreat;
            bossRepo.Edit(thisBoss);
            return RedirectToAction("Index"); 
        }

        public IActionResult GetPicture(int id)
        {
            var thisPicture = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id).Picture;
            return File(thisPicture, "image/png");
        }
    }
}
