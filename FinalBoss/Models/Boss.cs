using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoss.Models
{
    [Table("bosses")]
    public class Boss
    {
        [Key]
        public int BossId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }
        public string Location { get; set; }
        public bool ImmediateThreat { get; set; }
        public byte[] Picture { get; set; }
        public int HeroId { get; set; }
        public virtual Hero Hero { get; set; }

        public override bool Equals(System.Object otherBoss)
        {
            if(!(otherBoss is Boss))
            {
                return false;
            }
            else
            {
                Boss newBoss = (Boss)otherBoss;
                return this.BossId.Equals(newBoss.BossId);
            }
        }

        public override int GetHashCode()
        {
            return this.BossId.GetHashCode();
        }
    }
}
