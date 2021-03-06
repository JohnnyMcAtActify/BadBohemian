﻿
namespace MongoDbBooks.ViewModels.PlotGenerators
{
    using System.Collections.Generic;
    using System.Linq;

    using OxyPlot;
    using OxyPlot.Series;
    using OxyPlot.Axes;

    using MongoDbBooks.ViewModels.Utilities;

    public class CountryLocationsBooksAndPagesPlotGenerator : IPlotGenerator
    {
        public OxyPlot.PlotModel SetupPlot(Models.MainBooksModel mainModel)
        {
            _mainModel = mainModel;
            return SetupCountryLocationsBooksAndPagesPlot();
        }
        private Models.MainBooksModel _mainModel;

        private PlotModel SetupCountryLocationsBooksAndPagesPlot()
        {
            // Create the plot model
            var newPlot = new PlotModel { Title = "Countries in Location with Books and Pages Plot" };
            OxyPlotUtilities.SetupPlotLegend(newPlot, "Countries in Location with Books and Pages Plot");
            SetupLatitudeAndLongitudeAxes(newPlot);

            // create series and add them to the plot
            AddBooksAndPagesScatterSeries(newPlot);
            
            return newPlot;
        }

        private void AddBooksAndPagesScatterSeries(PlotModel newPlot)
        {
            ScatterSeries pointsSeries;

            OxyPlotUtilities.CreateScatterPointSeries(out pointsSeries,
                ChartAxisKeys.LongitudeKey, ChartAxisKeys.LatitudeKey, "Countries");

            foreach (var authorCountry in _mainModel.AuthorCountries)
            {
                var name = authorCountry.Country;
                var country = _mainModel.WorldCountries.Where(w => w.Country == name).FirstOrDefault();
                if (country != null)
                {
                    var pointSize = authorCountry.TotalBooksReadFromCountry;
                    if (pointSize < 5) pointSize = 5;

                    ScatterPoint point =
                        new ScatterPoint(country.Longitude, country.Latitude, pointSize,
                        authorCountry.TotalPagesReadFromCountry) { Tag = name };
                    pointsSeries.Points.Add(point);
                }
            }

            pointsSeries.TrackerFormatString = "{Tag}\nLat/Long ( {4:0.###} ,{2:0.###} ) \nTotalPages {6}";
            newPlot.Series.Add(pointsSeries);

            List<OxyColor> colors = new List<OxyColor>();
            foreach (var color in OxyPalettes.Jet(200).Colors)
            {
                var faintColor = OxyColor.FromArgb(128, color.R, color.G, color.B);
                colors.Add(faintColor);
            }

            OxyPalette faintPalette = new OxyPalette(colors);

            newPlot.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = faintPalette, Title = "Total Pages" });
        }

        private void SetupLatitudeAndLongitudeAxes(PlotModel newPlot)
        {
            var xAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Longitude",
                Key = ChartAxisKeys.LongitudeKey,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                Maximum = 200,
                Minimum = -200
            };
            newPlot.Axes.Add(xAxis);

            var yAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                Title = "Latitude",
                Key = ChartAxisKeys.LatitudeKey,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                Maximum = 100,
                Minimum = -100
            };
            newPlot.Axes.Add(yAxis);
        }

    }
}
