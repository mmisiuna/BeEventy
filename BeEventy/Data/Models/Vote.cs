using BeEventy.Data.Models;
using PostgreSQL.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Vote
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public Account User { get; set; }

    [ForeignKey("Event")]
    public int EventId { get; set; }
    public Event Event { get; set; }

    public bool IsPlus { get; set; } // Nowe pole do śledzenia, czy głos jest plusem czy minusem
}
