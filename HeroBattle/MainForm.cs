using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

using HeroBattle.PathFind;

namespace HeroBattle
{
    class Form1 : Form
    {
        DateTime datetime;

        Timer timer = new Timer();

        private Room room;

        public Form1()
        {
            this.Size = new Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 150;
            timer.Enabled = true;
            timer.Start();

            datetime = DateTime.Now;

            //
            room = new Room();
            room.Initialize();
        }

        private void Update(object sender, EventArgs e)
        {
            //
            room.Update();

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            room.OnPaint(e);
        }
    }
}
