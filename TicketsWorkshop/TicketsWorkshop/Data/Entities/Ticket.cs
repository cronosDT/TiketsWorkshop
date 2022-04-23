﻿using System.ComponentModel.DataAnnotations;

namespace TicketsWorkshop.Data.Entities
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Estado del ticket")]
        public bool? WasUsed { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Name { get; set; }

        [Display(Name = "Documento")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        public string? Document { get; set; }

        [Display(Name = "Entrada")]
        public Entrance? Entrance { get; set; }

        [Display(Name = "Fecha y hora")]
        public DateTime? DateTime { get; set; }

        public ICollection<TicketEntrance> TicketEntrances { get; set; }

        [Display(Name = "Entradas")]
        public int EntrancesNumber => TicketEntrances == null ? 0 : TicketEntrances.Count;
    }
}
