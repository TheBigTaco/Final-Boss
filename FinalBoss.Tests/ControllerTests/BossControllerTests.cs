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
    public class BossControllerTests
    {
        Mock<IBossRepository> mock = new Mock<IBossRepository>();
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

            var resultView = controller.Create(testBoss) as RedirectToActionResult;

            Assert.IsInstanceOfType(resultView, typeof(RedirectToActionResult));
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
        public void Mock_DeleteFromMockDatabase_Collection()
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

            IActionResult DeleteConfirmed = controller.DeleteConfirmed(testBoss.BossId) as IActionResult;
            ViewResult IndexView = controller.Index() as ViewResult;
            List<Boss> collection = IndexView.ViewData.Model as List<Boss>;
            foreach(Boss boss in collection) 
            {
                Console.WriteLine("#####"+boss.Name);
            }

            CollectionAssert.DoesNotContain(collection, testBoss);
        }
    }
}
