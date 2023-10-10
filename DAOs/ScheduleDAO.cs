using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;

namespace DevCoffeeManagerApp.DAOs
{
    public class ScheduleDAO
    {
        private IMongoCollection<ScheduleModel> collection;
        public ScheduleDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<ScheduleModel>("Schedule");
        }

        public void createSchedule(ScheduleModel scheduleModel)
        {
            collection.InsertOne(scheduleModel);
        }
    }
}
