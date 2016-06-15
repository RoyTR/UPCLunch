using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPC_Lunch.Models
{
    public class Plato
    {
        public int PlatoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        public bool Disponible { get; set; }

        public virtual Restaurante Restaurante { get; set; }
    }
}