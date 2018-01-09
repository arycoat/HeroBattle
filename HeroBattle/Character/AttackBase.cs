using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    public class AttackBase : CharacterState
    {
        public enum AttackType
        {
            None,
            Nearest,
        }

        private readonly AttackType attackType;

        public AttackBase(AttackType attackType)
            : base(State.Attack)
        {
            this.attackType = attackType;
        }

        public override void Update()
        {

        }
    }
}
