using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HeroBattle.CharacterState;

namespace HeroBattle
{
    public class AttackNearest : AttackBase
    {
        private Character hero;

        public AttackNearest(Character hero)
            : base(AttackType.Nearest)
        {
            this.hero = hero;
        }

        public override void Update()
        {
            base.Update();

            base.SetTimeStamp(base.GetTimeStamp() - 150);

            if (base.GetTimeStamp() > 0)
            {
                // 아직 움직일 시간이 되지 않았음.
                return;
            }

            Character target_ = hero.room.FindCharacter(hero.GetTarget());

            if (target_ == null)
            {
                return;
            }

            double dist = hero.room.DistanceTo(hero.GetPosition(), target_.GetPosition());
            Debug.WriteLine("{0} -> {1} = {2}", hero.GetPosition(), target_.GetPosition(), dist);
            if (dist > 1.5)
            {
                hero.SetMoveState();
                return;
            }

            if (target_.IsAlive())
            {
                target_.SetDamage(100); // damage > 0
                Debug.Print("Attack() : id = {0} enemy hp = {1}", target_.Id, target_.GetHp());

                if (target_.IsAlive() == false)
                {
                    hero.SetTarget(0);
                    hero.SetNoneState();
                }
            }
        }
    }
}
