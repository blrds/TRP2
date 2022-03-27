using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public double Time { get; set; }
        public double M { get; set; }
        public double Q { get; set; }
        public int ExpNumber { get; set; }

        public NotifyString Message { get; set; }

        private List<DataPoint> Process(double l, double k, double Tk)
        {
            double t = 0, T = 0;
            var answer = new List<DataPoint>();
            do {
                for (int n = 0; n < k; n++) {
                    double r = random.NextDouble();
                    T = -1 / l * Math.Log(r);
                    t += T;
                }
                answer.Add(new DataPoint(t));
            } while (t < Tk);
            return answer;
        }
        public ICommand StartCommand { get; }
        private bool CanStartCommnadExecute(object p) => (M!=Q && ExpNumber>=1);
        private void OnStartCommandExecuted(object p)
        {
            double lk = 1 / M;
            double k = 1 / (Q * Q) / (lk * lk);
            double l = k * lk;
            for (int i = 0; i < ExpNumber; i++) {
                var list = Process(l, k, Time);
            }
        }
        public MainWindowViewModel() {
            StartCommand = new LambdaCommand(OnStartCommandExecuted, CanStartCommnadExecute);
            M = 0;
            Q = 0;
            Time = 0;
            ExpNumber = 0;
        }
    }
}
