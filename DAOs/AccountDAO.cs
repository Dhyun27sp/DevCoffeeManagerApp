using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Bson;

namespace DevCoffeeManagerApp.DAOs
{
    public class AccountDAO
    {
        private IMongoCollection<AccountModel> collection;
        public AccountDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<AccountModel>("Account");
        }

        public void createAccount(AccountModel Account)
        {
            collection.InsertOne(Account);
        }

        public AccountModel getAccount(string phone_number)
        {
            var filter = Builders<AccountModel>.Filter.Eq("phone_number", phone_number); // Tạo một bộ lọc dựa trên phone_number
            AccountModel accountModel = collection.Find(filter).FirstOrDefault(); // Thực hiện truy vấn và lấy bản ghi đầu tiên hoặc null nếu không tìm thấy.
            return accountModel;
        }
    }
}
