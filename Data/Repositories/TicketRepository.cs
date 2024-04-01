using BeEventy.Data.Models;
using PostgreSQL.Data;

namespace BeEventy.Data.Repositories
{
    public class TicketRepository
    {
        private readonly List<Ticket> _tickets;

        public TicketRepository()
        {
            _tickets = new List<Ticket>();
        }
    }
}
