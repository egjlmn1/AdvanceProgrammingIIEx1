using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex1.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {

        private bool can_move = false;
        private double out_bound = 70;

        public Joystick()
        {
            InitializeComponent();
        }

        public Point GetRatio()
        {
            double Xratio = knobPosition.X / out_bound;
            double Yratio = -knobPosition.Y / out_bound;
            return new Point(Xratio, Yratio);
        }

        void NotifiyMoved()
        {

        }

        public void centerKnob_Completed(object sender, EventArgs e)
        {
            StopAnimation();
            MoveTo(0, 0);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if ((e.LeftButton == MouseButtonState.Pressed) && (!can_move))
            {
                StopAnimation();
                can_move = true;
            }
        }

        public bool Drag_Knob(double x, double y)
        {
            if (!can_move) { return false; }
            // position now centered in 0, 0
            if (Math.Pow(x, 2) + Math.Pow(y, 2) > Math.Pow(out_bound, 2))
            {
                FixPosition(ref x, ref y);
            }
            if ((x != knobPosition.X) || (y != knobPosition.Y))
            {
                MoveTo(x, y);
                return true;
            }
            return false;
        }

        private void MoveTo(double x, double y)
        {
            knobPosition.X = x;
            knobPosition.Y = y;
            NotifiyMoved();
        }

        public void Leave_Knob()
        {
            if (can_move)
            {
                Storyboard sb = Knob.FindResource("CenterKnob") as Storyboard;
                sb.Begin(this, true);
                can_move = false;
            }
        }

        private void StopAnimation()
        {
            double x = knobPosition.X;
            double y = knobPosition.Y;
            Storyboard sb = Knob.FindResource("CenterKnob") as Storyboard;
            sb.Stop(this);
            MoveTo(x, y);
        }

        private void FixPosition(ref double x, ref double y)
        {
            double radius = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            double delta_radius = radius - out_bound;
            double dy = delta_radius * y / radius;
            y -= dy;
            double dx = delta_radius * x / radius;
            x -= dx;
        }
    }
}
