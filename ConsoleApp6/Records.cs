using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    public static class Records
    {
        private const string FileName = "leaderboard.json";
        private static List<User> users;

        static Records()
        {
            LoadLeaderboard();
        }

        public static void AddUser(User user)
        {
            users.Add(user);
            SaveLeaderboard();
        }

        public static List<User> GetUsers()
        {
            return new List<User>(users);
        }

        private static void LoadLeaderboard()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                users = JsonConvert.DeserializeObject<List<User>>(json);
            }
            else
            {
                users = new List<User>();
            }
        }

        private static void SaveLeaderboard()
        {
            //сохраняет в ...\ConsoleApp6\bin\Debug\net6.0
            string json = JsonConvert.SerializeObject(users);
            File.WriteAllText(FileName, json);
        }
    }
}
