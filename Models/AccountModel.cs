﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class AccountModel
    {
        [BsonElement("password"),BsonRepresentation(BsonType.Int32)]
        public int Password { get; set; }

        [BsonElement("role"), BsonRepresentation(BsonType.String)]
        public string Role { get; set; }
    }
}
