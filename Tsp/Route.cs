using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public class Route
    {
        private List<Point> points;
        private double cost = double.MaxValue;

        public List<Point> Points
        {
            get { return this.points; }
            set 
            {
                this.points = value;
                this.cost = CalculateCost(value);
            }
        }

        public double Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }

        public string Result;

        public Route()
        {
            this.Points = new List<Point>();
        }

        public static double CalculateCost(List<Point> points)
        {
            double c = 0;
            for (int i = 0; i < points.Count - 1; i++)
            {
                c += points[i].GetDistance(points[i + 1]);
            }

            return c;
        }

    }
}
