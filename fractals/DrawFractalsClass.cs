using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace fractals
{
    /// <summary>
    /// Базовый класс для отрисовки фракталов
    /// </summary>
    public class DrawFractalsClass : MainWindow
    {
        public static Color startColor = Color.FromRgb(0, 0, 0);
        public static Color endColor = Color.FromRgb(0, 0, 0);
        internal List<Color> colors;
        // Глубина рекурсии
        internal int depth;
        
        public DrawFractalsClass(int depth)
        {
            this.depth = depth; 
        }
        /// <summary>
        /// Виртуальный метод для отрисовки фрактала
        /// </summary>
        /// <param name="canvas">Холст</param>
        public virtual void DrawFractal(Canvas canvas)
        {

        }
        /// <summary>
        /// Образование градиента с помощью выбранных цветов
        /// </summary>
        /// <returns>Список цветов</returns>
        internal List<Color> SetColors()
        {
            int rMin = startColor.R;
            int rMax = endColor.R;
            int gMin = startColor.G;
            int gMax = endColor.G;
            int bMin = startColor.B;
            int bMax = endColor.B;
            var colorList = new List<Color>();
            for (int i = 0; i <= depth; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * i / depth);
                var gAverage = gMin + (int)((gMax - gMin) * i / depth);
                var bAverage = bMin + (int)((bMax - bMin) * i / depth);
                colorList.Add(Color.FromRgb((byte)rAverage, (byte)gAverage, (byte)bAverage));
            }
            return colorList;
        }
    }
}
