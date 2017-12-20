using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroBattle
{
    class Hero
    {
        float height = 20, width = 20;

        public Point position { get; set; }

        public Point startPos { get; set; }
        public Point endPos { get; set; }

        public Hero()
        {

        }

        public void SetUp()
        {

        }

        public void Update(long delta)
        {

        }

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Tomato,
                new Rectangle((int)position.X * 50, (int)position.Y * 50, (int)height, (int)width));
        }
    }
}
