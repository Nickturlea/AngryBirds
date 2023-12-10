using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class ContactScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D ControlTexture;

        public ContactScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            ControlTexture = g.Content.Load<Texture2D>("Images/Contact");
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();

            //Making image full screen
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            sb.Draw(ControlTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);


            sb.End();

            base.Draw(gameTime);
        }
    }
}
