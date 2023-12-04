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
    public class CustomerModel
    {
        // Constructor
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string name { get; set; }

        [BsonElement("phone_number"), BsonRepresentation(BsonType.String)]
        public string phone_number { get; set; }

        [BsonElement("dob"), BsonRepresentation(BsonType.String)]
        public string dob { get; set; }

        [BsonElement("point"), BsonRepresentation(BsonType.Int32)]
        public int point { get; set; }
        public int pluspoint { get; set; }
        public string usedpoint { get; set; }

        public CustomerModel(string name, string phone_number, string dob, int point)
        {
            this.name = name;
            this.phone_number = phone_number;
            this.dob = dob;
            this.point = point;
        }
        public CustomerModel()
        {
        }
    }
}
