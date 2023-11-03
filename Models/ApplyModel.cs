using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class ApplyModel
    {
        [BsonElement("menu")]
        public List<MenuModel> menu { get; set; }

        [BsonElement("dish")]
        public List<DishModel> dish { get; set; }
    }
}
