using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalBoss.Models
{
    public interface IBossRepository
    {
        IQueryable<Boss> Bosses { get; }
        Boss Save(Boss boss);
        Boss Edit(Boss boss);
        void Remove(Boss boss);
    }
}
