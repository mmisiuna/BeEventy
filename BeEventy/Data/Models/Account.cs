using BeEventy.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostgreSQL.Data
{
    
    [Table("account")]
    public class Account
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("active_account")]
        public bool ActiveAccount { get; set; }
    }
}