using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalBoss.Models;

namespace FinalBoss.Tests.ModelTests
{
    [TestClass]
    public class BossTests
    {
        [TestMethod]
        public void GetEverything_ReturnsBossProperties_Strings()
        {
            Boss boss = new Boss
            {
                Name = "Bowser",
                Species = "Koopa King",
                Sex = "Male",
                Location = "Mushroom Kingdom",
                ImmediateThreat = true,
                HeroId = 1
            };

            Assert.AreEqual("Bowser", boss.Name);
            Assert.AreEqual("Koopa King", boss.Species);
            Assert.AreEqual("Male", boss.Sex);
            Assert.AreEqual("Mushroom Kingdom", boss.Location);
            Assert.AreEqual(true, boss.ImmediateThreat);
            Assert.AreEqual(1, boss.HeroId);
        }

		[TestMethod]
		public void Equals_BossesAreTheSame_True()
		{
			Boss boss = new Boss
			{
				Name = "Bowser",
				Species = "Koopa King",
				Sex = "Male",
				Location = "Mushroom Kingdom",
				ImmediateThreat = true,
				HeroId = 1
			};

			Boss bossCopy = boss;

			Assert.AreEqual(bossCopy, boss);
		}
    }
}
