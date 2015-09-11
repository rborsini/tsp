using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tsp
{
    public class TwoOptEngine
    {
        private Point startPoint;
        private Point endPoint;

        public static Route BestRoute;
        public static int ExplorerCounter;
        public static int ExplorerFinishedCounter;

        public Route Solve(List<Point> points)
        {
            BestRoute = null;
            ExplorerCounter = 0;
            ExplorerFinishedCounter = 0;

            this.startPoint = points.First(p => p.Type == PointType.Start);
            this.endPoint = points.First(p => p.Type == PointType.End);

            // remaining points
            List<Point> unvisitedPoints = points.Where(p => p.Type == PointType.Waypoiny).ToList();

            // Start del primo explorer
            Explorer explorer = new Explorer(new List<Point>(), this.startPoint, unvisitedPoints, this.endPoint);

            BestRoute.Result = String.Format("Total: {0}, Finished: {1}, Cost: {2}", ExplorerCounter, ExplorerFinishedCounter, Convert.ToInt32(BestRoute.Cost));
            return BestRoute;
        }


        // Metodo statico chiamato dai singoli explorer al termine dell'esplorazione
        public static void OnFinished(Route route)
        {
            ExplorerFinishedCounter++;
            if(BestRoute == null || route.Cost < BestRoute.Cost)
            {
                BestRoute = route;
            }
        }

    }

    /// <summary>
    /// Classe di esplorazione a partire da un nodo che si può trovare in un qualsiasi punto del cammino
    /// </summary>
    public class Explorer : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        Route route = new Route();


        /// <summary>
        /// Esploratore
        /// </summary>
        /// <param name="visitedPoints">Punti già visistati</param>
        /// <param name="currentPoint">Punto corrente</param>
        /// <param name="reaminingPoints">Punti ancora da visitare</param>
        /// <param name="end">Arrivo (punto finale)</param>
        public Explorer(List<Point> visitedPoints, Point currentPoint, List<Point> reaminingPoints, Point end)
        {
            TwoOptEngine.ExplorerCounter++;
            // Clonazione delle liste per non influire con gli altri explorer
            List<Point> clonedVisitedPoints = Clone(visitedPoints);
            List<Point> clonedReaminingPoints = Clone(reaminingPoints);
            Point clonedCurrentPoint = currentPoint.Clone();

            // aggiunto il punto corrente a quelli già visistati
            clonedVisitedPoints.Add(clonedCurrentPoint);

            double currentCost = Route.CalculateCost(visitedPoints);
            if(TwoOptEngine.BestRoute == null || currentCost < TwoOptEngine.BestRoute.Cost)
            {
                // ricerca dei 2 punti più vicini al punto corrente
                List<Point> nearPoints = GetNearPoints(clonedCurrentPoint, clonedReaminingPoints);

                // se non ho più punti da visitare => sono arrivato!
                if (nearPoints.Count == 0)
                {
                    // creo la route
                    route.Points = clonedVisitedPoints;
                    route.Points.Add(end);

                    // sono arrivato in fondo
                    TwoOptEngine.OnFinished(route);
                    //Dispose();
                }

                // creazione di 2 nuovi esploratori per i 2 punti vicini
                foreach (Point point in nearPoints)
                {
                    clonedReaminingPoints.Remove(point);
                    Explorer expoler = new Explorer(clonedVisitedPoints, point, clonedReaminingPoints, end);
                    clonedReaminingPoints.Add(point);
                }
            }
            //else
            //{
            //    Dispose();
            //}

        }

        List<Point> Clone(List<Point> points)
        {
            List<Point> clonedList = new List<Point>();

            foreach(Point point in points)
            {
                clonedList.Add(point.Clone());
            }

            return clonedList;
        }

        /// <summary>
        /// Struttura di appoggio per la ricerca dei punti più vicini
        /// </summary>
        private class PointDistance
        {
            public Point Point;
            public double Distance;

            public PointDistance()
            {
                this.Point = null;
                this.Distance = double.MaxValue;
            }

            public PointDistance(Point point, double distance)
            {
                this.Point = point;
                this.Distance = distance;
            }
        }

        /// <summary>
        /// Ricerca dei 2 punti più vicini al punto corrente tra i rimanenti
        /// </summary>
        /// <param name="point"></param>
        /// <param name="remainingPoints"></param>
        /// <returns></returns>
        private List<Point> GetNearPoints(Point point, List<Point> remainingPoints)
        {
            // Inizio con 2 punti vuoti a distanza infinita
            List<PointDistance> nearPoints = new List<PointDistance>() { new PointDistance(), new PointDistance() };

            // Ciclo su tutti i punti rimanenti
            for (int i = 0; i < remainingPoints.Count; i++)
            {
                // distanza dal punto riferimento al punto corrente del ciclo
                double currentDistance = point.GetDistance(remainingPoints[i]);
                if (currentDistance < nearPoints.Max(n => n.Distance))
                {
                    nearPoints.Add(new PointDistance(remainingPoints[i], currentDistance));

                    nearPoints = nearPoints.OrderByDescending(n => n.Distance).ToList();
                    nearPoints.RemoveAt(0);
                }
            }

            // Ritorno solo i punti "veri", tolgo quelli a null
            return nearPoints.Where(n => n.Point != null).Select(n => n.Point).ToList();
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

    }

}
