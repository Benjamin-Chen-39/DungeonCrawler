using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace lib
{
    public class Database : DbContext
    {
    
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Treasure> Treasures { get; set; }  
        


        // public void AddRoom(int Id, int NorthRoomId, int SouthRoomId, int EastRoomId, int WestRoomId, bool isEscapeRoom, int MonsterId, int TreasureId)
        // {
        //     Rooms.Add(new Room { Id = Id, NorthRoomId = NorthRoomId, SouthRoomId = SouthRoomId, EastRoomId = EastRoomId, WestRoomId = WestRoomId, isEscapeRoom = isEscapeRoom, MonsterId = MonsterId, TreasureId = TreasureId });
        //     if (MonsterId != 0)
        //         Monsters.Add(new Monster { Id = MonsterId, Health = 10, Attack = 3, Defense = 1, Name = "Goblin" });
        //     if (TreasureId != 0)
        //         Treasures.Add(new Treasure { Id = TreasureId, BonusStat = 2 });
        // }

        public Database() : base() { }
        public Database(DbContextOptions<Database> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options.IsConfigured is false)
            {
                var basedir = System.AppContext.BaseDirectory;
                var solndir = Directory.GetParent(basedir).Parent.Parent.Parent.Parent;
                options.UseSqlite($"Data Source={solndir.FullName}/db/app.db");
            }

            // to disable change tracking
            // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            // db.Entry(some_obj).State = EntityState.Detached;

            // use either option below to log the queries to the console
            // options.LogTo(Console.WriteLine, LogLevel.Information);
            // options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));

            // displays parameter values in logs
            options.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}