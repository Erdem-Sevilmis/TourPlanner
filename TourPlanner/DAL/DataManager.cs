﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataManager
    {
        public readonly TourPlannerContext dbContext;

        public DataManager()
        {
            dbContext = new TourPlannerContext();
        }

        
        public List<Tour> GetAllToursFromDatabase()
        {
            return dbContext.Tours.ToList();
        }

        public void DeleteTourFromDb(Tour tourToDelete)
        {
            dbContext.Tours.Remove(tourToDelete);
            dbContext.SaveChanges();
        }

        public void DeleteTourLogFromDb(TourLog tourLogToDelete)
        {
            dbContext.TourLogs.Remove(tourLogToDelete);
            dbContext.SaveChanges();
        }


        public TourLog GetTourLogById(int tourLogId)
        {
            TourLog tourLogToEdit = dbContext.TourLogs.Where(x => x.Id == tourLogId).FirstOrDefault();
            return tourLogToEdit;

        }
        public Tour GetTourById(int tourId)
        {
            Tour tourToEdit = dbContext.Tours.Where(x => x.Id == tourId).FirstOrDefault();
            return tourToEdit;

        }
        public void AddNewTourToDb(Tour tour)
        {
            dbContext.Tours.Add(tour);
            dbContext.SaveChanges();
        }


        public void DbSaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}