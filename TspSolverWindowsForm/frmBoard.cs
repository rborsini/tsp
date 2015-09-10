using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsp;

namespace TspSolverWindowsForm
{
    public partial class frmBoard : Form
    {
        public frmBoard()
        {
            InitializeComponent();

            this.formGraphics = pnlBoard.CreateGraphics();

        }

        private List<Tsp.Point> points = new List<Tsp.Point>();
        private System.Drawing.Graphics formGraphics;

        private void btnRandom_Click(object sender, EventArgs e)
        {
            Clear();
            Random r = new Random();

            for(int i = 0; i < Convert.ToInt32(this.numericUpDown.Value); i++)
            {
                Tsp.Point point = new Tsp.Point();

                point.Name = (i + 1).ToString();
                point.X = Convert.ToInt32(r.NextDouble() * 500);
                point.Y = Convert.ToInt32(r.NextDouble() * 500);

                AddPoint(point);
            }

        }

        private void pnlBoard_Click(object sender, EventArgs e)
        {
            Tsp.Point point = new Tsp.Point();
            MouseEventArgs arg = (MouseEventArgs)e;

            point.Name = (this.points.Count + 1).ToString();
            point.X = Convert.ToInt32(arg.X);
            point.Y = Convert.ToInt32(arg.Y);

            AddPoint(point);
        }

        private void AddPoint(Tsp.Point point)
        {
            if (this.points.Count == 0)
            {
                point.Type = Tsp.PointType.Start;
            }
            else
            {
                point.Type = Tsp.PointType.End;

                if (this.points.Count > 1)
                {
                    this.points.Last().Type = Tsp.PointType.Waypoiny;
                    DrawPoint(this.points.Last());
                }
            }

            this.points.Add(point);
            DrawPoint(point);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            this.points.Clear();
            this.lblResult.Text = "";
            this.formGraphics.Clear(System.Drawing.Color.White);
        }

        private void btnShowRoute_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < this.points.Count -1; i++)
            {
                DrawLine(this.points[i], this.points[i + 1]);
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            this.formGraphics.Clear(System.Drawing.Color.White);
            Route route = new Engine().Solve(this.points);

            this.lblResult.Text = route.Result;

            for (int i = 0; i < route.Points.Count - 1; i++)
            {
                DrawPoint(route.Points[i]);
                DrawLine(route.Points[i], route.Points[i + 1]);
            }

            DrawPoint(route.Points[route.Points.Count - 1]);

        }

        private void DrawPoint(Tsp.Point point)
        {
            System.Drawing.Pen myPen;
            Color color = System.Drawing.Color.Red;
            switch(point.Type)
            {
                case Tsp.PointType.Start:
                    color = System.Drawing.Color.Cyan;
                    break;
                case Tsp.PointType.Waypoiny:
                    color = System.Drawing.Color.Gray;
                    break;
                case Tsp.PointType.End:
                    color = System.Drawing.Color.Red;
                    break;
            }

            myPen = new System.Drawing.Pen(color);
            
            int x1 = ((int)point.X-2);
            int y1 = ((int)point.Y - 2);

            formGraphics.DrawRectangle(myPen, x1, y1, 7, 7);
            formGraphics.DrawRectangle(myPen, x1, y1, 6, 6);
            formGraphics.DrawRectangle(myPen, x1, y1, 5, 5);
            formGraphics.DrawRectangle(myPen, x1, y1, 4, 4);
            formGraphics.DrawRectangle(myPen, x1, y1, 3, 3);
            formGraphics.DrawRectangle(myPen, x1, y1, 2, 2);

        }

        private void DrawLine(Tsp.Point origin, Tsp.Point destination)
        {
            System.Drawing.Pen myPen;
            myPen = new System.Drawing.Pen(System.Drawing.Color.Black);


            int x1 = ((int)origin.X);
            int y1 = ((int)origin.Y);
            int x2 = ((int)destination.X);
            int y2 = ((int)destination.Y);

            formGraphics.DrawLine(myPen, x1, y1, x2, y2);
        }



    }
}
