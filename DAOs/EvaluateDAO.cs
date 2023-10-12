using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevCoffeeManagerApp.Config;
using DevCoffeeManagerApp.Models;
using MongoDB.Driver;

namespace DevCoffeeManagerApp.DAOs
{
    public class EvaluateDAO
    {
        private IMongoCollection<EvaluateModel> collection;
        public EvaluateDAO()
        {
            IMongoDatabase db = ConnectionMongoDB.getdatabase();
            collection = db.GetCollection<EvaluateModel>("Evaluate");
        }

        public void createEvaluate(EvaluateModel evaluamodel)
        {
            collection.InsertOne(evaluamodel);
        }
        
    }
}
