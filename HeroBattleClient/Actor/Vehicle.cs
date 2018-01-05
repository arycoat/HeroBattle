using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HeroBattleClient
{
    using Point = System.Drawing.Point;

    public class Vehicle : Mover
    {
        private float maxspeed;
        private float maxforce;
        public float range;
        public long id;

        public Vehicle()
            : base()
        {
            maxspeed = 3;
            maxforce = 0.1f;
            range = 0;
        }

        /*
         * https://msdn.microsoft.com/en-us/library/windows/desktop/bb509618(v=vs.85).aspx
         */
        private double lerp(double x, double y, double s)
        {
            return x * (1 - s) + y * s;
        }

        public void seek(Vector target)
        {
            Vector desired = target - position;
            double d = desired.Length;
            if (d < 0.1)
                return;

            desired.Normalize();

            double closeTo = 50;
            if (d < closeTo + range)
            {
                double m = lerp(0, maxspeed, Math.Max(d - range, 0) / closeTo);
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

            //!~ debug paint
            base.OnPaint(e);
            //~!
        }
    }
}
