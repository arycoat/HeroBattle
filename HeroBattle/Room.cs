using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroBattle
{
    using CharacterType = Character.CharacterType;

    public class Room
    {
        private Map map;
        private List<Character> characters;

        public Room()
        {
            map = new Map(10, 10);
            characters = new List<Character>();
        }

        public void Initialize()
        {
            map.Initialize();
            map.load();

            Hero hero = new Hero();
            hero.SetUp(this, 1, CharacterType.Player);
            hero.SetMoveType(new MoveNormal(hero));
            hero.SetAttackType(new AttackNearest(hero));
            hero.SetPosition(new Point(5, 1));
            characters.Add(hero);

            Random random = new Random();
            Func<Point?> GetRandomPosition = () =>
            {
                Point endPoint = new Point(random.Next(0, 9), random.Next(0, 9));
                if (map.IsWalkable(endPoint.X, endPoint.Y) == true && 
                    characters.Exists(c => c.GetPosition().Equals(endPoint)) == false)
                {
                    return endPoint;
                }

                return null;
            };

            for (int i = 0; i < 5; i++)
            {
                Point? endPoint = null;
                while (endPoint == null)
                {
                    endPoint = GetRandomPosition();
                }
                
                if (endPoint != null)
                {
                    Hero enemy = new Hero();
                    enemy.SetUp(this, 1000 + i, CharacterType.Enemy);
                    enemy.SetMoveType(new MoveNone(enemy));
                    enemy.SetPosition(endPoint.Value);
                    characters.Add(enemy);
                }
            }
        }

        public void Update()
        {
            characters.ForEach(c => c.Update(0));
        }

        internal Map GetMap()
        {
            return this.map;
        }

        public double DistanceTo(Point point1, Point point2)
        {
            var a = (double)(point2.X - point1.X);
            var b = (double)(point2.Y - point1.Y);

            return Math.Sqrt(a * a + b * b);
        }

        public Character SearchTarget(Character hero)
        {
            Point pivot = hero.GetPosition();

            List<Character> sorted = characters
                .OrderBy(x => DistanceTo(x.GetPosition(), pivot))
                .ToList();

            return sorted
                .Find(c => c.GetCharacterType() == CharacterType.Enemy && c.IsAlive());
        }

        public void OnPaint(PaintEventArgs e)
        {
            map.OnPaint(e);
            
            characters.ForEach(c =>
            {
                if (c.GetCharacterType() == CharacterType.Player)
                {
                    Point pos = c.GetPosition();
                    e.Graphics.FillEllipse(Brushes.Blue, new Rectangle(pos.X * 50 + 15, pos.Y * 50 + 15, 20, 20));
                }
                else if (c.GetCharacterType() == CharacterType.Enemy && c.IsAlive() == true)
                {
                    Point pos = c.GetPosition();
                    e.Graphics.FillEllipse(Brushes.Red, new Rectangle(pos.X * 50 + 15, pos.Y * 50 + 15, 20, 20));

                    Font myFont = new System.Drawing.Font("Helvetica", 11, FontStyle.Italic);
                    Brush myBrush = new SolidBrush(System.Drawing.Color.Red);
                    e.Graphics.DrawString(c.Id.ToString(), myFont, myBrush, pos.X * 50 + 5, pos.Y * 50 + 5);
                    e.Graphics.DrawString(c.GetHp().ToString(), myFont, myBrush, pos.X * 50 + 5, pos.Y * 50 + 30);
                }
                
            });
        }

        internal Character FindCharacter(long targetId)
        {
            return characters.Find(c => c.Id == targetId);
        }
    }
}
