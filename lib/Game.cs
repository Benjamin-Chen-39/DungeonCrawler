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
        public bool IsRunning;

        public Game(Database database, int startRoom)
        {
            this._db = database;
            this.Rooms = new();
            this.Monsters = new();
            this.Treasures = new();
            this.TurnLimit = 25;
            this.Player = new PlayerCharacter() { Health = 20, Attack = 5, Defense = 2 };
            this.Minotaur = new();
            this.CurrentRoomId = startRoom;
            this.CurrentRoom = new();
            this.CurrentMonster = new();
            this.CurrentTreasure = new();
            this.IsRunning = true;
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
            Console.Write($"\nYou are in room {this.CurrentRoomId}. ");
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

        //Move function
        public bool Move(string direction)
        {
            switch (direction)
            {
                case "north":
                    if (CurrentRoom.NorthRoomId != 0) { this.CurrentRoomId = CurrentRoom.NorthRoomId; return true; } else { Console.WriteLine("You walk into the wall."); }
                    break;
                case "south":
                    if (CurrentRoom.SouthRoomId != 0) { this.CurrentRoomId = CurrentRoom.SouthRoomId; return true; } else { Console.WriteLine("You walk into the wall."); }
                    break;
                case "east":
                    if (CurrentRoom.EastRoomId != 0) { this.CurrentRoomId = CurrentRoom.EastRoomId; return true; } else { Console.WriteLine("You walk into the wall."); }
                    break;
                case "west":
                    if (CurrentRoom.WestRoomId == 1)
                    {
                        Console.WriteLine("Congratulations! You found the exit!");
                        IsRunning = false;
                    }
                    if (CurrentRoom.WestRoomId != 0) { this.CurrentRoomId = CurrentRoom.WestRoomId; return true; } else { Console.WriteLine("You walk into the wall."); }
                    break;
                default:
                    break;
            }
            return false;
        }

        //Fight monster
        public void AttackMonster()
        {
            if (CurrentMonster.Id != 0 && CurrentMonster.IsDead == false)
            {    //player attacks current monster
                int damage = Player.Attack - CurrentMonster.Defense;
                if (damage < 0)
                    damage = 0;
                CurrentMonster.Health -= damage;
                Console.WriteLine($"You attack the monster, dealing {damage} damage!");

                if (CurrentMonster.Health <= 0)
                {
                    Console.WriteLine("You killed the monster and gained one attack point.");
                    Player.Attack += 1;
                    CurrentMonster.IsDead = true;
                }
            }
        }

        public void MonsterAttack()
        {
            //monster attacks player
            int damage = CurrentMonster.Attack - Player.Defense;
            if (damage < 0)
                damage = 0;
            Player.Health -= damage;
            Console.WriteLine($"{CurrentMonster.Name} attacks you, dealing {damage} damage.");
            if (Player.Health <= 0)
            {
                Console.WriteLine("The monster killed you! Game over.");
                IsRunning = false;
            }
            else
            {
                Console.WriteLine($"You have {Player.Health} health points.");
            }
        }

    }
}
