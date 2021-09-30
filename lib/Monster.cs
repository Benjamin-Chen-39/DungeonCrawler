using System;
using System.ComponentModel.DataAnnotations;

namespace lib
{
    public class Monster
    {
        [Key]
        public int Id { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public string Name { get; set; }

    }
}