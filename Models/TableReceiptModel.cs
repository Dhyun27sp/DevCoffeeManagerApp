using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class TableReceiptModel
    {       
        [BsonId, BsonElement("_No"), BsonRepresentation(BsonType.Int32)]
        public int _No { get; set; }
    }
}
