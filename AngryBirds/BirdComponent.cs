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
        private AimShotComponent aimShot;

        public BirdComponent(Game game, Vector2 initialPosition, Texture2D birdTexture, int width, int height, AimShotComponent aimShot)
            : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.targetPosition = initialPosition;
            this.velocity = Vector2.Zero;
            this.birdTexture = birdTexture;
            this.imageWidth = width;
            this.imageHeight = height;
            this.aimShot = aimShot;
        }

        public bool CanLaunch { get; set; } // Flag to control launch


        public void Launch(float power)
        {
            if (CanLaunch)
            {
                // Use the power value to determine the launch velocity
                velocity = new Vector2(power * 7, 0); // 7 is just an example multiplier
                CanLaunch = false; // Reset the flag after launch
            }
        }


        public override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (CanLaunch)
            {
                position += velocity;
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

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)(position.X - imageWidth / 2),
                (int)(position.Y - imageHeight / 2),
                imageWidth,
                imageHeight);
        }
    }
}
