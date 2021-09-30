using System;
using System.Collections.Generic;
using System.Linq;

namespace lib
{
    public class Game
    {
        private Database _db;
        public List<Room> Rooms;
        public List<Monster> Monsters;
        public List<Treasure> Treasures;
        public int TurnLimit;
        public PlayerCharacter Player;
        public Minotaur Minotaur;
        public Game(Database database)
        {
            this._db = database;
            this.Rooms = new();
            this.Monsters = new();
            this.Treasures = new();
            this.TurnLimit = 25;
            this.Player = new();
            this.Minotaur = new();
        }


    }
}