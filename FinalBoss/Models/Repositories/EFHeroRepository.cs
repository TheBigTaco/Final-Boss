using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinalBoss.Models
{
    public class EFHeroRepository : IHeroRepository
    {
        FinalBossContext db;
        public EFHeroRepository()
        {
            db = new FinalBossContext();
        }

        public EFHeroRepository(FinalBossContext thisDb)
        {
            db = thisDb;
        }
        public IQueryable<Hero> Heroes
        { get { return db.Heroes; } }

        public Hero Save(Hero hero)
        {
            db.Heroes.Add(hero);
            db.SaveChanges();
            return hero;
        }

        public Hero Edit(Hero hero)
        {
            db.Entry(hero).State = EntityState.Modified;
            db.SaveChanges();
            return hero;
        }

        public void Remove(Hero hero)
        {
            db.Heroes.Remove(hero);
            db.SaveChanges();
        }

		public void DeleteAll()
		{
			db.Database.ExecuteSqlCommand("delete from heroes where HeroId != 1");
		}
        
    }
}
