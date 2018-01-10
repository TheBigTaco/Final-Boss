using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FinalBoss.Models;

namespace FinalBoss.Migrations
{
    [DbContext(typeof(FinalBossContext))]
    [Migration("20180110165558_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FinalBoss.Models.Boss", b =>
                {
                    b.Property<int>("BossId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HeroId");

                    b.Property<int?>("HeroId1");

                    b.Property<bool>("ImmediateThreat");

                    b.Property<string>("Location");

                    b.Property<string>("Name");

                    b.Property<string>("Sex");

                    b.Property<string>("Species");

                    b.HasKey("BossId");

                    b.HasIndex("HeroId1");

                    b.ToTable("bosses");
                });

            modelBuilder.Entity("FinalBoss.Models.Hero", b =>
                {
                    b.Property<int>("HeroId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Weapon");

                    b.HasKey("HeroId");

                    b.ToTable("heroes");
                });

            modelBuilder.Entity("FinalBoss.Models.Boss", b =>
                {
                    b.HasOne("FinalBoss.Models.Hero", "Hero")
                        .WithMany()
                        .HasForeignKey("HeroId1");
                });
        }
    }
}
