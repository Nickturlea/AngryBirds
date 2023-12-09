using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class AimShotComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D aimShotTexture;
        private Texture2D xTexture; // Texture for the "X"
        public Vector2 position;
        public int width;
        public int height;
        private bool drawX = false;
        private Vector2 xPosition;

        public Vector2 PositionAimshot { get; private set; }
        public int ImageWidthAimshot { get; private set; }
        public int ImageHeightAimshot { get; private set; }


        public AimShotComponent(Game game, Vector2 initialPosition, Texture2D aimShotTexture, Texture2D xTexture, int width, int height)
            : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.position = initialPosition;
            this.aimShotTexture = aimShotTexture;
            this.xTexture = xTexture;
            this.width = width;
            this.height = height;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseState mouseState = Mouse.GetState();
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                Rectangle aimShotBounds = new Rectangle((int)position.X, (int)position.Y, width, height);
                if (aimShotBounds.Contains(mouseState.Position))
                {
                    drawX = true;
                    xPosition = mouseState.Position.ToVector2();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(aimShotTexture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.White);

            if (drawX)
            {
                // Scale factor for the "X" texture
                float scale = 0.5f; // Adjust scale as needed
                                    // Calculate the scaled width and height
                int scaledWidth = (int)(xTexture.Width * scale);
                int scaledHeight = (int)(xTexture.Height * scale);
                // Draw the "X" centered at the position where the aimShot was selected
                spriteBatch.Draw(xTexture, new Rectangle((int)xPosition.X - scaledWidth / 2, (int)xPosition.Y - scaledHeight / 2, scaledWidth, scaledHeight), Color.White);
            }

            spriteBatch.End();
        }


    }
}

