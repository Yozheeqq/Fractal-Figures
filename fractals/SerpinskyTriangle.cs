using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    /// <summary>
    /// Класс для отрисовки треугольника Серпинского
    /// </summary>
    public class SerpinskyTriangle : DrawFractalsClass
    {
        public SerpinskyTriangle(int depth) : base(depth)
        {

        }
        /// <summary>
        /// Отрисовка фрактала
        /// </summary>
        /// <param name="canvas">Холст</param>
        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            // Очистка холста
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            // Создаем начальный треугольник
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
        /// <summary>
        /// Рекурсвиный метод для отрисовки
        /// </summary>
        /// <param name="canvas">Холст</param>
        /// <param name="current">Текущая итерация</param>
        /// <param name="triangle">Треугольник для взаимодействия</param>
        private void DrawNext(Canvas canvas, int current, Polygon triangle)
        {
            if (current == depth)
                return;
            // Центры сторон
            Point[] points = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                points[i] = new Point((triangle.Points[i].X + triangle.Points[(i + 1) % 3].X) / 2,
                (triangle.Points[i].Y + triangle.Points[(i+1)%3].Y) / 2);
            }
            // Маленькие треугольки для дальнейшего взаимодействия
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
