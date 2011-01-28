using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fractal
{
    [Serializable]
    public class SectorInfo
    {
        public Guid Id { get; set; }
        public int FromX { get; set; }
        public int FromY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double RealMinimum { get; set; }
        public double ImgMinimum { get; set; }
        public double Delta { get; set; }
        public int MaxIterations { get; set; }
        public int MaxValue { get; set; }

        public SectorInfo Clone()
        {
            return (SectorInfo) this.MemberwiseClone();
        }
    }
}


