using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public class Engine
    {

        public Route Solve(List<Point> points)
        {
            return new Route() { Points = points };
        }

    }
}
