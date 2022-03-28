using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRP2.Models
{
    class FlowInfo
    {
        public List<DataPoint> TargetFlow { get; private set; }
        public List<DataPoint> BaseFlow { get; private set; }

        public int TargetFlowLevel { get; private set; }

        public int Intensity { get; private set; }

        public int TargetFlowSize => TargetFlow.Count;

        public int BaseFlowSize => BaseFlow.Count;

        public FlowInfo(int level, int intens) {
            TargetFlowLevel = level;
            Intensity = intens;
            TargetFlow = new List<DataPoint>();
            BaseFlow = new List<DataPoint>();
        }

    }
}
