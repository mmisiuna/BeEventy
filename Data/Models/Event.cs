using Microsoft.AspNetCore.Authentication;
using PostgreSQL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeEventy.Data.Models
{
    [Table("event")]
    public class Event
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("date_of_start")]
        public DateTime DateOfStart { get; set; }
        [Column("date_of_end")]
        public DateTime DateOfEnd { get; set; }
        [Column("date_of_upload")]
        public DateTime DateOfUpload { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("prize")]
        public decimal Prize { get; set; }
        [Column("is_online")]
        public bool IsOnline { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("image")]
        public string Image { get; set; }
        [Column("author_id")] 
        public int AuthorId { get; set; }
        [Column("pluses")]
        public int Pluses { get; set; }
        [Column("minuses")]
        public int Minuses { get; set; }
        [Column("amount_of_all_tickets")]
        public int AmountOfAllTickets { get; set; }
        [Column("amount_of_batches")]
        public int AmmountOfBatches { get; set; }
        [ForeignKey("AuthorId")]
        public Account Author { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
