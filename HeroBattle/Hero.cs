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

    class Hero : Character
    {
        float height = 20, width = 20;

        private Map map;
        private Brush brush = Brushes.White;
        private Character target { get; set; }

        public Hero()
        {
            base.move = new MoveNormal(this);
            this.target = null;
        }

        public void SetUp(Map map, Brush brush)
        {
            this.map = map;
            this.brush = brush;

            this.maxHp = 1000;
            this.hp = this.maxHp;
        }

        public override void Update(long delta)
        {
            if (state.GetState() == State.None)
            {
                FindTarget();
            }

            if (state.GetState() == State.Move)
            {
                move.Update();
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

            Character target_ = this.target;

            if (target_ == null)
            {
                return;
            }

            if (target_.IsAlive())
            {
                target_.SetDamage(100); // damage > 0
                Debug.Print("Attack() : enemy hp = {0}", target_.GetHp());
            }
        }

        internal Map GetMap()
        {
            return this.map;
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
            return move.IsEnd();
        }

        private void FindTarget()
        {
            Character target_ = this.target;

            if (target_.GetPosition() != new Point(-1, -1))
            {
                move.FindPath(target_);

                state.SetState(State.Move);
                state.SetTimeStamp(1000);
            }
        }
    }
}
