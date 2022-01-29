using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace fractals
{
    public class SerpinskyCarpet : DrawFractalsClass
    {
        public SerpinskyCarpet(int depth) : base(depth)
        {

        }

        public override void DrawFractal(Canvas canvas)
        {
            colors = SetColors();
            if (canvas.Children.Count != 0)
            {
                canvas.Children.Clear();
            }
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
