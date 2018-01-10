using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalBoss.Models;


namespace FinalBoss.Tests.ModelTests
{
    [TestClass]
    public class HeroTests
    {
        [TestMethod]
        public void Equals_HeroesAreTheSame_True()
        {
            Hero hero = new Hero
            {
                Name = "Mario",
                Weapon = "Fire_Flower"
            };

            Hero heroCopy = hero;

            Assert.AreEqual(heroCopy, hero);
        }
    }
}
