using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

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
