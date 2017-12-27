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

            Hero enemy = new Hero();
            enemy.SetUp(this, 2, CharacterType.Enemy);
            enemy.SetBrush(Brushes.Red);
            enemy.SetMoveType(new MoveNone(enemy));
            enemy.SetPosition(new Point(-1, -1));

            //
            hero.SetPosition(new Point(5, 1));
            enemy.SetPosition(new Point(7, 5));

            characters.Add(hero);
            characters.Add(enemy);
        }

        public void Update()
        {
            characters.ForEach(c =>
            {
                c.Update(0);

                if (c.GetCharacterType() == CharacterType.Enemy && c.IsAlive() == false)
                {
                    Random random = new Random();
                    Point endPoint = new Point(random.Next(0, 9), random.Next(0, 9));
                    if (map.IsWalkable(endPoint.X, endPoint.Y) == true)
                    {
                        c.SetUp(this, c.Id + 1, CharacterType.Enemy);
                        c.SetPosition(endPoint);
                    }
                }
            });
        }

        internal Map GetMap()
        {
            return this.map;
        }

        public Character SearchTarget()
        {
            return characters.Find(c => c.GetCharacterType() == CharacterType.Enemy);
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
