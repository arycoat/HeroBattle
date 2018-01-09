using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBattle
{
    using State = CharacterState.State;
    using MoveType = HeroBattle.MoveBase.MoveType;

    public class Character
    {
        public enum CharacterType
        {
            Player,
            Enemy,
        }

        private MoveBase move { get; set; }
        private AttackBase attack { get; set; }
        private CharacterState state;

        protected long hp, maxHp;
        private Point position;
        private Room room;
        private long Id;
        private CharacterType characterType { get; set; }
        private long target { get; set; }

        public Character()
        {
            this.target = -1;
            this.state = NoneBase.GetEmpty;
        }

        public virtual void SetUp(Room room, long id, CharacterType characterType)
        {
            this.room = room;
            this.Id = id;
            this.characterType = characterType;
        }

        public void SetMoveType(MoveBase move)
        {
            this.move = move;
        }

        internal MoveBase.MoveType GetMoveType()
        {
            return move.moveType;
        }

        public void SetAttackType(AttackBase attack)
        {
            this.attack = attack;
        }

        public void SetMoveState()
        {
            move.SetTimeStamp(1000);
            state = move;
        }

        public void SetAttackState()
        {
            attack.SetTimeStamp(1000);
            state = attack;
        }

        public void SetNoneState()
        {
            state = NoneBase.GetEmpty;
        }

        public CharacterType GetCharacterType()
        {
            return this.characterType;
        }

        public State GetState()
        {
            return state.GetState();
        }

        public virtual void Update(long delta)
        {
            state.Update();
        }

        public long GetHp()
        {
            return this.hp;
        }

        public long GetID()
        {
            return this.Id;
        }

        public Point GetPosition()
        {
            return this.position;
        }

        internal void SetPosition(Point pos)
        {
            this.position = pos;
        }

        public bool IsAlive()
        {
            return (hp > 0);
        }

        public void SetDamage(long damage)
        {
            this.hp = Math.Max(this.hp - damage, 0);
        }

        internal Map GetMap()
        {
            return this.room.GetMap();
        }

        public long GetTarget()
        {
            return this.target;
        }

        internal void SetTarget(long target)
        {
            this.target = target;
        }

        internal void FindPath(Character target)
        {
            move.FindPath(target);
        }

        public Character FindCharacter()
        {
            return room.FindCharacter(target);
        }

        public Character SearchTarget()
        {
            return room.SearchTarget(this);
        }

        internal double DistanceTo(Point point)
        {
            return room.DistanceTo(GetPosition(), point);
        }
    }
}
