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
    public partial class CircleForm : ShapeForm
    {
        public CircleForm(Settings settings) : base(settings)
        {
            var graphicspath = new GraphicsPath();
            graphicspath.AddEllipse(new Rectangle(0, 0, Width, Height));
            Region = new Region(graphicspath);
        }
    }
}
