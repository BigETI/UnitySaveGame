using System;
using UnityEngine;

/// <summary>
/// Unity save game data namespace
/// </summary>
namespace UnitySaveGame.Data
{
    /// <summary>
    /// A class that describes date and time data
    /// </summary>
    [Serializable]
    public class DateTimeData : IDateTimeData
    {
        /// <summary>
        /// Minute
        /// </summary>
        [SerializeField]
        private int year;

        /// <summary>
        /// Minute
        /// </summary>
        [SerializeField]
        private int month;

        /// <summary>
        /// Minute
        /// </summary>
        [SerializeField]
        private int day;

        /// <summary>
        /// Minute
        /// </summary>
        [SerializeField]
        private int hour;

        /// <summary>
        /// Minute
        /// </summary>
        [SerializeField]
        private int minute;

        /// <summary>
        /// Second
        /// </summary>
        [SerializeField]
        private int second;

        /// <summary>
        /// Millisecond
        /// </summary>
        [SerializeField]
        private int millisecond;

        /// <summary>
        /// Minute
        /// </summary>
        public int Year
        {
            get => year;
            set => year = value;
        }

        /// <summary>
        /// Minute
        /// </summary>
        public int Month
        {
            get => month;
            set => month = value;
        }

        /// <summary>
        /// Minute
        /// </summary>
        public int Day
        {
            get => day;
            set => day = value;
        }

        /// <summary>
        /// Minute
        /// </summary>
        public int Hour
        {
            get => hour;
            set => hour = value;
        }

        /// <summary>
        /// Minute
        /// </summary>
        public int Minute
        {
            get => minute;
            set => minute = value;
        }

        /// <summary>
        /// Second
        /// </summary>
        public int Second
        {
            get => second;
            set => second = value;
        }

        /// <summary>
        /// Millisecond
        /// </summary>
        public int Millisecond
        {
            get => millisecond;
            set => millisecond = value;
        }

        /// <summary>
        /// Date and time
        /// </summary>
        public DateTime DateTime
        {
            get => new DateTime(year, month, day, hour, minute, second, millisecond);
            set
            {
                year = value.Year;
                month = value.Month;
                day = value.Day;
                hour = value.Hour;
                minute = value.Minute;
                second = value.Second;
                millisecond = value.Millisecond;
            }
        }

        /// <summary>
        /// Constructs date and time data
        /// </summary>
        /// <param name="dateTime">Date and time</param>
        public DateTimeData(DateTime dateTime) => DateTime = dateTime;

        /// <summary>
        /// Constructs date and time data
        /// </summary>
        /// <param name="dateTimeData">Date and time data</param>
        public DateTimeData(DateTimeData dateTimeData)
        {
            if (dateTimeData == null)
            {
                throw new ArgumentNullException(nameof(dateTimeData));
            }
            DateTime = dateTimeData.DateTime;
        }
    }
}
