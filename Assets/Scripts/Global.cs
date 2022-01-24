using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global {
    [Serializable]
    public struct RangedFloat {
        public float minValue;
        public float maxValue;
    }

    public class MinMaxRangeAttribute : Attribute {
        public MinMaxRangeAttribute(float min, float max)
        {
            Min = min;
            Max = max;
        }
        public float Min { get; private set; }
        public float Max { get; private set; }
    }

} //Global end
