using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AngryBirds
{
    internal class levelComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont mainFont;
        private List<string> menuItems;

        public int selectedIndex { get; set; }
        private Vector2 position;
        private Color mainColor = Color.White;

        private KeyboardState levelmenu;

        public levelComponent(Game game, SpriteBatch sb,
        SpriteFont mainFont, string[] menus) : base(game)
        {
            this.sb = sb;
            this.mainFont = mainFont;
            menuItems = menus.ToList();

            //positioning my menu, gotta convert so no magic numbers later
            float centerY = Shared.stage.Y / 2 - (menuItems.Count * mainFont.LineSpacing) / 2;
            float spacing = 100;
            float centerX = spacing;
            position = new Vector2(centerX, centerY);
        }


        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();



            if (ks.IsKeyDown(Keys.Down) && levelmenu.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && levelmenu.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            levelmenu = ks;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            sb.Begin();



            Vector2 tempPos = position;
            for (int i = 0; i < menuItems.Count; i++)
            {
                    sb.DrawString(mainFont, menuItems[i], tempPos, mainColor);
                    tempPos.Y += mainFont.LineSpacing;


            }
            sb.End();
            base.Draw(gameTime);
        }

    }
}
