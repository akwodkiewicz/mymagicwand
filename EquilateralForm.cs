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
    public partial class EquilateralForm : ShapeForm
    {
        private int _vertices;
        private double _angle;
        private GraphicsPath _gpath;
        private float _rotationAngle;
        public EquilateralForm(int size = 0, int sides = 0) : base(size)
        {
            _rotationAngle = Program.Rnd(100, 500) / (float)100;
            _vertices = (sides == 0) ? Program.Rnd(3, 8) : sides;
            _angle = 2 * Math.PI / _vertices;
            var pointsList = PreparePoints();
            _gpath = new GraphicsPath();
            _gpath.AddPolygon(pointsList.ToArray());
            Region = new Region(_gpath);
        }
        public override void Rotate()
        {
            var m = new Matrix();
            m.RotateAt(_rotationAngle, Center);
            _gpath.Transform(m);
            Region = new Region(_gpath);
        }
        private List<Point> PreparePoints()
        {
            var pointsList = new List<Point>();
            for (int i = 0; i < _vertices; i++)
            {
                var x = (int) (Width/2 + Width/2 * Math.Sin(i * _angle));
                var y = (int) (Height/2 + Height/2 * Math.Cos(i * _angle));
                pointsList.Add(new Point(x, y));
            }
            return pointsList;
        }
    }
}
