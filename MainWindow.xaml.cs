using Ex1.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MovingMouseEvent(object sender, MouseEventArgs e)
        {
            Joystick js = Knob as Joystick;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(js as Control).X - (js as Control).RenderSize.Width / 2;
                double y = e.GetPosition(js as Control).Y - (js as Control).RenderSize.Height / 2;
                js.Drag_Knob(x, y);
            }
            else
            {
                js.Leave_Knob();
            }
            debug.Content = js.GetRatio().X + ", " + js.GetRatio().Y;
        }

    }
}
