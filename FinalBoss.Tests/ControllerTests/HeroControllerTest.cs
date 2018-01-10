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
    public class HeroControllerTest
    {
        Mock<IHeroRepository> mock = new Mock<IHeroRepository>();
        EFHeroRepository db = new EFHeroRepository(new TestDbContext());

        public void Dispose()
        {
            db.DeleteAll();
        }

        private void DbSetup()
        {
            mock.Setup(e => e.Heroes).Returns()
        }
    }
}
