using System;
using System.Collections.Generic;
using System.Linq;

namespace lib
{
    public class Game
    {
        private Database _db;
        public List<Room> Rooms { get; set; }
        public List<Monster> Monsters { get; set; }
        public List<Treasure> Treasures { get; set; }
        public int TurnLimit;
        public PlayerCharacter Player;
        public Minotaur Minotaur;
        public int CurrentRoomId;
        public Room CurrentRoom;
        public Monster CurrentMonster;
        public Treasure CurrentTreasure;

        public Game(Database database, int startRoom)
        {
            this._db = database;
            this.Rooms = new();
            this.Monsters = new();
            this.Treasures = new();
            this.TurnLimit = 25;
            this.Player = new();
            this.Minotaur = new();
            this.CurrentRoomId = startRoom;
        }

        public void AddRoom(int Id, int NorthRoomId, int SouthRoomId, int EastRoomId, int WestRoomId, bool isEscapeRoom, int MonsterId, int TreasureId)
        {
            Rooms.Add(new Room { Id = Id, NorthRoomId = NorthRoomId, SouthRoomId = SouthRoomId, EastRoomId = EastRoomId, WestRoomId = WestRoomId, isEscapeRoom = isEscapeRoom, MonsterId = MonsterId, TreasureId = TreasureId });
            if (MonsterId != 0)
                Monsters.Add(new Monster { Id = MonsterId, Health = 10, Attack = 3, Defense = 1, Name = "Goblin" });
            if (TreasureId != 0)
                Treasures.Add(new Treasure { Id = TreasureId, BonusStat = 2 });
        }
        //list of actions
        public void viewRoom()
        {
            CurrentRoom = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First();
            Console.WriteLine($"You are in room {this.CurrentRoomId}");
            int roomMonster = CurrentRoom.MonsterId;
            if (roomMonster != 0)
            {
                CurrentMonster = _db.Monsters.Where(monster => monster.Id == roomMonster).First();
                Console.WriteLine($"You see a {CurrentMonster.Name}");
            }
            int roomTreasure = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First().TreasureId;
            if (roomTreasure != 0)
            {
                CurrentTreasure = _db.Treasures.Where(treasure => treasure.Id == roomTreasure).First();
                Console.WriteLine("You see a treasure");
            }
            CurrentRoom = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First();


        }
        // //move rooms
        // public void MoveRoom()

        //fight
        //sneak
        //acquire treasure

    }
}
