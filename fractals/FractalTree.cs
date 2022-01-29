using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class FractalTree : DrawFractalsClass
    {
        // Следующий угол
        private double s_angle = 45;
        // Отношение отрезков
        private double s_ratio = 0.5;
        // Начальный угол
        private double s_startAngle = 0;
        // Длина первого отрезка
        private double s_length = 120;
        
        /// <summary>
        /// Конструктор для параметров
        /// </summary>
        /// <param name="depth">Глубина рекурсии</param>
        /// <param name="ratio">Отношение отрезков</param>
        /// <param name="startAngle">Начальный угол</param>
        /// <param name="nextAngle">Следующий угол</param>
        public FractalTree(int depth, double ratio, double startAngle, double nextAngle) : base(depth)
        {
            s_ratio = ratio;
            s_angle = nextAngle;
            s_startAngle = startAngle;
        }
        /// <summary>
        /// Отрисовка фрактала
        /// </summary>
        /// <param name="canvas">Холст</param>
        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            // Очистка холста
            if(canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
            DrawNext(canvas, 0, mainWindow.Width / 4, mainWindow.Height / 2 - 50, s_length, s_startAngle);
        }
        /// <summary>
        /// Рекурсивный метод для отрисовки
        /// </summary>
        /// <param name="canvas">Холст</param>
        /// <param name="current">Нынешняя итерация</param>
        /// <param name="x">Координаты начала отрезка по Х</param>
        /// <param name="y">Координаты начала отрезка по У</param>
        /// <param name="length">Длина отрезка</param>
        /// <param name="angle">Угол поворота</param>
        private void DrawNext(Canvas canvas, int current, double x, double y, double length, double angle)
        {
            // Выход из рекурсии
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
