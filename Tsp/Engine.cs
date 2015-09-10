using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public class Engine
    {

        private Point startPoint;
        private Point endPoint;

        public Route Solve(List<Point> points)
        {
            this.startPoint = points.First(p => p.Type == PointType.Start);
            this.endPoint = points.First(p => p.Type == PointType.End);

            // init route
            Route route = new Route();
            route.Points.Add(this.startPoint);

            // remaining points
            List<Point> unvisitedPoints = points.Where(p => p.Type == PointType.Waypoiny).ToList();
            
            // loop nodes
            Point nextPoint = this.startPoint;
            while(nextPoint != null)
            {
                nextPoint = GetNearPoint(nextPoint, unvisitedPoints);

                if(nextPoint != null)
                {
                    route.Points.Add(nextPoint);
                    unvisitedPoints.Remove(nextPoint);
                }
            }

            route.Points.Add(this.endPoint);
            route.Result = String.Join(" - ", route.Points.Select(p => p.Name));

            return route;
        }

        private Point GetNearPoint(Point point, List<Point> remainingPoints)
        {
            Point nearPoint = null;
            double bestDistance = double.MaxValue;

            for(int i = 0; i < remainingPoints.Count; i++)
            {
                double currentDistance = point.GetDistance(remainingPoints[i]);
                if(currentDistance < bestDistance)
                {
                    nearPoint = remainingPoints[i];
                    bestDistance = currentDistance;
                }
            }

            return nearPoint;
        }

        private class Explorer
        {



        }

    }
}
