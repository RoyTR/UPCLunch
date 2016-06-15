using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPC_Lunch.Models
{
    public class RestauranteFavorito
    {
        public int RestauranteFavoritoId { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public int RestauranteId { get; set; }
        public virtual Restaurante Restaurante { get; set; }
    }
}