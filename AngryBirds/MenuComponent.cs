using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AngryBirds
{
    internal class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont mainFont, menuFont;
        private List<string> menuItems;
        private Texture2D backgroundmenuTexture;

        public int selectedIndex { get; private set; }
        private Vector2 position;
        private Color mainColor = Color.Black;
        private Color menuColor = Color.Red;
        private Song backgroundMusic;

        private KeyboardState menu;

        public MenuComponent(Game game, SpriteBatch sb, SpriteFont mainFont, SpriteFont menuFont, string[] menus, Texture2D backgroundmenuTexture) : base(game)
        {
            this.sb = sb;
            this.mainFont = mainFont;
            this.menuFont = menuFont;
            this.backgroundmenuTexture = backgroundmenuTexture;
            menuItems = menus.ToList();
            MediaPlayer.Volume = 0.20f;
            backgroundMusic = game.Content.Load<Song>("Music/startMusic");

            float centerY = Shared.stage.Y / 2 - (menuItems.Count * mainFont.LineSpacing) / 2;
            float spacing = 100;

            float totalWidth = menuItems.Max(item => menuFont.MeasureString(item).X);

            float centerX = (Shared.stage.X - totalWidth) / 2;

            position = new Vector2(centerX, centerY);
        }

        private KeyboardState previousKs;

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKs = Keyboard.GetState();

            if (currentKs.IsKeyDown(Keys.Down) && previousKs.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (currentKs.IsKeyDown(Keys.Up) && previousKs.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }


            previousKs = currentKs;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            if (MediaPlayer.State != MediaState.Playing)
            {
                // Play the background music
                MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }

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
