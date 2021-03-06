﻿using System;
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
    public class BossControllerTests : IDisposable
    {
        Mock<IBossRepository> mock = new Mock<IBossRepository>();
        EFBossRepository db = new EFBossRepository(new TestDbContext());
        public void Dispose()
        {
            db.DeleteAll();
        }
        private void DbSetup()
        {
            mock.Setup(e => e.Bosses).Returns(new Boss[]
            {
                new Boss {
                    BossId = 1,
                    Name = "Bowser",
                    Species = "Koopa King",
                    Sex = "Male",
                    Location = "Mushroom Kingdom",
                    ImmediateThreat = true,
                    HeroId = 1
                },
				new Boss {
                    BossId = 2,
                    Name = "Madame Broode",
                    Species = "Rabbit",
                    Sex = "Female",
                    Location = "Cascade Kingdom",
                    ImmediateThreat = false,
					HeroId = 1
				}
            }.AsQueryable());
        }

        [TestMethod]
        public void Mock_GetViewResultIndex_ActionResult()
        {
            DbSetup();
            BossController controller = new BossController(mock.Object);

            var result = controller.Index();

            Assert.IsInstanceOfType(result, typeof(ActionResult));
        }

        [TestMethod]
        public void Mock_IndexContainsModelData_List()
        {
            DbSetup();
            ViewResult indexView = new BossController(mock.Object).Index() as ViewResult;

            var result = indexView.ViewData.Model;

            Assert.IsInstanceOfType(result, typeof(List<Boss>));
        }

        [TestMethod]
        public void Mock_IndexModelContainsBosses_Collection()
        {
            DbSetup();
            BossController controller = new BossController(mock.Object);
            Boss testBoss = new Boss
            {
                BossId = 1,
                Name = "Bowser",
                Species = "Koopa King",
                Sex = "Male",
                Location = "Mushroom Kingdom",
                ImmediateThreat = true,
                HeroId = 1
            };

            ViewResult IndexView = controller.Index() as ViewResult;
            List<Boss> collection = IndexView.ViewData.Model as List<Boss>;

            CollectionAssert.Contains(collection, testBoss);
        }

        [TestMethod]
        public void Mock_PostViewResultCreate_ViewResult()
        {
			Boss testBoss = new Boss
			{
                BossId = 1,
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

            DbSetup();
            BossController controller = new BossController(mock.Object);

            var resultView = controller.Create(testBoss, null) as RedirectToActionResult;

            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DB_CreatesNewEntries_Collection()
        {
            BossController controller = new BossController(db); 
			Boss testBoss = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

            controller.Create(testBoss, null);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;

            CollectionAssert.Contains(collection, testBoss);
        }

        [TestMethod]
        public void Mock_GetDetails_ReturnsView()
        {
			Boss testBoss = new Boss
			{
                BossId = 1,
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

			DbSetup();
			BossController controller = new BossController(mock.Object);

            var resultView = controller.Details(testBoss.BossId) as ViewResult;
            var model = resultView.ViewData.Model as Boss;

			Assert.IsInstanceOfType(resultView, typeof(ViewResult));
			Assert.IsInstanceOfType(model, typeof(Boss));
        }

        [TestMethod]
        public void DB_DeleteSpecificEntries_Collection()
		{
			BossController controller = new BossController(db);
			Boss testBoss1 = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};
			Boss testBoss2 = new Boss
			{
				Name = "Madame Broode",
				Species = "Rabbit",
				Sex = "Female",
				Location = "Cascade Kingdom",
				ImmediateThreat = false,
				HeroId = 1
			};

			controller.Create(testBoss1, null);
            controller.Create(testBoss2, null);
            var collection = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;
            controller.DeleteConfirmed(collection[0].BossId);
			var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;

            CollectionAssert.DoesNotContain(collection2, testBoss1);
		}

		[TestMethod]
		public void DB_EditSpecificEntries_Boss()
		{
			BossController controller = new BossController(db);
			Boss testBoss1 = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

			controller.Create(testBoss1, null);

			var collection = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;
            Boss bossToEdit = (controller.Edit(collection[0].BossId) as ViewResult).ViewData.Model as Boss;
            bossToEdit.Name = "New Bowser";
            controller.Edit(bossToEdit);
			var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;

            Assert.AreEqual("New Bowser", collection2[0].Name);
		}

        [TestMethod]
        public void DB_FilterEntriesByThreat_Collection()
        {
            BossController controller = new BossController(db);

			Boss testBoss1 = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};
			Boss testBoss2 = new Boss
			{
				Name = "Madame Broode",
				Species = "Rabbit",
				Sex = "Female",
				Location = "Cascade Kingdom",
				ImmediateThreat = false,
				HeroId = 1
			};

            var isThreat = new List<Boss> { testBoss1 };

			controller.Create(testBoss1, null);
			controller.Create(testBoss2, null);
            var collection = (controller.IndexThreat(true) as ViewResult).ViewData.Model as List<Boss>;
            CollectionAssert.AreEqual(isThreat, collection);

        }

        // this a weird pointer reference or something. 
        [TestMethod]
        public void DB_ChangeSpecificEntryThreat()
        {
			BossController controller = new BossController(db);
			Boss testBoss1 = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

			controller.Create(testBoss1, null);
			var collection = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;
            controller.ToggleThreat(collection[0].BossId);
            var collection2 = (controller.Index() as ViewResult).ViewData.Model as List<Boss>;


            Assert.AreNotEqual(true, collection2[0].ImmediateThreat);

        }
    }
}
