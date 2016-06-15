using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UPC_Lunch.Models
{
    public class Restaurante
    {
        public int RestauranteId { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public string RUC { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Latitud { get; set; }
        [Required]
        public string Longitud { get; set; }
        [Required]
        public int AforoCompleto { get; set; }
        [Required]
        public string Estado { get; set; }
        public bool MesaDisponible { get; set; }

        public virtual List<Plato> Platos { get; set; }
    }
}