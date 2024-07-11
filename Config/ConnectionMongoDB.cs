

using MongoDB.Driver;

namespace DevCoffeeManagerApp.Config
{
    public class ConnectionMongoDB
    {
        private static string username = "20110ABC";
        private static string password = "cafe2023";
        
        public static IMongoDatabase getdatabase()
        {

            MongoClient dbClient;
            try
            {
                //push
                dbClient = new MongoClient("mongodb+srv://" + username + ":" + password + "@cafe-manager.jpn3aq0.mongodb.net/");
            }
            catch
            {
                dbClient = new MongoClient("mongodb://localhost:27017/store");
            }
            var database = dbClient.GetDatabase("store");
            return database;
        }
    }
}
