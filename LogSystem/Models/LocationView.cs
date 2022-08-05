using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.ML.Data;


namespace LogSystem.Modelss
{
    public class LocationView
    {
        public int TripId { get; set; }
        public Nullable<System.DateTime> Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string WeatherType { get; set; }
        public Nullable<int> Visibility { get; set; }
        public Nullable<int> Temperature { get; set; }

        public Nullable<int> LocationId { get; set; }
        public Nullable<int> LocationId2 { get; set; }

        public string LocationName { get; set; }
        public string LocationName2 { get; set; }
        public string LocationType { get; set; }
        public string LocationAddress { get; set; }
        public string LocationAddress2 { get; set; }

        public Nullable<int> DriverId { get; set; }
        public string DriverName { get; set; }

        public Nullable<int> AutotypeId { get; set; }
        public string AutotypeName { get; set; }

        public Nullable<int> OrderId { get; set; }
        public string OrderName { get; set; }

        public Nullable<int> Time { get; set; }
    }

    public class ResultViewModel
    {
        public string weather_state_name { get; set; }
        public Nullable<int> visibility { get; set; }
        public Nullable<int> the_temp { get; set; }

    }

    public class RootObject
    {
        public Nullable<float> visibility { get; set; }
        public Nullable<float> the_temp { get; set; }
        public string weather_state_name { get; set; }

    }


}