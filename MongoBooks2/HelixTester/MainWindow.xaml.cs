﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;

using HelixTester.ViewModels;


using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;


using _3DTools;

using HelixToolkit.Wpf;


namespace HelixTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly Stopwatch watch = new Stopwatch();

        private int numberOfPoints;

        private LinesVisual3D linesVisual;
        private PointsVisual3D pointsVisual;
        private ScreenSpaceLines3D screenSpaceLines;

        private Point3DCollection points;

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainVieWModel = new MainViewModel(this);
            DataContext = mainVieWModel;


            this.watch.Start();

            this.NumberOfPoints = 100;
            //this.DataContext = this;

            CompositionTarget.Rendering += this.OnCompositionTargetRendering;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public bool ShowLinesVisual3D { get; set; }

        public bool ShowPointsVisual3D { get; set; }

        public bool ShowScreenSpaceLines3D { get; set; }

        public bool ShowWireLines { get; set; }

        public Point3DCollection Points
        {
            get
            {
                return this.points;
            }

            set
            {
                this.points = value;
                this.RaisePropertyChanged("Points");
            }
        }

        public int NumberOfPoints
        {
            get
            {
                return this.numberOfPoints;
            }

            set
            {
                this.numberOfPoints = value;
                this.RaisePropertyChanged("NumberOfPoints");
            }
        }

        public static IEnumerable<Point3D> GeneratePoints(int n, double time)
        {
            const double R = 2;
            const double Q = 0.5;
            for (int i = 0; i < n; i++)
            {
                double t = Math.PI * 2 * i / (n - 1);
                double u = (t * 24) + (time * 5);
                var pt = new Point3D(Math.Cos(t) * (R + (Q * Math.Cos(u))), Math.Sin(t) * (R + (Q * Math.Cos(u))), Q * Math.Sin(u));
                yield return pt;
                if (i > 0 && i < n - 1)
                {
                    yield return pt;
                }
            }
        }

        protected void RaisePropertyChanged(string property)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(property));
            }
        }

        private void OnCompositionTargetRendering(object sender, EventArgs e)
        {
            if (this.ShowLinesVisual3D && this.linesVisual == null)
            {
                this.linesVisual = new LinesVisual3D { Color = Colors.Blue };
                View1.Children.Add(this.linesVisual);
            }

            if (!this.ShowLinesVisual3D && this.linesVisual != null)
            {
                this.linesVisual.IsRendering = false;
                View1.Children.Remove(this.linesVisual);
                this.linesVisual = null;
            }

            if (this.ShowPointsVisual3D && this.pointsVisual == null)
            {
                this.pointsVisual = new PointsVisual3D { Color = Colors.Red, Size = 6 };
                View1.Children.Add(this.pointsVisual);
            }

            if (!this.ShowPointsVisual3D && this.pointsVisual != null)
            {
                this.pointsVisual.IsRendering = false;
                View1.Children.Remove(this.pointsVisual);
                this.pointsVisual = null;
            }

            if (this.ShowScreenSpaceLines3D && this.screenSpaceLines == null)
            {
                this.screenSpaceLines = new ScreenSpaceLines3D { Color = Colors.Green };
                View1.Children.Add(this.screenSpaceLines);
            }

            if (!this.ShowScreenSpaceLines3D && this.screenSpaceLines != null)
            {
                View1.Children.Remove(this.screenSpaceLines);
                this.screenSpaceLines = null;
            }


            if (this.Points == null || this.Points.Count != this.NumberOfPoints)
            {
                this.Points = new Point3DCollection(GeneratePoints(this.NumberOfPoints, this.watch.ElapsedMilliseconds * 0.001));
            }

            if (this.linesVisual != null)
            {
                this.linesVisual.Points = this.Points;
            }

            if (this.pointsVisual != null)
            {
                this.pointsVisual.Points = this.Points;
            }

            if (this.screenSpaceLines != null)
            {
                this.screenSpaceLines.Points = this.Points;
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
