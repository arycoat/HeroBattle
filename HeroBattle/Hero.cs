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

        private Brush brush = Brushes.White;
        private long target { get; set; }

        public Hero()
        {
            this.target = 0;
        }

        public override void SetUp(Room room, long id, CharacterType characterType)
        {
            base.SetUp(room, id, characterType);
            this.maxHp = 1000;
            this.hp = this.maxHp;
        }

        public void SetBrush(Brush brush)
        {
            this.brush = brush;
        }

        public void SetMoveType(MoveBase move)
        {
            base.move = move;
        }

        public override void Update(long delta)
        {
            if (state.GetState() == State.None)
            {
                if (move.moveType == MoveBase.MoveType.None)
                    return;

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

            Character target_ = room.FindCharacter(this.target);

            if (target_ == null)
            {
                return;
            }

            double dist = room.DistanceTo(GetPosition(), target_.GetPosition());
            Debug.WriteLine("{0} -> {1} = {2}", GetPosition(), target_.GetPosition(), dist);
            if (dist > 1.5)
            {
                state.SetState(State.Move);
                return;
            }

            if (target_.IsAlive())
            {
                target_.SetDamage(100); // damage > 0
                Debug.Print("Attack() : id = {0} enemy hp = {1}", target_.Id, target_.GetHp());

                if (target_.IsAlive() == false)
                {
                    SetTarget(0);
                    state.SetState(State.None);
                }
            }
        }

        public void SetTarget(long targetId)
        {
            this.target = targetId;
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
            Character target_ = room.FindCharacter(this.target);

            if (target_ == null)
                target_ = room.SearchTarget(this);

            if (target_ != null && target_.GetPosition() != new Point(-1, -1))
            {
                SetTarget(target_.Id);
                move.FindPath(target_);

                state.SetState(State.Move);
                state.SetTimeStamp(1000);
            }
        }
    }
}
