using System;
using Microsoft.EntityFrameworkCore;

namespace FinalBoss.Models
{
    public class FinalBossContext : DbContext
    {
		public virtual DbSet<Boss> Bosses { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
				.UseMySql(@"Server=localhost;Port=8889;database=finalbosses;uid=root;pwd=root;");
    }
}
