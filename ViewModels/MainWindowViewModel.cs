using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TRP2.Infrastructure.Commands;
using TRP2.Infrastructure.ObservableObjects;
using TRP2.Models;
using TRP2.ViewModels.Base;

namespace TRP2.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private static readonly Random random = new Random();

        public ObservableCollection<FlowInfo> Flows { get; private set; }

        public PlotModel Model { get; private set; }
        public double Time { get; set; }
        public double M { get; set; }
        public double Q { get; set; }
        public int ExpNumber { get; set; }
        public int Intensity { get => Flows[0].Intensity; }
        public int TargetFlowLevel { get => Flows[0].TargetFlowLevel; }
        public double BaseFlowSize
        {
            get
            {
                double answer = 0;
                foreach (var a in Flows)
                    answer += a.BaseFlowSize;
                answer /= Flows.Count;
                return answer;
            }
        }
        public double TargetFlowSize
        {
            get
            {
                double answer = 0;
                foreach (var a in Flows)
                    answer += a.TargetFlowSize;
                answer /= Flows.Count;
                return answer;
            }
        }

        public NotifyString Message { get; set; }

        private FlowInfo Process(int l, int k, double Tk)
        {
            double t = 0.0, T = 0.0;
            var answer = new FlowInfo(k, l);
            while (t < Tk)
            {
                for (int n = 0; n < k; n++)
                {
                    if (t > Tk)
                        return answer;

                    double r = random.NextDouble();
                    T = (-1.0 / l) * Math.Log(r);
                    t += T;
                    answer.BaseFlow.Add(new Models.DataPoint(t));
                }
                answer.TargetFlow.Add(new Models.DataPoint(t));
            }
            return answer;
        }
        public ICommand StartCommand { get; }
        private bool CanStartCommnadExecute(object p) => (M != Q && ExpNumber >= 1 && M >= 0 && Q >= 0);
        private void OnStartCommandExecuted(object p)
        {
            Flows.Clear();
            double lk = 1 / M;
            double k = Math.Ceiling(1 / (Q * Q) / (lk * lk));
            double l = Math.Round(k * lk);
            for (int i = 0; i < ExpNumber; i++)
            {
                var flow = Process((int)Math.Round(l), (int)Math.Round(k), Time);
                Flows.Add(flow);
            }
            ModelSetup((int)Math.Round(k));
            
            var series = new HistogramSeries { XAxisKey = "X", YAxisKey = "Y", StrokeThickness = 0 };
            foreach (var a in Flows.Last().BaseFlow)
                series.Items.Add(new HistogramItem(a.Time - 0.02, a.Time + 0.02, 0.04f, 1));

            foreach (var a in Flows.Last().TargetFlow)
                series.Items.Add(new HistogramItem(a.Time - 0.02, a.Time + 0.02, 0.08f, 2));
            series.FillColor = OxyColors.Automatic;
            Model.Series.Add(series);

            double avT = 0.0f, avB = 0.0f;
            foreach (var a in Flows)
            {
                avT += a.TargetFlowSize;
                avB += a.BaseFlowSize;
            }
            avT /= Flows.Count;
            avB /= Flows.Count;
            Flows.Add(new FlowInfo((int)Math.Round(k), (int)Math.Round(l), (int)Math.Round(avT), (int)Math.Round(avB)));
            Model.InvalidatePlot(true);

        }

        private void ModelSetup(int k)
        {
            Model.Legends.Clear();
            Model.Axes.Clear();
            Model.Series.Clear();
            Model.Legends.Add(new Legend());
            Model.Axes.Add(new LinearAxis { Key = "X", Position = AxisPosition.Bottom, Minimum = 0, Maximum = Time + Time * 0.01 });
            Model.Axes.Add(new LinearAxis { Key = "Y", Position = AxisPosition.Left, Minimum = 0, Maximum = 2.01 });
        }
        public MainWindowViewModel()
        {
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommnadExecute);
            M = 1.5;
            Q = 0.5;
            Time = 100;
            ExpNumber = 1;
            Model = new PlotModel { Title = "" };
            ModelSetup(10);
            Flows = new ObservableCollection<FlowInfo>();
            Flows.Add(new FlowInfo(0, 0));

        }
    }
}
