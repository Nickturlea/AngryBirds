using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D aimShot;
        public Vector2 positionAimshot;
        public int imageWidthAimshot;
        public int imageHeightAimshot;

        public AimShotComponent(Game game, Vector2 initialPosition, Texture2D aimShot, int width, int height) : base(game)
        {
            this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
            this.positionAimshot = initialPosition;
            this.aimShot = aimShot;
            this.imageWidthAimshot = width;
            this.imageHeightAimshot = height;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(aimShot, new Rectangle((int)positionAimshot.X, (int)positionAimshot.Y, imageWidthAimshot, imageHeightAimshot), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)positionAimshot.X, (int)positionAimshot.Y, imageWidthAimshot, imageHeightAimshot);
        }

    }
}
