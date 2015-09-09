using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public enum PointType
    {
        Start, 
        Waypoiny,
        End
    }

    public class Point
    {
        public PointType Type;
        public double X;
        public double Y;
    }
}
