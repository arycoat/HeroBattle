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
    using State = HeroState.State;

    class Hero
    {
        float height = 20, width = 20;

        private Point position { get; set; }
        public Point endPos { get; set; }

        private Map map;
        private List<Point> path;
        private HeroState state;

        public Hero()
        {
            path = new List<Point>();
            state = new HeroState();
        }

        public void SetUp(Map map)
        {
            this.map = map;
        }

        public void Update(long delta)
        {
            if (state.GetState() == State.Move)
            {
                Move();
            }

            if (state.GetState() == State.None)
            {
                if (endPos != new Point(-1, -1))
                {
                    FindPath();

                    state.SetState(State.Move);
                    state.SetTimeStamp(1000);
                }
            }
        }

        private void Move()
        {
            state.SetTimeStamp(state.GetTimeStamp() - 150);

            if (state.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            state.SetTimeStamp(state.GetTimeStamp() + 1000);

            if (path.Count > 0)
            {
                position = path[0];
                path.RemoveAt(0);
            }

            if (IsEndMove() == true)
            {
                state.SetState(State.None);
                endPos = new Point(-1, -1);
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

        private void FindPath()
        {
            SearchParameters searchParameters = new SearchParameters(position, endPos, map);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> foundPath = pathFinder.FindPath();

            if (foundPath.Count > 0)
            {
                foundPath.RemoveAt(foundPath.Count - 1);
                path = foundPath;
            }
        }
    }
}
