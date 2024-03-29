﻿using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Models;
using TourPlanner.Views;
using System.Collections;
using System.Windows.Media.Imaging;
using static log4net.Appender.RollingFileAppender;
using DAL;
using BL;
using log4net.Config;

namespace TourPlanner.Viewmodels
{
    public class ManageToursViewModel : ViewModelBase
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //DataManager dataManagerForView;
        //TourManager tourLogicManagerForView;
        public event EventHandler CurrentSelectedTourUpdated;
        public List<Tour> Tours { get; set; } = new();
        public List<TourLog> logsOfCurrentTour = new List<TourLog>();
        private Tour _currentSelectedTour;
        public Tour CurrentSelectedTour
        {
            get { return _currentSelectedTour; }
            set
            {
                _currentSelectedTour = value;
                logsOfCurrentTour = new List<TourLog>();
                foreach (TourLog tourLog in TourPlannerDataManager.dbContext.TourLogs)
                {
                    if (_currentSelectedTour.Id == tourLog.TourId)
                    {
                        logsOfCurrentTour.Add(tourLog);
                    }
                }
            }
        }

        public void ChangeCurrentSelectedTour(Tour newSelection)
        {
            CurrentSelectedTour = newSelection;
            CurrentSelectedTourUpdated.Invoke(this, EventArgs.Empty);
        }

        public ManageToursViewModel()
        {
            //dataManagerForView = TourPlannerDataManager;
            //tourLogicManagerForView = TourPlannerLogicManager;
            //CurrentTours = new List<string>
            //{
            //    "test1",
            //    "test2",
            //    "Test3"
            //};
            Tours = TourPlannerDataManager.GetAllToursFromDatabase();
            AddTourCommand = new Commands.AddTourCommand(this);
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }





        public void OpenCreateTourPopup()
        {
            try
            {
                CreateTourPopupWindow popup = new CreateTourPopupWindow(this);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Error opening CreateTourPopupWindow", ex);
            }
        }

        public void OpenEditTourPopup(Tour tourToEdit)
        {
            try
            {
                EditTourPopupWindow popup = new EditTourPopupWindow(this, tourToEdit.Id, tourToEdit.Name, tourToEdit.From, tourToEdit.To, tourToEdit.TransportType);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Error opening EditTourPopupWindow", ex);
            }
        }

        public void OpenCreateTourLogPopup()
        {
            try
            {
                CreateTourLogPopupWindow popup = new CreateTourLogPopupWindow(this);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Error opening CreateTourLogPopupWindow", ex);
            }
        }

        public void OpenEditTourLogPopup(TourLog tourLogToEdit)
        {
            try
            {
                EditTourLogPopupWindow popup = new EditTourLogPopupWindow(this, tourLogToEdit);
                popup.ShowDialog();
            }
            catch (Exception ex)
            {
                _log.Error("Error opening EditTourLogPopupWindow", ex);
            }
        }





        public void EditTourToDatabase(int oldTourId, string Name, string From, string To, string TranportType)
        {

            var results = TourPlannerLogicManager.GetTimeAndDistance(new Uri("https://www.mapquestapi.com/directions/v2/route?key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG&from=" + From.Replace(' ', '+') + "&to=" + To.Replace(' ', '+') + "&unit=k"));
            TimeSpan tourTime = results.time;
            double tourDistance = results.distance;
            var image = TourPlannerLogicManager.GetImage(new Uri("https://www.mapquestapi.com/staticmap/v5/map?start=" + From.Replace(' ', '+') + "&end=" + To.Replace(' ', '+') + "&size=600,400@2x&key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG"));
            int imageId = TourPlannerLogicManager.GetImageId();
            SafeImageWithId(imageId, image);

            try
            {
                TourPlannerLogicManager.EditTourToDatabaseLogic(oldTourId, Name, From, To, TranportType, tourDistance, tourTime, imageId);
            }
            catch (Exception ex)
            {
                _log.Error("Error editing tour", ex);
            }

        }
        public void AddTourToDatabase(string Name, string From, string To, string TranportType)
        {
            var results = TourPlannerLogicManager.GetTimeAndDistance(new Uri("https://www.mapquestapi.com/directions/v2/route?key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG&from=" + From.Replace(' ', '+') + "&to=" + To.Replace(' ', '+') + "&unit=k"));
            TimeSpan tourTime = results.time;
            double tourDistance = results.distance;
            var image = TourPlannerLogicManager.GetImage(new Uri("https://www.mapquestapi.com/staticmap/v5/map?start=" + From.Replace(' ', '+') + "&end=" + To.Replace(' ', '+') + "&size=600,400@2x&key=RWjNFiNXi7QJ5jmjgYM7mjujwDcF3ebG"));
            int imageId = TourPlannerLogicManager.GetImageId();
            SafeImageWithId(imageId, image);


            try
            {
                TourPlannerLogicManager.AddTourToDatabaseLogic(Name, From, To, TranportType, tourTime, tourDistance, imageId);
            }
            catch (Exception ex)
            {
                _log.Error("Error adding tour", ex);
            }
        }



        private void SafeImageWithId(int imageId, byte[] imageBytes)
        {

            string currentFolderPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Images";
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                BitmapDecoder decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                BitmapFrame frame = decoder.Frames[0];

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(frame);

                using (FileStream output = new FileStream(currentFolderPath + "\\" + imageId + ".jpg", FileMode.Create))
                {
                    encoder.Save(output);
                }
            }
        }



        private List<string> _currentTours;

        public List<string> CurrentTours
        {
            get
            {
                return _currentTours;
            }
            set
            {
                _currentTours = value;
                OnPropertyChanged(nameof(CurrentTours));
            }
        }

        public ICommand AddTourCommand { get; }

        public ICommand DeleteTourCommand { get; }

        public ICommand ModifyTourCommand { get; }
    }
}
