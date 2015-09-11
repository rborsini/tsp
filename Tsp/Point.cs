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
        public string Name;

        public double GetDistance(Point p)
        {
            return GetDistance(this, p);
        }

        public static double GetDistance(Point origin, Point destination)
        {
            return Math.Sqrt(Math.Pow(destination.X - origin.X, 2) + Math.Pow(destination.Y - origin.Y, 2));
        }

        public override string ToString()
        {
            return this.Name;
        }

        public Point Clone()
        {
            Point clonedPoint = new Point();

            clonedPoint.Name = this.Name;
            clonedPoint.Type = this.Type;
            clonedPoint.X = this.X;
            clonedPoint.Y = this.Y;

            return clonedPoint;
        }

    }
}
