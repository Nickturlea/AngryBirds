using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AngryBirds
{
    internal class birdAimShot : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D BoxTexture;
        private Vector2 position;
        private int imageWidth;
        private int imageHeight;
        public int ImageWidth => imageWidth;
        public int ImageHeight => imageHeight;
        public bool Visible { get; set; } = false;

        public birdAimShot(Game game, Texture2D slingShotTexture, int width, int height) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.BoxTexture = slingShotTexture;
            this.imageWidth = width;
            this.imageHeight = height;
        }

        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(BoxTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight), Color.White);
                spriteBatch.End();
            }
        }

        public void UpdatePosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);
        }
    }
}
