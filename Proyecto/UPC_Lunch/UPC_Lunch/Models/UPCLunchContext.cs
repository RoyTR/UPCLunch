using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace UPC_Lunch.Models
{
    public class UPCLunchContext : DbContext
    {
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Plato> Platos { get; set; }

        public DbSet<RestauranteFavorito> RestaurantesFavoritos { get; set; }
    }
}