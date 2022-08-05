// This file was auto-generated by ML.NET Model Builder. 

using Microsoft.ML.Data;
using System;

namespace LogSystemML.Model
{
    public class ModelInput
    {
        [ColumnName("TripId"), LoadColumn(0)]
        public float TripId { get; set; }


        [ColumnName("Start"), LoadColumn(1)]
        public string Start { get; set; }


        [ColumnName("End"), LoadColumn(2)]
        public string End { get; set; }


        [ColumnName("WeatherType"), LoadColumn(3)]
        public string WeatherType { get; set; }


        [ColumnName("Visibility"), LoadColumn(4)]
        public float Visibility { get; set; }


        [ColumnName("Temperature"), LoadColumn(5)]
        public float Temperature { get; set; }


        [ColumnName("LocationId"), LoadColumn(6)]
        public float LocationId { get; set; }


        [ColumnName("LocationId2"), LoadColumn(7)]
        public float LocationId2 { get; set; }


        [ColumnName("DriverId"), LoadColumn(8)]
        public float DriverId { get; set; }


        [ColumnName("AutotypeId"), LoadColumn(9)]
        public float AutotypeId { get; set; }


        [ColumnName("OrderId"), LoadColumn(10)]
        public float OrderId { get; set; }


        [ColumnName("Time"), LoadColumn(11)]
        public float Time { get; set; }


    }

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
        public string conditions { get; set; }
        public Nullable<int> visibility { get; set; }
        public Nullable<int> temp { get; set; }
    }

    public class RootObject
    {
        public Nullable<float> visibility { get; set; }
        public Nullable<float> temp { get; set; }
        public string conditions { get; set; }
    }
}