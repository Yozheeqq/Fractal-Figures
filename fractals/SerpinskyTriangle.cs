using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class SerpinskyTriangle : DrawFractalsClass
    {
        public SerpinskyTriangle(int depth) : base(depth)
        {

        }

        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            Polygon triangle = new Polygon();
            triangle.Points = new PointCollection();
            triangle.Points.Add(new Point(500, 100));
            triangle.Points.Add(new Point(250, 500));
            triangle.Points.Add(new Point(750, 500));
            triangle.Stroke = new SolidColorBrush(colors[0]);
            triangle.StrokeThickness = 2;
            canvas.Children.Add(triangle);
            DrawNext(canvas, 0, triangle);
        }

        private void DrawNext(Canvas canvas, int current, Polygon triangle)
        {
            if (current == depth)
                return;
            Point[] points = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                points[i] = new Point((triangle.Points[i].X + triangle.Points[(i + 1) % 3].X) / 2,
                (triangle.Points[i].Y + triangle.Points[(i+1)%3].Y) / 2);
            }
            Polygon smalTriangle = new Polygon();
            smalTriangle.Points = new PointCollection(points);
            smalTriangle.Stroke = new SolidColorBrush(colors[current + 1]);
            smalTriangle.StrokeThickness = 2;
            canvas.Children.Add(smalTriangle);
            Polygon[] polygons = new Polygon[3];
            for (int i = 0; i < 3; i++)
            {
                polygons[i] = new Polygon();
                polygons[i].Points = new PointCollection(new Point[] 
                    {triangle.Points[(i + 1)%3], points[i], points[(i+1)%3] 
                    });
            }
            for (int i = 0; i < 3; i++)
            {
                DrawNext(canvas, current + 1, polygons[i]);
            }
            
        }
    }
}
