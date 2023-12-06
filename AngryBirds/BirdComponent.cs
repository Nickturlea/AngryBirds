using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class BirdComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D birdTexture;
        private Vector2 position;
        private int imageWidth;
        private int imageHeight;

            public BirdComponent(Game game, Vector2 initialPosition, Texture2D birdTexture, int width, int height) : base(game)
            {
                this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
                this.position = initialPosition;
                this.birdTexture = birdTexture;
                this.imageWidth = width;
                this.imageHeight = height;
            }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(birdTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
