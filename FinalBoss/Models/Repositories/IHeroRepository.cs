using System;
using System.Linq;
using System.Collections.Generic;

namespace FinalBoss.Models
{
    public interface IHeroRepository
    {
        IQueryable<Hero> Heroes { get; }
        Hero Save(Hero hero);
        Hero Edit(Hero hero);
        void Remove(Hero hero);
    }
}
