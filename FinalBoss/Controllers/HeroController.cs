using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalBoss.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FinalBoss.Controllers
{
    public class HeroController : Controller
    {
        private IHeroRepository heroRepo;
        public HeroController(IHeroRepository repo = null)
        {
            if(repo == null)
            {
                this.heroRepo = new EFHeroRepository();
            }
            else
            {
                this.heroRepo = repo;
            }
        }

        public IActionResult Index(int? id = null, string name = null)
        {
			if (id != null && name != null)
			{
				ViewBag.ModalId = id;
				ViewBag.ModalName = name;

			}
			else
			{
				ViewBag.ModalId = null;
			}
            return View(heroRepo.Heroes.ToList());
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hero hero, IFormFile image)
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
                hero.Picture = newImage;
            }
            else
            {
                Console.WriteLine("No Image");
            }

            heroRepo.Save(hero);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Hero thisHero = heroRepo.Heroes.FirstOrDefault(x => x.HeroId == id);
            return View(thisHero);
        }

        [HttpPost]
        public IActionResult Edit(Hero hero)
        {
            heroRepo.Edit(hero);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            Hero thisHero = heroRepo.Heroes.Include(hero => hero.Bosses).FirstOrDefault(x => x.HeroId == id);
            return View(thisHero);
        }

        public ActionResult Delete(int id)
        {
            Hero thisHero = heroRepo.Heroes.FirstOrDefault(x => x.HeroId == id);
            return View(thisHero);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Hero thisHero = heroRepo.Heroes.FirstOrDefault(x => x.HeroId == id);
            heroRepo.Remove(thisHero);
            return RedirectToAction("Index");
        }

		public IActionResult GetPicture(int id)
		{
			var thisPicture = heroRepo.Heroes.FirstOrDefault(x => x.HeroId == id).Picture;
			return File(thisPicture, "image/png");
		}
    }
}
