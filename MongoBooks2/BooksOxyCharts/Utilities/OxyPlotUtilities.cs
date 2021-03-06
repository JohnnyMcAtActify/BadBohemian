﻿namespace BooksOxyCharts.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using OxyPlot;
    using OxyPlot.Series;
    using BooksCore.Geography;

    public class OxyPlotUtilities
    {
        #region Constants

        // taken from http://dmcritchie.mvps.org/excel/colors.htm
        private static readonly List<Tuple<byte, byte, byte>> StandardColours =
            new List<Tuple<byte, byte, byte>>
            {
                                    // R    G    B
            new Tuple<byte,byte,byte>(255,	0   ,0),
            new Tuple<byte,byte,byte>(0,	255 ,0),
            new Tuple<byte,byte,byte>(0,	0	,255),
            new Tuple<byte,byte,byte>(255,	255	,0),
            new Tuple<byte,byte,byte>(255,	0	,255),
            new Tuple<byte,byte,byte>(0,	255	,255),
            new Tuple<byte,byte,byte>(128,	0	,0),
            new Tuple<byte,byte,byte>(0,	128	,0),
            new Tuple<byte,byte,byte>(0,	0,	128),
            new Tuple<byte,byte,byte>(128,	128	,0),
            new Tuple<byte,byte,byte>(128,	0	,128),
            new Tuple<byte,byte,byte>(0,	128	,128),
            new Tuple<byte,byte,byte>(153,	153	,255),
            new Tuple<byte,byte,byte>(153,	51	,102),
            new Tuple<byte,byte,byte>(255,	255	,204),
            new Tuple<byte,byte,byte>(204,	255	,255),
            new Tuple<byte,byte,byte>(102,	0	,102),
            new Tuple<byte,byte,byte>(255,	128	,128),
            new Tuple<byte,byte,byte>(0,	102	,204),
            new Tuple<byte,byte,byte>(204,	204	,255),
            new Tuple<byte,byte,byte>(0,	0	,128),
            new Tuple<byte,byte,byte>(255,	0	,255),
            new Tuple<byte,byte,byte>(255,	255	,0),
            new Tuple<byte,byte,byte>(0,	255	,255),
            new Tuple<byte,byte,byte>(128,	0	,128),
            new Tuple<byte,byte,byte>(128,	0	,0),
            new Tuple<byte,byte,byte>(0,	128	,128),
            new Tuple<byte,byte,byte>(0,	0	,255),
            new Tuple<byte,byte,byte>(0,	204	,255),
            new Tuple<byte,byte,byte>(204,	255	,255),
            new Tuple<byte,byte,byte>(204,	255	,204),
            new Tuple<byte,byte,byte>(255,	255	,153),
            new Tuple<byte,byte,byte>(153,	204	,255),
            new Tuple<byte,byte,byte>(255,	153	,204),
            new Tuple<byte,byte,byte>(204,	153	,255),
            new Tuple<byte,byte,byte>(255,	204	,153),
            new Tuple<byte,byte,byte>(51,	102	,255),
            new Tuple<byte,byte,byte>(51,	204	,204),
            new Tuple<byte,byte,byte>(153,	204	,0),
            new Tuple<byte,byte,byte>(255,	204	,0),
            new Tuple<byte,byte,byte>(255,	153	,0),
            new Tuple<byte,byte,byte>(255,	102	,0),
            new Tuple<byte,byte,byte>(102,	102	,153),
            new Tuple<byte,byte,byte>(150,	150	,150),
            new Tuple<byte,byte,byte>(0,	51	,102),
            new Tuple<byte,byte,byte>(51,	153	,102),
            new Tuple<byte,byte,byte>(0,	51	,0),
            new Tuple<byte,byte,byte>(51,  51	,0),
            new Tuple<byte,byte,byte>(153,	51	,0),
            new Tuple<byte,byte,byte>(153,	51	,102),
            new Tuple<byte,byte,byte>(51,	51	,153),
        };

        #endregion

        public static void SetupPlotLegend(PlotModel newPlot,
            string title = "Performance Curves")
        {
            newPlot.LegendTitle = title;
            newPlot.LegendOrientation = LegendOrientation.Horizontal;
            newPlot.LegendPlacement = LegendPlacement.Outside;
            newPlot.LegendPosition = LegendPosition.TopRight;
            newPlot.LegendBackground = OxyColor.FromAColor(200, OxyColors.White);
            newPlot.LegendBorder = OxyColors.Black;
            newPlot.LegendMargin = 20;
        }

        public static void AddLineSeriesToModel(PlotModel newPlot, LineSeries[] lineSeries)
        {
            foreach (LineSeries series in lineSeries)
            {
                newPlot.Series.Add(series);
            }
        }

        public static void PopulateVerticalLineSeries(LineSeries series, double xValue, double yMin, double yMax)
        {
            series.Points.Add(new DataPoint(xValue, yMin));
            series.Points.Add(new DataPoint(xValue, yMax));
        }

        public static void CreateVerticalLineSeries(out LineSeries seriesEspMin, out LineSeries seriesEspMax,
            out LineSeries seriesOperatingPoint, out LineSeries seriesBestEfficiencyPoint,
            out LineSeries seriesMaxProductionPoint,
            out LineSeries seriesCatalogueCurveOperatingPoint,
            out LineSeries seriesCatalogueCurveBestEfficiencyPoint,
            string xAxisKey, string yAxisKey = "Head")
        {
            seriesEspMin = new LineSeries
            {
                Title = "ESP Min Flow",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Cyan,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };

            seriesEspMax = new LineSeries
            {
                Title = "ESP Max Flow",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.YellowGreen,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };

            seriesOperatingPoint = new LineSeries
            {
                Title = "Operating Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Brown,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.LongDash,
                StrokeThickness = 2
            };

            seriesBestEfficiencyPoint = new LineSeries
            {
                Title = "Best Efficiency Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Blue,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Dash,
                StrokeThickness = 2
            };

            seriesMaxProductionPoint = new LineSeries
            {
                Title = "Max Production Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Black,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };

            seriesCatalogueCurveOperatingPoint = new LineSeries
            {
                Title = "Catalogue Operating Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.OrangeRed,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.DashDotDot,
                StrokeThickness = 2
            };

            seriesCatalogueCurveBestEfficiencyPoint = new LineSeries
            {
                Title = "Catalogue Best Efficiency Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.RosyBrown,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.DashDotDot,
                StrokeThickness = 2
            };
        }

        public static void CreateVerticalLineSeries(out LineSeries seriesEspMin, out LineSeries seriesEspMax,
            out LineSeries seriesOperatingPoint, out LineSeries seriesBestEfficiencyPoint,
            string xAxisKey, string yAxisKey = "Head")
        {
            seriesEspMin = new LineSeries
            {
                Title = "ESP Min Flow",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Cyan,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };

            seriesEspMax = new LineSeries
            {
                Title = "ESP Max Flow",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.YellowGreen,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Solid,
                StrokeThickness = 2
            };

            seriesOperatingPoint = new LineSeries
            {
                Title = "Operating Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Brown,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.LongDash,
                StrokeThickness = 2
            };

            seriesBestEfficiencyPoint = new LineSeries
            {
                Title = "Best Efficiency Point",
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = OxyColors.Orange,
                MarkerType = MarkerType.None,
                LineStyle = LineStyle.Dash,
                StrokeThickness = 2
            };
        }


        public static void CreateScatterPointSeries(out ScatterSeries series,
            string xAxisKey, string yAxisKey, string title)
        {
            series = new ScatterSeries 
            {
                MarkerType = MarkerType.Circle,
                Title = title,
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey
            };
        }


        public static void CreateLineSeries(out LineSeries series,
            string xAxisKey, string yAxisKey, string title, int colourIndex)
        {
            List<OxyColor> coloursArray = new List<OxyColor>()
            {
                OxyColors.Red,
                OxyColors.Blue,
                OxyColors.Green,
                OxyColors.PaleVioletRed,
                OxyColors.LightBlue,
                OxyColors.LightGreen
            };

            int index = colourIndex % coloursArray.Count;
            OxyColor colour = coloursArray[index];

            series = new LineSeries
            {
                Title = title,
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = colour
            };
        }

        public static void LinearRegression(List<double> xVals, List<double> yVals,
                                            out double rsquared, out double yintercept,
                                            out double slope)
        {
            double sumOfX = 0;
            double sumOfY = 0;
            double sumOfXSq = 0;
            double sumOfYSq = 0;
            double sumCodeviates = 0;
            double count = xVals.Count;
            rsquared = yintercept = slope = 0.0;
            if (xVals.Count != yVals.Count || xVals.Count < 1) return;

            for (int ctr = 0; ctr < count; ctr++)
            {
                double x = xVals[ctr];
                double y = yVals[ctr];
                sumCodeviates += x * y;
                sumOfX += x;
                sumOfY += y;
                sumOfXSq += x * x;
                sumOfYSq += y * y;
            }
            double ssX = sumOfXSq - ((sumOfX * sumOfX) / count);
            double residualNumerator = (count * sumCodeviates) - (sumOfX * sumOfY);
            double residualDenominator = (count * sumOfXSq - (sumOfX * sumOfX)) * (count * sumOfYSq - (sumOfY * sumOfY));
            double sCo = sumCodeviates - ((sumOfX * sumOfY) / count);

            double meanX = sumOfX / count;
            double meanY = sumOfY / count;
            double dblR = residualNumerator / Math.Sqrt(residualDenominator);
            rsquared = dblR * dblR;
            yintercept = meanY - ((sCo / ssX) * meanX);
            slope = sCo / ssX;
        }

        public static List<OxyColor> SetupStandardColourSet(byte aValue = 225)
        {
            List<OxyColor> standardColours = new List<OxyColor>();

            foreach (Tuple<byte, byte, byte> colour in StandardColours)
            {
                standardColours.Add(OxyColor.FromArgb(aValue, colour.Item1, colour.Item2, colour.Item3));
            }

            return standardColours;
        }

        public static void CreateLongLineSeries(out LineSeries series, string xAxisKey,
            string yAxisKey, string title, int colourIndex, byte aValue = 225)
        {
            List<OxyColor> coloursArray = SetupStandardColourSet(aValue);

            int index = colourIndex % coloursArray.Count;
            OxyColor colour = coloursArray[index];

            series = new LineSeries
            {
                Title = title,
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = colour
            };
        }

        public static void CreateLongAreaSeries(out AreaSeries series, string xAxisKey,
            string yAxisKey, string title, int colourIndex, byte aValue = 225)
        {
            List<OxyColor> coloursArray = SetupStandardColourSet(aValue);

            int index = colourIndex % coloursArray.Count;
            OxyColor colour = coloursArray[index];

            series = new AreaSeries
            {
                Title = title,
                XAxisKey = xAxisKey,
                YAxisKey = yAxisKey,
                Color = colour,
                Color2 = OxyColors.Transparent
            };
        }

        public static IEnumerable<AreaSeries> StackLineSeries(IList<LineSeries> series)
        {
            double[] total = new double[series[0].Points.Count];

            for (int s = 0; s < series.Count; s++)
            {
                LineSeries lineSeries = series[s];
                AreaSeries areaSeries = new AreaSeries()
                {
                    Title = lineSeries.Title,
                    Color = lineSeries.Color,
                };

                for (int p = 0; p < lineSeries.Points.Count; p++)
                {
                    double x = lineSeries.Points[p].X;
                    double y = lineSeries.Points[p].Y;

                    areaSeries.Points.Add(new DataPoint(x, total[p]));
                    total[p] += y;
                    areaSeries.Points2.Add(new DataPoint(x, total[p]));
                }

                yield return areaSeries;
            }
        }

        public static void GetNewModelForPieSeries(
            out PlotModel modelP1, out dynamic seriesP1, string title)
        {
            modelP1 = new PlotModel { Title = title, Padding = new OxyThickness(15), LegendPadding = 15, TitlePadding = 15 };

            seriesP1 = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,

                AngleSpan = 360,
                StartAngle = 270,

                InsideLabelFormat = "{0}",
                OutsideLabelFormat = "{1}",
                TrackerFormatString = "{1} {2:0.0}",
                LabelField = "{0} {1} {2:0.0}"
            };
        }

        public static void AddResultsToPieChart(
            dynamic seriesP1, List<KeyValuePair<string, int>> totals, byte aValue = 225)
        {
            int colourIndex = 0;

            List<OxyColor> coloursArray = SetupStandardColourSet(aValue);

            foreach (KeyValuePair<string, int> total in totals)
            {
                string name = total.Key;
                int count = total.Value;

                OxyColor color = coloursArray[colourIndex % coloursArray.Count];
                bool isExploded = (count < 20);

                if (count > 0)
                    seriesP1.Slices.Add(
                        new PieSlice(name, count) { IsExploded = isExploded, Fill = color });

                colourIndex++;
            }
        }

        public static PlotModel CreatePieSeriesModelForResultsSet(
            List<KeyValuePair<string, int>> results, string title, byte aValue = 225)
        {
            PlotModel modelP1;
            dynamic seriesP1;

            GetNewModelForPieSeries(out modelP1, out seriesP1, title);

            AddResultsToPieChart(seriesP1, results, aValue);

            modelP1.Series.Add(seriesP1);

            return modelP1;
        }

        public static int SetupFaintPaletteForRange(
            int range, out List<OxyColor> colors, out OxyPalette faintPalette, byte aValue = 225)
        {
            // add 20% tolerance to the range
            range *= 12;
            range /= 10;
            if (range < 15)
                range = 5;

            // set up the colours
            colors = new List<OxyColor>();
            foreach (OxyColor color in OxyPalettes.Jet(range).Colors)
            {
                OxyColor faintColor = OxyColor.FromArgb(aValue, color.R, color.G, color.B);
                colors.Add(faintColor);
            }

            // then put them into tthe palette
            faintPalette = new OxyPalette(colors);
            return range;
        }


        public static void AddCountryGeographyAreaSeriesToPlot(
            PlotModel newPlot, CountryGeography country, OxyColor colour, string title, string tag, string trackerFormat)
        {
            int i = 0;
            IOrderedEnumerable<PolygonBoundary> landBlocks = country.LandBlocks.OrderByDescending(b => b.TotalArea);

            foreach (var boundary in landBlocks)
            {
                AreaSeries areaSeries = new AreaSeries
                {
                    Color = colour,
                    Title = title,
                    RenderInLegend = false,
                    Tag = tag
                };

                List<PolygonPoint> points = boundary.Points;
                if (points.Count > PolygonReducer.MaxPolygonPoints)
                    points = PolygonReducer.AdaptativePolygonReduce(points, PolygonReducer.MaxPolygonPoints);

                foreach (PolygonPoint point in points)
                {
                    double ptX;
                    double ptY;
                    point.GetCoordinates(out ptX, out ptY);

                    DataPoint dataPoint = new DataPoint(ptX, ptY);
                    areaSeries.Points.Add(dataPoint);
                }

                areaSeries.TrackerFormatString = trackerFormat;
                newPlot.Series.Add(areaSeries);

                // just do the 10 biggest bits per country (looks to be enough)
                i++;
                if (i > 10)
                    break;
            }
        }

    }
}
