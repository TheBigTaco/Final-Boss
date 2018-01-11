using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using FinalBoss.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalBoss.ViewModels
{
    public class BossHeroes
    {

        private static EFHeroRepository heroRepo = new EFHeroRepository(); 

        public Boss Boss { get; set; }
        public SelectList Heroes { get; set; }
        
        public BossHeroes()
        {
            // not sure if necessary
            //Boss = new Boss();
            Heroes = new SelectList(heroRepo.Heroes, "HeroId", "Name");
   
        }
    }
}
