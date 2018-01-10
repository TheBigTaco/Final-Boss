using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FinalBoss.Models;

namespace FinalBoss.Migrations.TestDb
{
    [DbContext(typeof(TestDbContext))]
    partial class TestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FinalBoss.Models.Boss", b =>
                {
                    b.Property<int>("BossId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HeroId");

                    b.Property<bool>("ImmediateThreat");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Sex");

                    b.Property<string>("Species");

                    b.HasKey("BossId");

                    b.HasIndex("HeroId");

                    b.ToTable("bosses");
                });

            modelBuilder.Entity("FinalBoss.Models.Hero", b =>
                {
                    b.Property<int>("HeroId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<byte[]>("Picture");

                    b.Property<string>("Weapon");

                    b.HasKey("HeroId");

                    b.ToTable("heroes");
                });

            modelBuilder.Entity("FinalBoss.Models.Boss", b =>
                {
                    b.HasOne("FinalBoss.Models.Hero", "Hero")
                        .WithMany("Bosses")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
