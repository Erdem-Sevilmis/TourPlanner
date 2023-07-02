﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;


namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            // Set the path to your JSON configuration file
            string configFilePath = "D:\\FH Software Engineering\\Zweites Semester\\Semesterprojekt\\TourPlanner\\TourPlanner\\TourPlanner\\appsettings.json";

            // Read the JSON configuration file
            string json = File.ReadAllText(configFilePath);

            // Parse the JSON string
            var jsonObject = JObject.Parse(json);

            // Retrieve the connection string from the parsed JSON object
            string connectionString = (string)jsonObject["ConnectionStrings"]["DefaultConnection"];

            string test = "222";
        }
    }
}
