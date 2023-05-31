using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pokedex.Models;

    public class MvcPokeContext : DbContext
    {
        public MvcPokeContext (DbContextOptions<MvcPokeContext> options)
            : base(options)
        {
        }

        public DbSet<Pokedex.Models.PokeModel> PokeModel { get; set; } = default!;
    }
