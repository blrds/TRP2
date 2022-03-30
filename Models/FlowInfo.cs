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

        public int TargetFlowSize => TargetFlow.Count==0?_targetFlowSize:TargetFlow.Count;

        public int BaseFlowSize => BaseFlow.Count==0?_baseFlowSize:BaseFlow.Count;

        private int _targetFlowSize = 0;
        private int _baseFlowSize = 0;
        public FlowInfo(int level, int intens) {
            TargetFlowLevel = level;
            Intensity = intens;
            TargetFlow = new List<DataPoint>();
            BaseFlow = new List<DataPoint>();
        }

        public FlowInfo(int level, int intens, int tSize, int bSize)
        {
            TargetFlowLevel = level;
            Intensity = intens;
            TargetFlow = new List<DataPoint>();
            BaseFlow = new List<DataPoint>();
            _targetFlowSize = tSize;
            _baseFlowSize = bSize;
        }
    }
}
