namespace TicketsWorkshop.Data.Entities
{
    public class TicketEntrance
    {
        public int Id { get; set; }

        public Ticket Ticket { get; set; }

        public Entrance Entrance { get; set; }

    }
}
