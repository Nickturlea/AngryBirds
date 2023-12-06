using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AngryBirds
{
    internal class BirdComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D birdTexture;
        private Vector2 position;
        private Vector2 targetPosition;
        private Vector2 velocity;
        private int imageWidth;
        private int imageHeight;
        private MouseState previousMouseState;
        private float speed = 3f;

        public BirdComponent(Game game, Vector2 initialPosition, Texture2D birdTexture, int width, int height)
            : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.targetPosition = initialPosition;
            this.velocity = Vector2.Zero;
            this.birdTexture = birdTexture;
            this.imageWidth = width;
            this.imageHeight = height;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                targetPosition = new Vector2(mouseState.X, mouseState.Y);
                velocity = Vector2.Normalize(targetPosition - position) * speed;
            }


            if (Vector2.Distance(position, targetPosition) > speed)
            {
                position += velocity;
            }
            else
            {
                position = targetPosition;
                velocity = Vector2.Zero;
            }

            previousMouseState = mouseState;

            base.Update(gameTime);
        }



        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            Rectangle sourceRectangle = new Rectangle(0, 0, 125, 125);
            Vector2 drawPosition = new Vector2(position.X - imageWidth / 2, position.Y - imageHeight / 2);

            spriteBatch.Draw(birdTexture, new Rectangle((int)drawPosition.X, (int)drawPosition.Y, imageWidth, imageHeight), sourceRectangle, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
