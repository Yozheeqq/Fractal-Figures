using Microsoft.Win32;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace fractals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   
        /// <summary>
        /// Конструктор главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            winHeight.Text = $"{mainWindow.Height}";
            winWidth.Text = $"{mainWindow.Width}";
        }

        /// <summary>
        /// Если меняется размер окна.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void OnWinSizeChanging(object sender, EventArgs args)
        {
            if(CheckWinSizeChanging(winHeight.Text, winWidth.Text, out int h, out int w))
            {
                mainWindow.Height = h;
                mainWindow.Width = w;
            }
        }
        /// <summary>
        /// Сохранение картинки в файл.
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void SaveCanvasToFile(object sender, EventArgs args)
        {
            // Создаем битмапу, где будет храниться картинка
            RenderTargetBitmap render = new RenderTargetBitmap((int)fractalCanvas.Width, (int)fractalCanvas.Height,
                96d, 96d, System.Windows.Media.PixelFormats.Default);
            render.Render(fractalCanvas);
            var crop = new CroppedBitmap(render, new Int32Rect(0, 0, (int)(fractalCanvas.Width / 1.3), (int)(fractalCanvas.Height / 1.3)));
            BitmapEncoder pngEncoder = new PngBitmapEncoder();
            pngEncoder.Frames.Add(BitmapFrame.Create(crop));
            // Выбираем файл, куда сохранять.
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JPG file (*.jpg)|*.jpg|PNG file (*.png)|*.png";
            saveDialog.ShowDialog();
            string path = saveDialog.FileName;
            if(!string.IsNullOrEmpty(path))
            {
                using (var fs = System.IO.File.OpenWrite(path))
                {
                    pngEncoder.Save(fs);
                }
            }
        }
        /// <summary>
        /// Показ дополнительных опций для фрактального дерева
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void SelectFractalChanged(object sender, EventArgs args)
        {
            if (selectFractal.SelectedIndex == 0)
            {
                ShowFractalTreeOptions(sender, args, true);
            }
            else
            {
                ShowFractalTreeOptions(sender, args, false);
            }
        }
        /// <summary>
        /// Изменение отношения отрезков
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void RatioChanged(object sender, EventArgs args)
        {
            ratioText.Content = $"Отношение отрезков: {ratioSlider.Value:f2}";
        }
        /// <summary>
        /// Изменение начального угла
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void StartAngleChanged(object sender, EventArgs args)
        {
            startAngleText.Content = $"Начальный угол: {startAngleSlider.Value:f2}";
        }
        /// <summary>
        /// Изменение следующего угла
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void NextAngleChanged(object sender, EventArgs args)
        {
            nextAngleText.Content = $"Следующий угол: {nextAngleSlider.Value:f2}";
        }
        /// <summary>
        /// Вывод настроек для фрактального дерева
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        /// <param name="isShown">Показывать или нет</param>
        private void ShowFractalTreeOptions(object sender, EventArgs args, bool isShown)
        {
            setOptionsFractalTree.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            ratioText.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            ratioSlider.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            startAngleText.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            startAngleSlider.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            nextAngleText.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
            nextAngleSlider.Visibility = isShown ? Visibility.Visible : Visibility.Hidden;
        }
        /// <summary>
        /// Проверка на корректность размеров окна
        /// </summary>
        /// <param name="heightStr">Высота строка</param>
        /// <param name="widthStr">Ширина строка</param>
        /// <param name="h">Высота int</param>
        /// <param name="w">Ширина int</param>
        /// <returns>false - некорректные данные, true - корректные</returns>
        private bool CheckWinSizeChanging(string heightStr, string widthStr, out int h, out int w)
        {
            int winH = (int)mainWindow.Height;
            int winW = (int)mainWindow.Width;
            if(!int.TryParse(heightStr, out int height) || !int.TryParse(widthStr, out int width) ||
                height < 540 || height > 1080 || width < 960 || width > 1920)
            {
                MessageBox.Show($"Неверный параметры окна.\nШирина лежит в пределах [960;1920]\n" +
                    "Высота лежит в пределах [540;1080] \n" +
                    "Также значения должны быть целыми числами\n" +
                    $"{winH} {winW}");
                h = 0;
                w = 0;
                return false;
            }
            h = height;
            w = width;
            return true;
        }
        /// <summary>
        /// Открывание окна выбора начального цвета
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void OnStartColorSelecting(object sender, EventArgs args)
        {
            SelectColorWindow colorWindow = new SelectColorWindow(true);
            colorWindow.Owner = this;
            colorWindow.Show();
        }
        /// <summary>
        /// Открывание окна выбора конечного цвета
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void OnEndColorSelecting(object sender, EventArgs args)
        {
            SelectColorWindow colorWindow = new SelectColorWindow(false);
            colorWindow.Owner = this;
            colorWindow.Show();
        }
        /// <summary>
        /// Начать отрисовку фракталов
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void StartDrawingClick(object sender, EventArgs args)
        {
            if (CheckCorrectInfo())
            {
                switch (selectFractal.Text)
                {
                    case "Фрактальное дерево":
                        DrawFractalTree(sender, args);
                        break;
                    case "Кривая коха":
                        DrawKochSnowflake(sender, args);
                        break;
                    case "Ковер Серпинского":
                        DrawSerpinskyCarpet(sender, args);
                        break;
                    case "Треугольник Серпинского":
                        DrawSerpinskyTriangle(sender, args);
                        break;
                    case "Множество Кантора":
                        DrawKantorMult(sender, args);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверные данные о фрактале. \nГлубина рекурсии должна быть от 1 до 15.");
            }
        } 
        /// <summary>
        /// Отрисовка фрактального дерева
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void DrawFractalTree(object sender, EventArgs args)
        {
            int depth = int.Parse(recursionDepth.Text);
            FractalTree fractalTree = new FractalTree(depth, ratioSlider.Value, startAngleSlider.Value,
                nextAngleSlider.Value);
            fractalTree.DrawFractal(fractalCanvas);
        }
        /// <summary>
        /// Отрисовка снежинки Коха
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void DrawKochSnowflake(object sender, EventArgs args)
        {
            int depth = int.Parse(recursionDepth.Text);
            KochSnowflake kochSnowflake = new KochSnowflake(depth);
            kochSnowflake.DrawFractal(fractalCanvas);
        }
        /// <summary>
        /// Отрисовка ковера Серпинского
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void DrawSerpinskyCarpet(object sender, EventArgs args)
        {
            int depth = int.Parse(recursionDepth.Text);
            SerpinskyCarpet serpinskyCarpet = new SerpinskyCarpet(depth);
            serpinskyCarpet.DrawFractal(fractalCanvas);
        }
        /// <summary>
        /// Отрисовка треугольника Серпинского
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void DrawSerpinskyTriangle(object sender, EventArgs args)
        {
            int depth = int.Parse(recursionDepth.Text);
            SerpinskyTriangle serpinskyTriangle = new SerpinskyTriangle(depth);
            serpinskyTriangle.DrawFractal(fractalCanvas);
        }
        /// <summary>
        /// Отрисовка Канторого множества
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void DrawKantorMult(object sender, EventArgs args)
        {
            int depth = int.Parse(recursionDepth.Text);
            KantorMult kantorMult = new KantorMult(depth, 40);
            kantorMult.DrawFractal(fractalCanvas);
        }
        /// <summary>
        /// Проверка кореектности данных о отрисоке фрактала
        /// </summary>
        /// <returns></returns>
        private bool CheckCorrectInfo()
        {
            if (selectFractal.Text != null && int.TryParse(recursionDepth.Text, out int depth) &&
                depth >= 1 && depth <= 15)
            {
                return true;
            }   
            else
            {
                return false;
            }
        }
    }
}
