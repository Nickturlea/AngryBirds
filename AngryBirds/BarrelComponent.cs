using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AngryBirds
{
    internal class BarrelComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D BoxTexture;
        private Vector2 position;
        private int imageWidth;
        private int imageHeight;

        public BarrelComponent(Game game, Vector2 initialPosition, Texture2D slingShotTexture, int width, int height) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.BoxTexture = slingShotTexture;
            this.imageWidth = width;
            this.imageHeight = height;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(BoxTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);
        }

    }
}
