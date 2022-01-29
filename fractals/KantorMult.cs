using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    /// <summary>
    /// Класс для отрисовки Канторого множества
    /// </summary>
    public class KantorMult : DrawFractalsClass
    {
        // Расстояние между строками
        private double distance;
        private double length = 900;
        /// <summary>
        /// Конструктор для параметров
        /// </summary>
        /// <param name="depth">Глубина рекурсии</param>
        /// <param name="distance">Расстояние между строками</param>
        public KantorMult(int depth, double distance) : base(depth)
        {
            this.distance = distance;
        }

        /// <summary>
        /// Отрисовка фракталов
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
            DrawNext(canvas, 0, 100, 100, length);
        }
        /// <summary>
        /// Рекурсивный метод для отрисовки
        /// </summary>
        /// <param name="canvas">Холст</param>
        /// <param name="current">Нынешняя итерация</param>
        /// <param name="x">Координаты начала отрезка по Х</param>
        /// <param name="y">Координаты начала отрезка по У</param>
        /// <param name="len">Длина отрезка</param>
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
