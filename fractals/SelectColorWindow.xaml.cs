using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace fractals
{
    /// <summary>
    /// Окно для выбора цвета градиента
    /// </summary>
    public partial class SelectColorWindow : Window
    {
        // true - начальный цвет, false - конечный
        bool flag;
        
        public SelectColorWindow(bool flag)
        {
            InitializeComponent();
            this.flag = flag;
        }
        /// <summary>
        /// Нажатие на кнопку сохранения цвета
        /// </summary>
        /// <param name="sender">Объект</param>
        /// <param name="args">Параметры</param>
        private void SelectColorClick(object sender, EventArgs args)
        {
            if (flag)
            {
                DrawFractalsClass.startColor = colorPicker.SelectedColor;
            }
            else
            {
                DrawFractalsClass.endColor = colorPicker.SelectedColor;
            }
        }
    }
}
