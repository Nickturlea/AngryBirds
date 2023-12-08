using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont mainFont, menuFont;
        private List<string> menuItems;
        private Texture2D backgroundmenuTexture;

        public int selectedIndex { get; set; }
        private Vector2 position;
        private Color mainColor = Color.Black;
        private Color menuColor = Color.Red;

        private KeyboardState menu;
        public MenuComponent(Game game, SpriteBatch sb,
          SpriteFont mainFont, SpriteFont menuFont, string[] menus, Texture2D backgroundmenuTexture) : base(game)
        {
            this.sb = sb;
            this.mainFont = mainFont;
            this.menuFont = menuFont;
            this.backgroundmenuTexture = backgroundmenuTexture;
            menuItems = menus.ToList();

            //positioning my menu, gotta convert so no magic numbers later
            float centerY = Shared.stage.Y / 2 - (menuItems.Count * mainFont.LineSpacing) / 2;
            float spacing = 100;
            float centerX = spacing;
            position = new Vector2(centerX, centerY);
            this.backgroundmenuTexture = backgroundmenuTexture;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();



            if (ks.IsKeyDown(Keys.Down) && menu.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && menu.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            menu = ks;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {

            sb.Begin();


            int screenWidth = (int)Shared.stage.X;
            int screenHeight = (int)Shared.stage.Y;

            sb.Draw(backgroundmenuTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);


            Vector2 tempPos = position;
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedIndex)
                {
                    sb.DrawString(menuFont, menuItems[i], tempPos, menuColor);
                    tempPos.Y += menuFont.LineSpacing;
                }
                else
                {
                    sb.DrawString(mainFont, menuItems[i], tempPos, mainColor);
                    tempPos.Y += mainFont.LineSpacing;

                }
            }
            sb.End();
            base.Draw(gameTime);
        }

    }
}
