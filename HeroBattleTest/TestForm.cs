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

namespace HeroBattleTest
{
    using Point = System.Drawing.Point;

    class Mover
    {
        public Vector position;
        public Vector velocity { get; private set; }
        public Vector acceleration { get; private set; }
        public float mass;
        public Brush brush;

        public Mover()
        {
            velocity = new Vector(0, 0);
            acceleration = new Vector(0.0, 0.0);
        }

        public void applyForce(Vector force)
        {
            Vector f = Vector.Divide(force, mass);
            acceleration += f;
        }

        public void Update()
        {
            velocity += acceleration;
            position += velocity;
            acceleration *= 0;
        }

        public virtual void OnPaint(PaintEventArgs e)
        {
            int width = (int)mass + 2;
            int height = (int)mass + 2;
            e.Graphics.FillEllipse(brush, new Rectangle((int)position.X - width / 2, (int)position.Y - height / 2, width, height));
        }

        internal Vector attract(Mover mover)
        {
            Vector force = position - mover.position;
            double distance = force.Length;
            force.Normalize();
            double strength = (2.0 * mass * mover.mass) / (distance * distance);
            Vector.Multiply(force, strength);
            return force;
        }
    }

    class Vehicle : Mover
    {
        private float maxspeed;
        private float maxforce;

        public Vehicle()
            : base()
        {
            maxspeed = 4;
            maxforce = 0.1f;
        }

        /*
         * https://msdn.microsoft.com/en-us/library/windows/desktop/bb509618(v=vs.85).aspx
         */
        private double lerp(double x, double y, double s)
        {
            return x * (1 - s) + y * s;
        }

        internal void seek(Vector target)
        {
            Vector desired = target - position;
            double d = desired.Length;
            desired.Normalize();

            double closeTo = 50;
            if (d < closeTo)
            {
                double m = lerp(0, maxspeed, d / closeTo);
                desired *= m;
            }
            else
            {
                desired *= maxspeed;
            }

            Vector steer = desired - velocity;
            if (steer.LengthSquared > maxforce)
            {
                steer.Normalize();
                steer *= maxforce;
            }
            applyForce(steer);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            Point[] points = new Point[]
            {
                new Point(  0,  25),
                new Point(-10, -25),
                new Point( 10, -25)
            };

            e.Graphics.TranslateTransform((int)position.X, (int)position.Y);
            e.Graphics.RotateTransform((float)Vector.AngleBetween(new Vector(0, 1), velocity));
            e.Graphics.FillPolygon(brush, points);

            //e.Graphics.FillEllipse(Brushes.Red, new Rectangle(-2, -2, (int)4, (int)4));

            e.Graphics.ResetTransform(); //
        }
    }

    class TestForm : Form
    {
        Timer timer = new Timer();
        int counter = 0;
        Random random;

        private Vehicle mover;
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

            mover = new Vehicle();
            mover.position = new Vector(30.0, 30.0);
            mover.mass = 1;
            mover.brush = Brushes.Blue;

            attractor = new Mover();
            attractor.position = new Vector(250, 250);
            attractor.mass = 20;
            attractor.brush = Brushes.Red;
        }

        private void Update(object sender, EventArgs e)
        {
            mover.seek(attractor.position);
            mover.Update();

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
            mover.OnPaint(e);
        }
    }
}
