using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalBoss.Models
{
    [Table("heroes")]
    public class Hero
    {
        [Key]
        public int HeroId { get; set; }
        public string Name { get; set; }
        public string Weapon { get; set; }
        public virtual ICollection<Boss> Bosses { get; set; }

		public override bool Equals(System.Object otherHero)
		{
			if (!(otherHero is Hero))
			{
				return false;
			}
			else
			{
				Hero newHero = (Hero)otherHero;
				return this.HeroId.Equals(newHero.HeroId);
			}
		}

		public override int GetHashCode()
		{
			return this.HeroId.GetHashCode();
		}
    }
}
