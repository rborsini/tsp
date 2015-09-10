using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public class Route
    {
        public List<Point> Points;

        public double Cost
        {
            get
            {
                double cost = 0;
                for(int i = 0; i < this.Points.Count - 1; i++)
                {
                    cost += this.Points[i].GetDistance(this.Points[i + 1]);
                }

                return cost;
            }
        }

        public string Result;

        public Route()
        {
            this.Points = new List<Point>();
        }

    }
}
