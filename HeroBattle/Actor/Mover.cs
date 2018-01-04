using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HeroBattle
{
    public class Mover
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
}
