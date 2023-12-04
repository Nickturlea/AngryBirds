using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class AboutScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D backgroudAboutTexture;
        private SpriteFont mainFont;
        private string aboutText = ("Developed by:\n\nNicholas Turlea\nSamuel Estrada");

        public AboutScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            backgroudAboutTexture = g.Content.Load<Texture2D>("Images/AboutSceneBackground");
            mainFont = g.Content.Load<SpriteFont>("fonts/MainFont");
        }
    
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            //Making image full screen
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            sb.Draw(backgroudAboutTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            Vector2 textPosition = new Vector2(50, 50);
            sb.DrawString(mainFont, aboutText, textPosition, Color.White);

            sb.End();

            base.Draw(gameTime);
        }

    }
}
