using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    /// <summary>
    /// Класс для отрисовки ковера Серпинского
    /// </summary>
    public class SerpinskyCarpet : DrawFractalsClass
    {
        public SerpinskyCarpet(int depth) : base(depth)
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
            // Начальный квадрат
            Rectangle rectangle = new Rectangle()
            {
                Width = 500,
                Height = 500,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(rectangle, 250);
            Canvas.SetTop(rectangle, 100);
            canvas.Children.Add(rectangle);
            DrawNext(canvas, 0, rectangle);
        }
        /// <summary>
        /// Рекурсивный метод для отрисовки
        /// </summary>
        /// <param name="canvas">Холст</param>
        /// <param name="current">Нынешняя итерация</param>
        /// <param name="rect">Квадрат для взаимодействия</param>
        private void DrawNext(Canvas canvas, int current, Rectangle rect)
        {
            if (current == depth)
            {
                return;
            }
            List<Rectangle> rectangles = GetRectangles(rect, current);
            for (int i = 0; i < 9; i++)
            {
                canvas.Children.Add(rectangles[i]);
                if(i != 4)
                    DrawNext(canvas, current + 1, rectangles[i]);
            }
        }
        /// <summary>
        /// Создание 9-ти квадратов для дальнейшего использования
        /// </summary>
        /// <param name="rect">Текущий квадрат</param>
        /// <param name="current">Нынешняя итерация</param>
        /// <returns></returns>
        private List<Rectangle> GetRectangles(Rectangle rect, int current)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            double x = Canvas.GetLeft(rect);
            double y = Canvas.GetTop(rect);
            for (int i = 0; i < 9; i++)
            {
                Rectangle temp = new Rectangle()
                {
                    Width = rect.Width / 3,
                    Height = rect.Height / 3,
                    // Центральный квадрат красим в прозрачный цвет
                    Fill = i == 4 ? canvas.Background : new SolidColorBrush(colors[current]),
                };
                
                Canvas.SetLeft(temp, (i % 3) * temp.Height + x);
                Canvas.SetTop(temp, (i / 3) * temp.Width + y);
                rectangles.Add(temp);
            }
            return rectangles;
        }
    }
}
