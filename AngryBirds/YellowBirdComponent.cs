using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace AngryBirds
{
    internal class YellowBirdComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D BirdTexture;
        private Vector2 position;
        private int imageWidth;
        private int imageHeight;

        public YellowBirdComponent(Game game, Vector2 initialPosition, Texture2D yellowBird, int width, int height) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.BirdTexture = yellowBird;
            this.imageWidth = width;
            this.imageHeight = height;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(BirdTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);
        }

    }
}
