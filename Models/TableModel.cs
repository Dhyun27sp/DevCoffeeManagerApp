﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace DevCoffeeManagerApp.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class TableModel
    {
        [BsonElement("No_"), BsonRepresentation(BsonType.Int32)]
        public int No_ { get; set; }

        [BsonElement("Number_of_seat"), BsonRepresentation(BsonType.Int32), BsonIgnoreIfNull]
        public int? Seat { get; set; }
        [BsonElement("Status"), BsonRepresentation(BsonType.Boolean), BsonIgnoreIfNull]
        public bool? Status { get; set; }

        public int Floor {  get; set; }

        public TableModel(int no_, int seat, bool status)
        {
            No_ = no_;
            Seat = seat;
            Status = status;
        }
        public TableModel(int no_)
        {
            No_ = no_;
        }
    }
}
