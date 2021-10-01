using System;
using System.ComponentModel.DataAnnotations;

namespace lib
{
    public class Treasure
    {
        [Key]
        public int Id { get; set; }
        public int BonusStat { get; set; }
        public bool IsOpened { get; set; }

        public Treasure()
        {
            IsOpened = false;
        }
    }
}