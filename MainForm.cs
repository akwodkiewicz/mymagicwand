using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMagicWand
{
    public partial class MainForm : Form
    {
        private const int _timerInterval = 20;
        private const int _numOfShapes = 20;
        private const int _spawnTime = 2000;
        //private const int _programClosingTime = 5000;
        private int _spawnCounter;
        //private int _closeCounter;
        private int _spawnLeaps;
        private List<ShapeForm> _shapeList = new List<ShapeForm>();
        private Settings _settings;
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
            _settings = new Settings();
            _spawnLeaps = (_spawnTime / _numOfShapes) / _timerInterval;
            _spawnCounter = _spawnLeaps * _numOfShapes;
            //_closeCounter = _programClosingTime / _timerInterval;
            //_size = 0;
            timer1.Interval = _timerInterval;
            timer1.Start();
        }
        private void OnTick(object sender, EventArgs e)
        {
            _spawnCounter--;
            //_closeCounter--;
            if (_spawnCounter % _spawnLeaps == 0 && _spawnCounter >= 0)
            {
                ShapeForm shape;
                if (_settings.OneShapeMode && _settings.ShapeSides != 0)
                    shape = new EquilateralForm(_settings);
                else if (_settings.OneShapeMode)
                    shape = new CircleForm(_settings);
                else
                {
                    if (Program.Rnd(0, 61) < 10)
                        shape = new CircleForm(_settings);
                    else
                        shape = new EquilateralForm(_settings);
                }
                _shapeList.Add(shape);
                shape.Show();
            }
            //if (_closeCounter <= 0)
            //{
            //    var shape = _shapeList.First();
            //    _shapeList.Remove(shape);
            //    shape.Close();

            //    if (_shapeList.Count == 0)
            //    {
            //        timer1.Stop();
            //        Close();
            //        Application.Exit();
            //    }
            //}
            Animate();
        }
        private void Animate()
        {
            foreach (var shape in _shapeList)
            {
                if (shape.IsDisposed || shape.Disposing)
                    continue;
                shape.MoveForm();
                if (!_settings.FullOpacityMode)
                    shape.ChangeOpacity();
                shape.Rotate();
            }
        }

        private void oneColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetColors();
        }
        private void SetColors()
        {
            timer1.Stop();
            if (!colorModeToolStripMenuItem.Checked)
            {
                colorModeToolStripMenuItem.Checked = true;
                _settings.OneColorMode = true;
                var colorpicker = new ColorDialog();
                if (colorpicker.ShowDialog() == DialogResult.OK)
                    foreach (var sh in _shapeList)
                        sh.BackColor = colorpicker.Color;
                _settings.OneColor = colorpicker.Color;
            }
            else
            {
                colorModeToolStripMenuItem.Checked = false;
                _settings.OneColorMode = false;
                foreach (var sh in _shapeList)
                    sh.BackColor = Color.FromArgb(
                                Program.Rnd(0, 256),
                                Program.Rnd(0, 256),
                                Program.Rnd(0, 256)
                                );
            }
            timer1.Start();
        }
        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                pauseToolStripMenuItem.Text = "Resume";
            }
            else
            {
                timer1.Start();
                pauseToolStripMenuItem.Text = "Pause";
            }
        }

        private void CloseMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            Close();
            Application.Exit();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Restart();
        }
        private void Restart()
        {
            timer1.Stop();
            foreach (var sh in _shapeList)
                sh.Close();
            _shapeList.Clear();
            _spawnCounter = _spawnLeaps * _numOfShapes;
            //_closeCounter = _programClosingTime / _timerInterval;
            pauseToolStripMenuItem.Text = "Pause";
            timer1.Start();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SetSize(40, 0);
        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SetSize(60, 1);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SetSize(80, 2);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            SetSize(100, 3);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            SetSize(120, 4);
        }
        private void SetSize(int size, int index)
        {
            _settings.ShapeSize = size;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            (sizeToolStripMenuItem.DropDownItems[index] as ToolStripMenuItem).Checked = true;
            Restart();
        }
        private void aboutToolStripItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void fullOpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _settings.FullOpacityMode = !_settings.FullOpacityMode;
            if (_settings.FullOpacityMode)
            {
                fullOpacityToolStripMenuItem.Checked = true;
                foreach (var sh in _shapeList)
                    sh.Opacity = 1;
            }
            else
                fullOpacityToolStripMenuItem.Checked = false;
        }
        private void SetSides(int sides, int index)
        {
            _settings.ShapeSides = sides;
            _settings.OneShapeMode = (index == 0) ? false : true;
            foreach (var strip in oneShapeModeToolstripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;
            (oneShapeModeToolstripMenuItem.DropDownItems[index] as ToolStripMenuItem).Checked = true;
            Restart();
        }
        private void allShapesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(0, 0);
        }
        private void circleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(0, 1);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(3, 2);
        }

        private void squareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(4, 3);
        }

        private void pentagonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(5, 4);
        }

        private void hexagonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetSides(6, 5);
        }
    }
}
