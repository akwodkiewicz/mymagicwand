using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMagicWand
{
    public partial class ShapeForm : Form
    {
        public int XVel { get; set; }
        public int YVel { get; set; }
        public double OpacityVel { get; set; }
        public Point Center { get; set; }

        public ShapeForm(int size = 0)
        {
            InitializeComponent();
            Size = (size == 0) ? new Size(40, 40) : new Size(size, size);
            Height = Size.Height;
            Width = Size.Width;
            BackColor = Color.FromArgb(Program.Rnd(0, 256),
                               Program.Rnd(0, 256),
                               Program.Rnd(0, 256));
            Location = new Point(Cursor.Position.X - Width / 2,
                                 Cursor.Position.Y - Height / 2);
            XVel = Program.Rnd(-5, 6);
            YVel = Program.Rnd(-5, 6);
            OpacityVel = Program.Rnd(1, 9) / 100.0;
            Center = new Point(Width / 2, Height / 2);
        }
        public void ChangeOpacity()
        {
            if (Opacity <= 0 || Opacity >= 1)
                OpacityVel *= -1;
            Opacity += OpacityVel;
        }
        public void MoveForm()
        {
            var p = Location;
            var screenWidth = Screen.PrimaryScreen.Bounds.Width;
            var screenHeight = Screen.PrimaryScreen.Bounds.Height;
            if ((p.X < 0 && XVel < 0) || (p.X + Width/2 > screenWidth && XVel > 0))
            {
                XVel *= -1;
            }
            if ((p.Y < 0 && YVel < 0) || (p.Y + Height/2 > screenHeight && YVel > 0))
            {
                YVel *= -1;
            }
            p.Offset(XVel, YVel);
            Location = p;
        }
        virtual public void Rotate() { }
    }
}
