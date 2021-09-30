using System;
using System.ComponentModel.DataAnnotations;

namespace lib
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public int NorthRoomId { get; set; }
        public int SouthRoomId { get; set; }
        public int EastRoomId { get; set; }
        public int WestRoomId { get; set; }
        public bool isEscapeRoom { get; set; }
        public int MonsterId { get; set; }
        public int TreasureId { get; set; }
    }
}