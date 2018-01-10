﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FinalBoss.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Boss boss)
        {
            bossRepo.Save(boss);

            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Boss thisBoss = bossRepo.Bosses.FirstOrDefault(x => x.BossId == id);
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
    }
}
