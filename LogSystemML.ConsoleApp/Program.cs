// This file was auto-generated by ML.NET Model Builder. 

using System;
using LogSystemML.Model;

namespace LogSystemML.ConsoleApp
{
    class Program
    {

        static void Main(string[] args)
        {
            
            // Create single instance of sample data from first line of dataset for model input
            ModelInput sampleData = new ModelInput()
            {
                WeatherType = @"Heavy Cloud",
                Visibility = 13F,
                Temperature = 4F,
                LocationId = 1F,
                LocationId2 = 2F,
                DriverId = 1F,
                AutotypeId = 1F,
                OrderId = 1F,
            };

            // Make a single prediction on the sample data and print results
            var predictionResult = ConsumeModel.Predict(sampleData);

            Console.WriteLine("Using model to make single prediction -- Comparing actual Time with predicted Time from sample data...\n\n");
            Console.WriteLine($"WeatherType: {sampleData.WeatherType}");
            Console.WriteLine($"Visibility: {sampleData.Visibility}");
            Console.WriteLine($"Temperature: {sampleData.Temperature}");
            Console.WriteLine($"LocationId: {sampleData.LocationId}");
            Console.WriteLine($"LocationId2: {sampleData.LocationId2}");
            Console.WriteLine($"DriverId: {sampleData.DriverId}");
            Console.WriteLine($"AutotypeId: {sampleData.AutotypeId}");
            Console.WriteLine($"OrderId: {sampleData.OrderId}");
            Console.WriteLine($"\n\nPredicted Time: {predictionResult.Score}\n\n");
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();

            //var botClient = new TelegramBotClient("5553022128:AAGAkiiO6nZnB7R3iFTsPX0pqqsr5MzXlK8");
        }
    }
}