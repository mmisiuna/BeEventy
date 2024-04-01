using BeEventy.Data.Enums;
using Microsoft.AspNetCore.Authentication;

namespace BeEventy.Data.Models
{
    public class MusicEvent : Event
    {
        public int Id { get; set; }
        public MusicType Type { get; set; }
        public string Artist { get; set; }
    }
}