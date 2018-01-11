using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalBoss.Models;
using FinalBoss.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinalBoss.Tests.ControllerTests
{
    [TestClass]
    public class HeroControllerTest : IDisposable
    {
        Mock<IHeroRepository> mock = new Mock<IHeroRepository>();
        EFHeroRepository db = new EFHeroRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
        }

        private void DbSetup()
        {
            mock.Setup(e => e.Heroes).Returns(new Hero[]
            {
                new Hero {
                    HeroId = 2,
                    Name = "Mario",
                    Weapon = "Fire_Flower"
                },
                new Hero {
                    HeroId = 3,
                    Name = "Luigi",
                    Weapon = "Vacuum"
                }
            }.AsQueryable());
        }

		[TestMethod]
		public void Mock_GetViewResultIndex_ActionResult()
		{
			DbSetup();
            HeroController controller = new HeroController(mock.Object);
            var result = controller.Index();


		    Assert.IsInstanceOfType(result, typeof(ActionResult));
		}

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            DbSetup();
            ViewResult indexView = new HeroController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Hero>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsHeroes_Collection()
        {
            DbSetup();
            HeroController controller = new HeroController(mock.Object);

            Hero testHero = new Hero
            {
                HeroId = 2,
                Name = "Mario",
                Weapon = "Fire_Flower"
            };

            ViewResult IndexView = controller.Index() as ViewResult;
            List<Hero> collection = IndexView.ViewData.Model as List<Hero>;

            CollectionAssert.Contains(collection, testHero);
        }

        // tests the view that is returned. kinda weird.
        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
			Hero testHero = new Hero
			{
				HeroId = 2,
				Name = "Mario",
				Weapon = "Fire_Flower"
			};

            DbSetup();
            HeroController controller = new HeroController(mock.Object);

            var resultView = controller.Create(testHero, null) as RedirectToActionResult;

            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            HeroController controller = new HeroController(db);
            Hero testHero = new Hero
            {
                Name = "Mario",
                Weapon = "Fire_Flower"
            };

            controller.Create(testHero, null);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Hero>;

            CollectionAssert.Contains(collection, testHero);
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
            Hero testHero = new Hero
            {
                HeroId = 2,
                Name = "Mario",
                Weapon = "Fire_Flower"
            };

            DbSetup();
            HeroController controller = new HeroController(mock.Object);

            var resultView = controller.Details(testHero.HeroId) as ViewResult;
            var model = resultView.ViewData.Model as Hero;

            Assert.IsInstanceOfType(resultView, typeof(ViewResult));
            Assert.IsInstanceOfType(model, typeof(Hero));
        }

        [TestMethod]
        public void DB_DeleteSpecificHero_Collection()
        {
            HeroController controller = new HeroController(db);
            Hero testHero1 = new Hero
            {
                Name = "Luigi",
                Weapon = "vacuum"
            };
            Hero testHero2 = new Hero
            {
                Name = "Peach",
                Weapon = "Toad"
            };

            controller.Create(testHero1, null);
            controller.Create(testHero2, null);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Hero>;
            controller.DeleteConfirmed(collection[1].HeroId);
            var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Hero>;

            CollectionAssert.DoesNotContain(collection2, testHero1);
        }

        [TestMethod]
        public void DB_EditSpecificHero_Hero()
        {
            HeroController controller = new HeroController(db);
            Hero testHero1 = new Hero
            {
                Name = "Mario",
                Weapon = "Yoshi"
            };

            controller.Create(testHero1, null);

            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Hero>;
            Hero heroToEdit = (controller.Edit(collection[0].HeroId) as ViewResult).ViewData.Model as Hero;
            heroToEdit.Name = "New Mario";
            controller.Edit(heroToEdit);
            var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Hero>;

            Assert.AreEqual("New Mario", collection2[0].Name);
        }
    }


}
