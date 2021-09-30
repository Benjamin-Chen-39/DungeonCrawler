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
            db.AddRoom(1, 0, 0, 2, 0, true, 0, 0);
            db.AddRoom(2, 0, 0, 3, 1, false, 1, 0);
            db.AddRoom(3, 0, 7, 4, 2, false, 2, 0);
            db.AddRoom(4, 0, 8, 0, 3, false, 0, 0);
            db.AddRoom(5, 0, 9, 0, 0, false, 3, 1);
            db.AddRoom(6, 0, 10, 7, 0, false, 0, 2);
            db.AddRoom(7, 3, 11, 8, 6, false, 0, 0);
            db.AddRoom(8, 4, 0, 0, 7, false, 4, 0);
            db.AddRoom(9, 5, 13, 10, 0, false, 0, 0);
            db.AddRoom(10, 6, 0, 11, 9, false, 0, 0);
            db.AddRoom(11, 7, 0, 0, 10, false, 0, 3);
            db.AddRoom(12, 0, 16, 0, 0, false, 0, 4);
            db.AddRoom(13, 9, 0, 14, 0, false, 0, 5);
            db.AddRoom(14, 0, 0, 15, 13, false, 0, 0);
            db.AddRoom(15, 0, 0, 16, 14, false, 5, 0);
            db.AddRoom(16, 12, 0, 0, 15, false, 0, 0);
            db.SaveChanges();

            var game = new Game(db);

        }
    }
}
