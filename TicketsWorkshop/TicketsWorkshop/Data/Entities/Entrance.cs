﻿using System.ComponentModel.DataAnnotations;

namespace TicketsWorkshop.Data.Entities
{
    public class Entrance
    {
        public int Id { get; set; }


        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string Description { get; set; }

        public ICollection<TicketEntrance> TicketEntrances { get; set; }


    }
}
