using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBoss.Models
{
    public class EFBossRepository : IBossRepository
    {
        FinalBossContext db = new FinalBossContext();

        public IQueryable<Boss> Bosses
        { get { return db.Bosses; } }

        public Boss Save(Boss boss)
        {
            db.Bosses.Add(boss);
            db.SaveChanges();
            return boss;
        }

        public Boss Edit(Boss boss)
        {
            db.Entry(boss).State = EntityState.Modified;
            db.SaveChanges();
            return boss;
        }

        public void Remove(Boss boss)
        {
            db.Bosses.Remove(boss);
            db.SaveChanges();
        }
    }
}
