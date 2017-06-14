using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyMagicWand
{
    public class Settings
    {
        public bool OneColorMode { get; set; }
        public Color OneColor { get; set; }
        public bool OneShapeMode { get; set; }
        public int ShapeSides { get; set; }
        public int ShapeSize { get; set; }
        public bool FullOpacityMode { get; set; }
        public Settings()
        {
            OneColorMode = false;
            OneShapeMode = false;
            ShapeSize = 40;
            FullOpacityMode = false;
        }
    }
}
