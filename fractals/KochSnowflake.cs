using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class KochSnowflake : DrawFractalsClass
    {
        private double s_length = 620;
        
        public KochSnowflake(int depth) : base(depth)
        {

        }

        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            Line line = new Line()
            {
                X1 = mainWindow.Width / 4 - s_length / 2 - 50,
                X2 = mainWindow.Width / 4 + s_length / 2 + 100,
                Y1 = mainWindow.Height / 2,
                Y2 = mainWindow.Height / 2,
                Stroke = new SolidColorBrush(colors[0]),
            };
            canvas.Children.Add(line);
            DrawNext(canvas, 0, new Point(line.X1, line.Y1), new Point(line.X2, line.Y2));
        }

        private void DrawNext(Canvas canvas, int current, Point p1, Point p2)
        {
            if (current == depth)
                return;
            var p3 = new Point((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
            var p4 = new Point((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
            var p5 = new Point(p3.X + (p4.X - p3.X) * 0.5 + (p4.Y - p3.Y) * Math.Sqrt(3)/2,
                p3.Y - (p4.X - p3.X) * Math.Sqrt(3) / 2 + (p4.Y - p3.Y) * 0.5);
            Line line1 = new Line()
            {
                X1 = p3.X,
                X2 = p5.X,
                Y1 = p3.Y,
                Y2 = p5.Y,
                Stroke = new SolidColorBrush(colors[current]),
            };
            Line line2 = new Line()
            {
                X1 = p4.X,
                X2 = p5.X,
                Y1 = p4.Y,
                Y2 = p5.Y,
                Stroke = new SolidColorBrush(colors[current]),
            };
            Line line3 = new Line()
            {
                X1 = p3.X,
                X2 = p4.X,
                Y1 = p3.Y,
                Y2 = p4.Y,
                Stroke = canvas.Background
            };
            canvas.Children.Add(line1);
            canvas.Children.Add(line2);
            canvas.Children.Add(line3);
            DrawNext(canvas, current + 1, p1, p3);
            DrawNext(canvas, current + 1, p4, p2);
            DrawNext(canvas, current + 1, p3, p5);
            DrawNext(canvas, current + 1, p5, p4);
        }
    }
}
