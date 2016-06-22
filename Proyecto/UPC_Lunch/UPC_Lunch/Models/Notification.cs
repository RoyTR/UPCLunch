using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UPC_Lunch.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool Seen { get; set; }
        public DateTime Date { get; set; }

        public int RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}