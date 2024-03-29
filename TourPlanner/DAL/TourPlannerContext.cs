﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    public class TourPlannerContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }

        public DbSet<TourLog> TourLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string configFilePath = "D:\\FH Software Engineering\\Zweites Semester\\Semesterprojekt\\TourPlanner\\TourPlanner\\TourPlanner\\appsettings.json";

            // Read the JSON configuration file
            string json = File.ReadAllText(configFilePath);

            // Parse the JSON string
            var jsonObject = JObject.Parse(json);

            // Retrieve the connection string from the parsed JSON object
            string connectionString = (string)jsonObject["ConnectionStrings"]["DefaultConnection"];

            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
