using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;

namespace slnLoginPage.Common
{
    class Database
    {
        private string path;

        SQLiteConnection connection;

        public Database()
        {
            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "MyDatabase.sqlite");
            connection = new SQLiteConnection(new SQLitePlatformWinRT(), path);
            connection.CreateTable<User>();
        }

        public int Register(User user)
        {
            return connection.Insert(new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password
            });
        }

        public bool Login(string username, string password)
        {
            var query = connection.Table<User>()
                .Where(user => user.UserName == username && user.Password == password);

            if(query.Count() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
