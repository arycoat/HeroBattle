using HeroBattle.PathFind;
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

        private Point position { get; set; }

        public Point endPos { get; set; }
        private Map map;
        private List<Point> path;

        public Hero()
        {
            path = new List<Point>();
        }

        public void SetUp(Map map)
        {
            this.map = map;
        }

        public void Update(long delta)
        {
            if (path.Count == 0)
            {
                SearchParameters searchParameters = new SearchParameters(position, endPos, map);
                PathFinder pathFinder = new PathFinder(searchParameters);
                path = pathFinder.FindPath();
                if (path.Count > 0)
                    path.RemoveAt(path.Count-1);
            }
            
            if (path.Count > 0)
            {
                position = path[0];
                path.RemoveAt(0);
            }
        }

        internal void SetPosition(Point pos)
        {
            this.position = pos;
        }

        public Point GetPosition()
        {
            return this.position;
        }

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.Tomato,
                new Rectangle((int)position.X * 50, (int)position.Y * 50, (int)height, (int)width));
        }

        public bool IsEndMove()
        {
            return (path.Count == 0);
        }
    }
}
