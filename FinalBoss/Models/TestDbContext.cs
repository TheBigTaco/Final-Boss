using System;
using Microsoft.EntityFrameworkCore;

namespace FinalBoss.Models
{
    public class TestDbContext : FinalBossContext
	{
		public override DbSet<Boss> Bosses { get; set; }
        public override DbSet<Hero> Heroes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder
				.UseMySql(@"Server=localhost;Port=8889;database=finalbosses_test;uid=root;pwd=root;");
	}
}
