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
    /// Interaction logic for SelectColorWindow.xaml
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
