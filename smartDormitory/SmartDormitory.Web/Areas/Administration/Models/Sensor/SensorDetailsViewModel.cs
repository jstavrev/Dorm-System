﻿using SmartDormitory.Models.DbModels;

namespace SmartDormitory.Web.Areas.Administration.Models.Sensor
{
    public class SensorDetailsViewModel
    {
        public SensorDetailsViewModel()
        {
        }

        public SensorDetailsViewModel(UserSensors userSensors)
        {
            this.Id = userSensors.Id;
            this.UserId = userSensors.UserId;
            this.SensorId = userSensors.SensorId;
            this.MinValue = userSensors.MinValue;
            this.MaxValue = userSensors.MaxValue;
            this.UpdateInterval = userSensors.UpdateInterval;
            this.Name = userSensors.Name;
            this.IsPublic = userSensors.IsPublic;
            this.IsRequiredNotification = userSensors.IsRequiredNotification;
            this.Latitude = userSensors.Latitude;
            this.Longitude = userSensors.Longitude;

        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public int SensorId { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public int UpdateInterval { get; set; }

        public string Name { get; set; }

        public bool IsPublic { get; set; }

        public bool IsRequiredNotification { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}