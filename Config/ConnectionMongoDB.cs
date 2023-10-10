using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Config
{
    public class ConnectionMongoDB
    {
        private static string username = "20110ABC";
        private static string password = "cafe2023";
        public static IMongoDatabase getdatabase()
        {
            var dbClient = new MongoClient("mongodb+srv://" + username + ":" + password + "@cafe-manager.jpn3aq0.mongodb.net/");
            var database = dbClient.GetDatabase("store");
            return database;
        }
    }
}
