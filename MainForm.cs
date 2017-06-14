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
        private bool _triangleMode = false;
        private bool _fullOpacity = false;
        private int _size;
        public MainForm()
        {
            InitializeComponent();
            Visible = false;
            _spawnLeaps = (_spawnTime / _numOfShapes) / _timerInterval;
            _spawnCounter = _spawnLeaps * _numOfShapes;
            //_closeCounter = _programClosingTime / _timerInterval;
            _size = 0;
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
                if (_triangleMode)
                    shape = new EquilateralForm(_size, 3);
                else
                {
                    if (Program.Rnd(0, 61) < 10)
                        shape = new CircleForm(_size);
                    else
                        shape = new EquilateralForm(_size);
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
                if (!_fullOpacity)
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
                var colorpicker = new ColorDialog();
                if (colorpicker.ShowDialog() == DialogResult.OK)
                    foreach (var sh in _shapeList)
                        sh.BackColor = colorpicker.Color;
            }
            else
            {
                colorModeToolStripMenuItem.Checked = false;
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
            colorModeToolStripMenuItem.Checked = false;
            timer1.Start();
        }

        private void trianglesOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _triangleMode = !_triangleMode;
            if (_triangleMode)
                trianglesOnlyToolStripMenuItem.Checked = true;
            else
                trianglesOnlyToolStripMenuItem.Checked = false;
            Restart();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _size = 40;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            toolStripMenuItem2.Checked = true;
            Restart();

        }
        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _size = 60;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            toolStripMenuItem3.Checked = true;
            Restart();

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _size = 80;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            toolStripMenuItem4.Checked = true;
            Restart();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            _size = 100;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            toolStripMenuItem5.Checked = true;
            Restart();

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            _size = 120;
            foreach (var strip in sizeToolStripMenuItem.DropDownItems)
                (strip as ToolStripMenuItem).Checked = false;

            toolStripMenuItem6.Checked = true;
            Restart();

        }

        private void aboutToolStripItem_Click(object sender, EventArgs e)
        {
            var about = new AboutForm();
            about.ShowDialog();
        }

        private void fullOpacityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _fullOpacity = !_fullOpacity;
            if (_fullOpacity)
                fullOpacityToolStripMenuItem.Checked = true;
            else
                fullOpacityToolStripMenuItem.Checked = false;
            Restart();
        }
    }
}
