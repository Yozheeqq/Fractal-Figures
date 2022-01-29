using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class FractalTree : DrawFractalsClass
    {
        private double s_angle = 45;
        private double s_ratio = 0.5;
        private double s_startAngle = 0;
        private double s_length = 120;
        

        public FractalTree(int depth, double ratio, double startAngle, double nextAngle) : base(depth)
        {
            s_ratio = ratio;
            s_angle = nextAngle;
            s_startAngle = startAngle;
        }

        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            if(canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            DrawNext(canvas, 0, mainWindow.Width / 4, mainWindow.Height / 2 - 50, s_length, s_startAngle);
        }

        private void DrawNext(Canvas canvas, int current, double x, double y, double length, double angle)
        {
            if (current == depth)
                return;
            double radian = angle * Math.PI / 180;
            double newX = x - length * Math.Sin(radian);
            double newY = y - length * Math.Cos(radian);
            Line line = new Line()
            {
                X1 = x,
                X2 = newX,
                Y1 = y,
                Y2 = newY,
                Stroke = new SolidColorBrush(colors[current]),
                StrokeThickness = 2
            };
            canvas.Children.Add(line);
            DrawNext(canvas, current + 1, newX, newY, length * s_ratio, angle + s_angle);
            DrawNext(canvas, current + 1, newX, newY, length * s_ratio, angle - s_angle);
        }
    }
}
