﻿namespace MongoDbBooks.ViewModels.PlotGenerators
{
    using System.Collections.Generic;
    using System.Linq;

    using OxyPlot;
    using OxyPlot.Series;
    using OxyPlot.Axes;

    using MongoDbBooks.Models;
    using MongoDbBooks.ViewModels.Utilities;

    public class PercentageBooksReadByLanguagePlotGenerator : IPlotGenerator
    {
        public OxyPlot.PlotModel SetupPlot(Models.MainBooksModel mainModel)
        {
            _mainModel = mainModel;
            return SetupPercentageBooksReadByLanguagePlot();
        }
        private Models.MainBooksModel _mainModel;

        private PlotModel SetupPercentageBooksReadByLanguagePlot()
        {
            // Create the plot model
            var newPlot = new PlotModel { Title = "Percentage Books Read by Language With Time Plot" };
            OxyPlotUtilities.SetupPlotLegend(newPlot, "Percentage Books Read by Language With Time Plot");
            SetupPercentageBooksReadKeyVsTimeAxes(newPlot);

            // get the languages (in order)
            BooksDelta.DeltaTally latestTally = _mainModel.BookDeltas.Last().OverallTally;
            List<string> languages = (from item in latestTally.LanguageTotals
                                      orderby item.Item2 descending
                                      select item.Item1).ToList();

            // create the series for the languages
            List<KeyValuePair<string, LineSeries>> languagesSeries =
                new List<KeyValuePair<string, LineSeries>>();

            for (int i = 0; i < languages.Count; i++)
            {
                LineSeries languageSeries;
                OxyPlotUtilities.CreateLongLineSeries(out languageSeries,
                    ChartAxisKeys.DateKey, ChartAxisKeys.PercentageBooksReadKey, languages[i], i);
                languagesSeries.Add(
                    new KeyValuePair<string, LineSeries>(languages[i], languageSeries));
            }

            // loop through the deltas adding points for each of the items
            foreach (var delta in _mainModel.BookDeltas)
            {
                var date = DateTimeAxis.ToDouble(delta.Date);
                foreach (var languageLine in languagesSeries)
                {
                    double percentage = 0.0;
                    foreach (var langTotal in delta.OverallTally.LanguageTotals)
                        if (langTotal.Item1 == languageLine.Key)
                            percentage = langTotal.Item3;
                    languageLine.Value.Points.Add(new DataPoint(date, percentage));
                }
            }

            // add them to the plot
            foreach (var languagesItems in languagesSeries)
                newPlot.Series.Add(languagesItems.Value);

            // finally update the model with the new plot
            return newPlot;
        }

        private void SetupPercentageBooksReadKeyVsTimeAxes(PlotModel newPlot)
        {
            var xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Date",
                Key = ChartAxisKeys.DateKey,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                StringFormat = "yyyy-MM-dd"
            };
            newPlot.Axes.Add(xAxis);

            var lhsAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "% of Books Read",
                Key = ChartAxisKeys.PercentageBooksReadKey,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None
            };
            newPlot.Axes.Add(lhsAxis);
        }
    }
}
