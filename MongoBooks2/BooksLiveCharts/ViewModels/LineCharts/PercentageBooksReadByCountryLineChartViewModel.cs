﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PercentageBooksReadByCountryLineChartViewModel.cs" company="N/A">
//   2016
// </copyright>
// <summary>
//   The percentage books read by country with time line chart view model class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace BooksLiveCharts.ViewModels.LineCharts
{
    using System;
    using LiveCharts;

    /// <summary>
    /// The percentage books read by country with time line chart view model class.
    /// </summary>
    public sealed class PercentageBooksReadByCountryLineChartViewModel : BasePercentageLineChartViewModel
    {
        /// <summary>
        /// Sets up the line chart series.
        /// </summary>
        protected override void SetupSeries()
        {
            SetupSeries(isBooks: true, isCountries: true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PercentageBooksReadByCountryLineChartViewModel"/> class.
        /// </summary>
        public PercentageBooksReadByCountryLineChartViewModel()
        {
            Title = "Percentage Books Read by Country With Time";
            PointLabel = chartPoint => $"({XAxisTitle} {new DateTime((long)chartPoint.X):d}, {YAxisTitle} {chartPoint.Y:G5})";
            LegendLocation = LegendLocation.Top;
            SetupSeries();
        }
    }
}
