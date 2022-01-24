using System;

/// <summary>
/// Unity save game namespace
/// </summary>
namespace UnitySaveGame
{
    /// <summary>
    /// An interface that represents date and time data
    /// </summary>
    public interface IDateTimeData
    {
        /// <summary>
        /// Minute
        /// </summary>
        int Year { get; set; }

        /// <summary>
        /// Minute
        /// </summary>
        int Month { get; set; }

        /// <summary>
        /// Minute
        /// </summary>
        int Day { get; set; }

        /// <summary>
        /// Minute
        /// </summary>
        int Hour { get; set; }

        /// <summary>
        /// Minute
        /// </summary>
        int Minute { get; set; }

        /// <summary>
        /// Second
        /// </summary>
        public int Second { get; set; }

        /// <summary>
        /// Millisecond
        /// </summary>
        int Millisecond { get; set; }

        /// <summary>
        /// Date and time
        /// </summary>
        DateTime DateTime { get; set; }
    }
}
