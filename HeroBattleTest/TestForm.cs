using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using HeroBattle;

namespace HeroBattleTest
{
    class TestForm : Form
    {
        Timer timer = new Timer();
        int counter = 0;
        Random random;

        private List<Vehicle> movers;
        private Mover attractor;

        public TestForm()
        {
            this.Size = new System.Drawing.Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 20;
            timer.Enabled = true;
            timer.Start();

            this.random = new Random();
            this.movers = new List<Vehicle>();

            {
                Vehicle mover = new Vehicle();
                mover.position = new Vector(30.0, 30.0);
                mover.mass = 1;
                mover.range = 30;
                mover.brush = Brushes.Blue;
                movers.Add(mover);
            }

            {
                Vehicle mover = new Vehicle();
                mover.position = new Vector(random.Next(0, 500), random.Next(0, 500));
                mover.mass = 1;
                mover.range = 100;
                mover.brush = Brushes.Green;
                movers.Add(mover);
            }

            {
                Vehicle mover = new Vehicle();
                mover.position = new Vector(random.Next(0, 500), random.Next(0, 500));
                mover.mass = 1;
                mover.range = 200;
                mover.brush = Brushes.Aqua;
                movers.Add(mover);
            }


            attractor = new Mover();
            attractor.position = new Vector(250, 250);
            attractor.mass = 20;
            attractor.brush = Brushes.Red;
        }

        private void Update(object sender, EventArgs e)
        {
            foreach(Vehicle mover in movers)
            {
                mover.seek(attractor.position);
                mover.Update();
            }

            if (counter++ > 200)
            {
                attractor.position = new Vector(random.Next(500), random.Next(500));
                counter = 0;
            }

            //
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            attractor.OnPaint(e);
            foreach(Vehicle mover in movers)
            {
                mover.OnPaint(e);
            }
        }
    }
}
