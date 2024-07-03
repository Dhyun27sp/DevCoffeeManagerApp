using Newtonsoft.Json;
using System.Collections.Generic;

namespace DevCoffeeManagerApp.Shipping
{
    public class Stop
    {
        [JsonProperty("coordinates")]
        public Coordinates coordinates { get; set; }

        [JsonProperty("address")]
        public string address { get; set; }
    }
    public class Coordinates
    {
        [JsonProperty("lat")]
        public string latitude { get; set; }

        [JsonProperty("lng")]
        public string longitude { get; set; }
    }

    public class Contact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    public class OrderItem
    {
        [JsonProperty("quantity")]
        public string Quantity { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; } = "LESS_THAN_10_KG";

        [JsonProperty("categories")]
        public List<string> Categories { get; set; } = new List<string>{ "FOOD_AND_BEVERAGE" };
    }
}
