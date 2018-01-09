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
    using MoveType = HeroBattle.MoveBase.MoveType;

    class Hero : Character
    {
        public Hero()
        {

        }

        public override void SetUp(Room room, long id, CharacterType characterType)
        {
            base.SetUp(room, id, characterType);
            this.maxHp = 1000;
            this.hp = this.maxHp;
        }

        public override void Update(long delta)
        {
            if (GetState() == State.None && GetMoveType() != MoveType.None)
            {
                FindTarget();
            }

            base.Update(delta);
        }

        private void FindTarget()
        {
            Character target_ = room.FindCharacter(GetTarget());

            if (target_ == null)
                target_ = room.SearchTarget(this);

            if (target_ != null && target_.GetPosition() != new Point(-1, -1))
            {
                SetTarget(target_.Id);
                FindPath(target_);
                SetMoveState();
            }
        }
    }
}
