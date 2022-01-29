using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class KantorMult : DrawFractalsClass
    {
        private double distance;
        private double length = 900;
        
        public KantorMult(int depth, double distance) : base(depth)
        {
            this.distance = distance;
        }

        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            DrawNext(canvas, 0, 100, 100, length);
        }

        private void DrawNext(Canvas canvas, int current, double x, double y, double len)
        {
            if (current == depth)
                return;
            Line line = new Line()
            {
                X1 = x,
                Y1 = y,
                Y2 = y,
                X2 = x + len,
                Stroke = new SolidColorBrush(colors[current]),
                StrokeThickness = 8
            };
            canvas.Children.Add(line);
            double x2 = (line.X1 + 2 * line.X2) / 3;
            DrawNext(canvas, current + 1, x, y + distance, len / 3);
            DrawNext(canvas, current + 1, x2, y + distance, len / 3);
        }
    }
}
