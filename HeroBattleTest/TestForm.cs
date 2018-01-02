using System;
using System.Windows;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroBattleTest
{
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
            e.Graphics.FillEllipse(brush, new Rectangle((int)position.X, (int)position.Y, (int)mass+2, (int)mass+2));
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

        internal void seek(Vector target)
        {
            Vector desired = target - position;
            desired.Normalize();
            desired *= maxspeed;

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
            e.Graphics.FillEllipse(brush, new Rectangle((int)position.X, (int)position.Y, (int)20, (int)20));
        }
    }

    class TestForm : Form
    {
        Timer timer = new Timer();

        private Vehicle mover;
        private Mover attractor;

        private Vector gravity;

        public TestForm()
        {
            this.Size = new System.Drawing.Size(517, 540);
            this.DoubleBuffered = true;

            timer.Tick += new EventHandler(Update);
            timer.Interval = 20;
            timer.Enabled = true;
            timer.Start();

            gravity = new Vector(0, 0.5);

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

            //
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);

            mover.OnPaint(e);
            attractor.OnPaint(e);
        }
    }
}
