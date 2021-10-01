using System;
using Xunit;
using FluentAssertions;
using lib;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace test
{
    public class DatabaseTest
    {
        private DbContextOptions<Database> _options;
        private Game _testGame;

        public DatabaseTest()
        {
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();

            _options = new DbContextOptionsBuilder<Database>().UseSqlite(conn).Options;
        }

        [Fact]
        public void ShouldCreateRoom()
        {
            using var ctx = new Database(_options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var newRoom = new Room { Id = 1, NorthRoomId = 0, SouthRoomId = 0, EastRoomId = 2, WestRoomId = 0, isEscapeRoom = true, MonsterId = 0, TreasureId = 0 };

            ctx.Add(newRoom);
            ctx.SaveChanges();

            ctx.Rooms.Count().Should().Be(1);
        }

        [Fact]
        public void ShouldCreateMonster()
        {
            using var ctx = new Database(_options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var newMonster = new Monster { Id = 1, Health = 10, Attack = 3, Defense = 1, Name = "Goblin" };

            ctx.Add(newMonster);
            ctx.SaveChanges();

            ctx.Monsters.Count().Should().Be(1);
        }

        [Fact]
        public void ShouldCreateTreasure()
        {
            using var ctx = new Database(_options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var newTreasure = new Treasure { Id = 1, BonusStat = 2 };

            ctx.Add(newTreasure);
            ctx.SaveChanges();

            ctx.Treasures.Count().Should().Be(1);
        }

        [Fact]
        public void ConnectGameToDatabaseAndStartGame()
        {
            using var ctx = new Database(_options);

            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            _testGame = new Game(ctx, 16);
            _testGame.AddRoom(1, 0, 0, 2, 0, true, 0, 0);
            _testGame.AddRoom(2, 0, 0, 3, 1, false, 1, 0);
            _testGame.AddRoom(3, 0, 7, 4, 2, false, 2, 0);
            _testGame.AddRoom(4, 0, 8, 0, 3, false, 0, 0);
            _testGame.AddRoom(5, 0, 9, 0, 0, false, 3, 1);
            _testGame.AddRoom(6, 0, 10, 7, 0, false, 0, 2);
            _testGame.AddRoom(7, 3, 11, 8, 6, false, 0, 0);
            _testGame.AddRoom(8, 4, 0, 0, 7, false, 4, 0);
            _testGame.AddRoom(9, 5, 13, 10, 0, false, 0, 0);
            _testGame.AddRoom(10, 6, 0, 11, 9, false, 0, 0);
            _testGame.AddRoom(11, 7, 0, 0, 10, false, 0, 3);
            _testGame.AddRoom(12, 0, 16, 0, 0, false, 0, 4);
            _testGame.AddRoom(13, 9, 0, 14, 0, false, 0, 5);
            _testGame.AddRoom(14, 0, 0, 15, 13, false, 0, 0);
            _testGame.AddRoom(15, 0, 0, 16, 14, false, 5, 0);
            _testGame.AddRoom(16, 12, 0, 0, 15, false, 0, 0);
            ctx.SaveChanges();

        foreach(Room room in _testGame.Rooms)
            {
                ctx.Rooms.Add(room);
            }

            _testGame.CurrentRoom.Should().Be(16);
            _testGame.Rooms.Count().Should().Be(16);

        }
    }
}
