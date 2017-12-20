using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ConsoleApp1
{
    class Form1 : Form
    {
        DateTime datetime;
        float time3 = 0, height = 20, width = 20;
        PointF location = new PointF(0, 0);
        PointF velocity = new PointF(50, 50);

        Timer timer = new Timer();

        public Form1()
        {
            this.Size = new Size(500, 500);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Start();

            datetime = DateTime.Now;
        }

        private void Update(object sender, EventArgs e)
        {
            location.X = velocity.X * (time3 / 1000);
            location.Y = velocity.Y * (time3 / 1000);

            time3 += 200;// (DateTime.Now.Ticks - dt.Ticks)/10000;

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    e.Graphics.DrawRectangle(Pens.Black, 
                        new Rectangle(new Point(i*50, j*50), new Size(50, 50)));

            e.Graphics.FillEllipse(Brushes.Tomato, 
                new Rectangle((int)location.X, (int)location.Y, (int)height, (int)width));
        }
    }
}
