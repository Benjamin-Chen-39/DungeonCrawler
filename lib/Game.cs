using System;
using System.Collections.Generic;
using System.Linq;

namespace lib
{
    public class Game
    {
        private Database _db;
        public List<Room> Rooms {get; set;}
        public List<Monster> Monsters {get; set;}
        public List<Treasure> Treasures{get; set;}
        public int TurnLimit;
        public PlayerCharacter Player;
        public Minotaur Minotaur;
        public int CurrentRoom;

        public Game(Database database, int startRoom)
        {
            this._db = database;
            this.Rooms = new();
            this.Monsters = new();
            this.Treasures = new();
            this.TurnLimit = 25;
            this.Player = new();
            this.Minotaur = new();
            this.CurrentRoom = startRoom;
        }


        //list of actions
        public void viewRoom()
        {
            Console.WriteLine($"You are in room {this.CurrentRoom}");
        }
        //move rooms
        //fight
        //sneak
        //acquire treasure

        public void AddRoom(int Id, int NorthRoomId, int SouthRoomId, int EastRoomId, int WestRoomId, bool isEscapeRoom, int MonsterId, int TreasureId)
        {
            Rooms.Add(new Room { Id = Id, NorthRoomId = NorthRoomId, SouthRoomId = SouthRoomId, EastRoomId = EastRoomId, WestRoomId = WestRoomId, isEscapeRoom = isEscapeRoom, MonsterId = MonsterId, TreasureId = TreasureId });
            if (MonsterId != 0)
                Monsters.Add(new Monster { Id = MonsterId, Health = 10, Attack = 3, Defense = 1, Name = "Goblin" });
            if (TreasureId != 0)
                Treasures.Add(new Treasure { Id = TreasureId, BonusStat = 2 });
        }
    }
}