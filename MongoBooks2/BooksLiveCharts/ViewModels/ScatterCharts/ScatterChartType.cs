﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ScatterChartType.cs" company="N/A">
//   2016
// </copyright>
// <summary>
//   The scatter-chart types enum.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace BooksLiveCharts.ViewModels.ScatterCharts
{
    using BooksLiveCharts.Utilities;

    /// <summary>
    /// Type of scatter chart.
    /// </summary>
    public enum ScatterChartType
    {
        [ChartType(Title = "Last 10 Books: Time Taken vs Pages",
            GeneratorClass = typeof(LastTenBooksAndPagesScatterChartViewModel))]
        LastTenBooksAndPages,

        [ChartType(Title = "Last 10 Books in Translation: Time Taken vs Pages",
            GeneratorClass = typeof(LastTenBooksAndPagesInTranslationScatterChartViewModel))]
        LastTenBooksAndPagesInTranslation,

        [ChartType(Title = "Last 10 Books: Time Taken vs Pages by time",
            GeneratorClass = typeof(LastTenBooksAndPagesWithTimeScatterChartViewModel))]
        LastTenBooksAndPagesWithTime
    }
}
