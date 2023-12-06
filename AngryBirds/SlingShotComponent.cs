using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AngryBirds
{
    internal class SlingShotComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D slingShotTexture;
        private Vector2 position;
        private int imageWidth; 
        private int imageHeight; 

        public SlingShotComponent(Game game, Vector2 initialPosition, Texture2D slingShotTexture, int width, int height) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.slingShotTexture = slingShotTexture;
            this.imageWidth = width;
            this.imageHeight = height;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(slingShotTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
