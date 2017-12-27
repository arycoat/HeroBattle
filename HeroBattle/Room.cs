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
            hero.SetBrush(Brushes.Blue);
            hero.SetMoveType(new MoveNormal(hero));
            hero.SetPosition(new Point(-1, -1));

            hero.SetPosition(new Point(5, 1));
            characters.Add(hero);

            for (int i = 1000; i < 1003; i++)
            {
                Random random = new Random();
                Point endPoint = new Point(random.Next(0, 9), random.Next(0, 9));
                if (map.IsWalkable(endPoint.X, endPoint.Y) == true)
                {
                    Hero enemy = new Hero();
                    enemy.SetUp(this, i, CharacterType.Enemy);
                    enemy.SetBrush(Brushes.Red);
                    enemy.SetMoveType(new MoveNone(enemy));
                    enemy.SetPosition(endPoint);
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

        public Character SearchTarget(Character hero)
        {
            Func<Point, Point, double> DistanceTo = (point1, point2) =>
            {
                var a = (double)(point2.X - point1.X);
                var b = (double)(point2.Y - point1.Y);

                return Math.Sqrt(a * a + b * b);
            };

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
            
            //characters.ForEach(c => (Hero)c.OnPaint(e));

            //
            //Font myFont = new System.Drawing.Font("Helvetica", 11, FontStyle.Italic);
            //Brush myBrush = new SolidBrush(System.Drawing.Color.Red);
            //e.Graphics.DrawString("E", myFont, myBrush, hero.endPos.X   * 50 + 10, hero.endPos.Y   * 50 + 30);
        }

        internal Character FindCharacter(long targetId)
        {
            return characters.Find(c => c.Id == targetId);
        }
    }
}
