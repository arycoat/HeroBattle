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
        private Hero hero;
        private List<Point> path;
        private Point endPos { get; set; }

        public MoveNormal(Hero hero)
        {
            path = new List<Point>();
            this.endPos = new Point(-1, -1);
            this.hero = hero;
        }

        public override void Update()
        {
            hero.state.SetTimeStamp(hero.state.GetTimeStamp() - 150);

            if (hero.state.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            //Debug.Print("Move()");

            hero.state.SetTimeStamp(hero.state.GetTimeStamp() + 1000);

            if (path.Count > 0)
            {
                hero.SetPosition(path[0]);
                path.RemoveAt(0);
                Debug.Print("Move() : hero : {0} => enemy : {1}", hero.GetPosition(), endPos);
            }

            if (path.Count == 0)
            {
                hero.state.SetTimeStamp(hero.state.GetTimeStamp() + 1000);
                hero.state.SetState(State.Attack);
                endPos = new Point(-1, -1);
            }
        }

        public override bool IsEnd()
        {
            return (hero.state.GetState() == State.None && endPos.Equals(new Point(-1, -1)));
        }

        public override void FindPath(Hero target_)
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
