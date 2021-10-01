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
            Console.Write($"You are in room {this.CurrentRoomId}. ");
            int roomMonster = CurrentRoom.MonsterId;
            if (roomMonster != 0)
            {
                CurrentMonster = _db.Monsters.Where(monster => monster.Id == roomMonster).First();
                Console.Write($"You see a {CurrentMonster.Name}. ");
            }
            int roomTreasure = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First().TreasureId;
            if (roomTreasure != 0)
            {
                CurrentTreasure = _db.Treasures.Where(treasure => treasure.Id == roomTreasure).First();
                Console.Write("You see a treasure.");
            }

            Console.WriteLine();

            CurrentRoom = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First();

            if (CurrentRoom.NorthRoomId != 0)
            {
                Console.WriteLine("There is a room to the north.");
            }
            if (CurrentRoom.SouthRoomId != 0)
            {
                Console.WriteLine("There is a room to the south.");
            }
            if (CurrentRoom.WestRoomId != 0)
            {
                Console.WriteLine("There is a room to the west.");
            }
            if (CurrentRoom.EastRoomId != 0)
            {
                Console.WriteLine("There is a room to the east.");
            }
        }
        // //move rooms
        // public void MoveRoom()

        //fight
        //sneak
        //acquire treasure

        public bool Move(string direction)
        {
            CurrentRoom = _db.Rooms.Where(room => room.Id == this.CurrentRoomId).First();

            switch (direction)
            {
                case "north":
                    if (CurrentRoom.NorthRoomId != 0) { this.CurrentRoomId = CurrentRoom.NorthRoomId; return true; } else { Console.WriteLine("You can't move in this direction."); }
                    break;
                case "south":
                    if (CurrentRoom.SouthRoomId != 0) { this.CurrentRoomId = CurrentRoom.SouthRoomId; return true; } else { Console.WriteLine("You can't move in this direction."); }
                    break;
                case "east":
                    if (CurrentRoom.EastRoomId != 0) { this.CurrentRoomId = CurrentRoom.EastRoomId; return true; } else { Console.WriteLine("You can't move in this direction."); }
                    break;
                case "west":
                    if (CurrentRoom.WestRoomId != 0) { this.CurrentRoomId = CurrentRoom.WestRoomId; return true; } else { Console.WriteLine("You can't move in this direction."); }
                    break;
                default:
                    break;
            }

            return false;
        }
    }
}
