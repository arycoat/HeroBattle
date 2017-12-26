using HeroBattle.PathFind;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace HeroBattle
{
    using State = CharacterState.State;

    class Hero
    {
        float height = 20, width = 20;

        private Point position { get; set; }

        private Map map;
        private List<Point> path;
        private CharacterState state;
        private Brush brush = Brushes.White;

        private long hp, maxHp;

        private Hero target { get; set; }
        private Point endPos { get; set; }

        public Hero()
        {
            path = new List<Point>();
            state = new CharacterState();
            this.endPos = new Point(-1, -1);
            this.target = null;
        }

        public void SetUp(Map map, Brush brush)
        {
            this.map = map;
            this.brush = brush;

            this.maxHp = 1000;
            this.hp = this.maxHp;
        }

        public void Update(long delta)
        {
            if (state.GetState() == State.None)
            {
                FindTarget();
            }

            if (state.GetState() == State.Move)
            {
                Move();
            }

            if (state.GetState() == State.Attack)
            {
                Attack();
            }
        }

        private void Attack()
        {
            state.SetTimeStamp(state.GetTimeStamp() - 150);

            if (state.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            Debug.Print("Attack()");
        }

        private void Move()
        {
            state.SetTimeStamp(state.GetTimeStamp() - 150);

            if (state.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            //Debug.Print("Move()");

            state.SetTimeStamp(state.GetTimeStamp() + 1000);

            if (path.Count > 0)
            {
                position = path[0];
                path.RemoveAt(0);
                Debug.Print("Move() : hero : {0} => enemy : {1}", position, endPos);
            }

            if (path.Count == 0)
            {
                state.SetTimeStamp(state.GetTimeStamp() + 1000);
                state.SetState(State.Attack);
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

        public void SetTarget(Hero target)
        {
            this.target = target;
        }

        public void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillEllipse(this.brush,
                new Rectangle((int)position.X * 50 + 15, (int)position.Y * 50 + 15, (int)height, (int)width));
        }

        public bool IsEndMove()
        {
            return (state.GetState() == State.None && endPos.Equals(new Point(-1, -1)));
        }

        private void FindPath(Hero target_)
        {
            Point targetPos = target_.GetPosition();

            SearchParameters searchParameters = new SearchParameters(position, targetPos, map);
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> foundPath = pathFinder.FindPath();

            if (foundPath.Count > 0)
            {
                endPos = targetPos;
                foundPath.RemoveAt(foundPath.Count - 1);
                path = foundPath;
            }
        }

        private void FindTarget()
        {
            Hero target_ = this.target;

            if (target_.GetPosition() != new Point(-1, -1))
            {
                FindPath(target_);

                state.SetState(State.Move);
                state.SetTimeStamp(1000);
            }
        }
    }
}
