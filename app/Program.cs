using System;
using lib;

namespace app
{
    class Program
    {
        static void Main(string[] args)
        {
            //create and intialize database
            using var db = new Database();

            var game = new Game(db, 16);

            game.AddRoom(1, 0, 0, 2, 0, true, 0, 0);
            game.AddRoom(2, 0, 0, 3, 1, false, 1, 0);
            game.AddRoom(3, 0, 7, 4, 2, false, 2, 0);
            game.AddRoom(4, 0, 8, 0, 3, false, 0, 0);
            game.AddRoom(5, 0, 9, 0, 0, false, 3, 1);
            game.AddRoom(6, 0, 10, 7, 0, false, 0, 2);
            game.AddRoom(7, 3, 11, 8, 6, false, 0, 0);
            game.AddRoom(8, 4, 0, 0, 7, false, 4, 0);
            game.AddRoom(9, 5, 13, 10, 0, false, 0, 0);
            game.AddRoom(10, 6, 0, 11, 9, false, 0, 0);
            game.AddRoom(11, 7, 0, 0, 10, false, 0, 3);
            game.AddRoom(12, 0, 16, 0, 0, false, 0, 4);
            game.AddRoom(13, 9, 0, 14, 0, false, 0, 5);
            game.AddRoom(14, 0, 0, 15, 13, false, 0, 0);
            game.AddRoom(15, 0, 0, 16, 14, false, 5, 0);
            game.AddRoom(16, 12, 0, 0, 15, false, 0, 0);

            foreach (Room room in game.Rooms)
            {
                db.Rooms.Add(room);
            }

            foreach (Monster monster in game.Monsters)
            {
                db.Monsters.Add(monster);
            }

            foreach (Treasure treasure in game.Treasures)
            {
                db.Treasures.Add(treasure);
            }

            db.SaveChanges();

            bool validRoomMove = true;

            Console.WriteLine($"Starting stats:\nHealth: {game.Player.Health}\nAttack: {game.Player.Attack}\nDefense: {game.Player.Defense}");
            Console.WriteLine("Valid commands: North/South/East/West for movement\nAttack/Open for attacking a monster or opening a treasure");
            Console.WriteLine("Look to check adjacent rooms\nRest to spend a turn and regain 3 health\nQuit for exiting");
            Console.WriteLine("Rules: Find the exit before the Minotaur catches you, or battle the fat cow to the death.");

            while (game.IsRunning)
            {
                //Beginning of a turn
                if (game.CurrentRoom.isEscapeRoom)
                {
                    Console.WriteLine("You found the exit");
                    break;
                }
                if (validRoomMove)
                {
                    game.viewRoom();
                    validRoomMove = false;
                }

                Console.WriteLine($"You have {game.TurnLimit} turns left.");
                Console.WriteLine("Please enter a command.");
                Console.Write("> ");

                var input = Console.ReadLine().ToLower();
                Console.WriteLine();

                switch (input)
                {
                    case "quit":
                        game.IsRunning = false;
                        break;
                    case "north":
                        validRoomMove = game.Move(input);
                        break;
                    case "south":
                        validRoomMove = game.Move(input);
                        break;
                    case "east":
                        validRoomMove = game.Move(input);
                        break;
                    case "west":
                        validRoomMove = game.Move(input);
                        break;
                    case "attack":
                        game.AttackMonster();
                        break;
                    case "open":
                        game.OpenTreasure();
                        break;
                    case "look":
                        game.Look();
                        break;
                    case "rest":
                        game.Rest();
                        break;
                    default:
                        break;
                }

                if (!game.IsRunning)
                    break;

                //if there's a live monster, it will attack you
                if (game.CurrentMonster.Id != 0 && !game.CurrentMonster.IsDead)
                    game.MonsterAttack();

                //end of turn, database of monsters/treasures should get updated
                db.SaveChanges();
                game.TurnLimit -= 1;

                //Trigger minotaur fight if you didn't find the exit
                if (game.TurnLimit == 0)
                {
                    Console.WriteLine("The minotaur catches up to you and teleports you both to an arena. The exit lies behind him. Kill him and win your freedom!");
                    Console.WriteLine("Here are your current stats: ");
                    Console.WriteLine($"Health:{game.Player.Health}\nAttack: {game.Player.Attack}\nDefense: {game.Player.Defense}");
                    game.FightMinotaur();
                }
            }
        }
    }
}
