using HeroBattle.PathFind;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    using State = CharacterState.State;

    class MoveNormal : MoveBase
    {
        private Character hero;
        private List<Point> path;
        private Point endPos { get; set; }

        public MoveNormal(Character hero)
            : base(MoveType.Normal)
        {
            path = new List<Point>();
            this.endPos = new Point(-1, -1);
            this.hero = hero;
        }

        public override void Update()
        {
            base.SetTimeStamp(base.GetTimeStamp() - 150);

            if (base.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            //Debug.Print("Move()");

            base.SetTimeStamp(base.GetTimeStamp() + 1000);

            if (path.Count > 0)
            {
                hero.SetPosition(path[0]);
                path.RemoveAt(0);
                Debug.Print("Move() : hero : {0} => enemy : {1}", hero.GetPosition(), endPos);
            }

            if (path.Count == 0)
            {
                hero.SetAttackState();
                endPos = new Point(-1, -1);
            }
        }

        public override bool IsEnd()
        {
            return (base.GetState() == State.None && endPos.Equals(new Point(-1, -1)));
        }

        public override void FindPath(Character target_)
        {
            Point targetPos = target_.GetPosition();

            SearchParameters searchParameters = new SearchParameters(hero.GetPosition(), targetPos, hero.GetMap());
            PathFinder pathFinder = new PathFinder(searchParameters);
            List<Point> foundPath = pathFinder.FindPath();

            if (foundPath.Count > 0)
            {
                endPos = targetPos;
                foundPath.RemoveAt(foundPath.Count - 1);
                path = foundPath;
            }
        }
    }
}
