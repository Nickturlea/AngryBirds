using AngryBirds;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

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
    private ProgressBarComponent progressBar;
    private SpriteFont font; 
    private bool mouseClickedAtZeroProgress;

    public BirdComponent(Game game, Vector2 initialPosition, Texture2D birdTexture, int width, int height, AimShotComponent aimShot, ProgressBarComponent progressBar, SpriteFont font) // Added SpriteFont parameter
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
        this.progressBar = progressBar;
        this.font = font; 
        this.mouseClickedAtZeroProgress = false;
    }

    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            Rectangle aimShotBounds = new Rectangle(
                (int)aimShot.positionAimshot.X,
                (int)aimShot.positionAimshot.Y,
                aimShot.imageWidthAimshot,
                aimShot.imageHeightAimshot);

            if (aimShotBounds.Contains(mouseState.X, mouseState.Y))
            {
                float progress = progressBar.Progress;

                if (progress > 0.0f)
                {
                    float speed = 3f + 10f * progress;
                    targetPosition = new Vector2(mouseState.X, mouseState.Y);
                    velocity = Vector2.Normalize(targetPosition - position) * speed;
                    mouseClickedAtZeroProgress = false; 
                }
                else
                {
                    mouseClickedAtZeroProgress = true;
                }
            }
        }

        position += velocity;

        previousMouseState = mouseState;

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();

        Rectangle sourceRectangle = new Rectangle(0, 0, 125, 125);
        Vector2 drawPosition = new Vector2(position.X - imageWidth / 2, position.Y - imageHeight / 2);

        spriteBatch.Draw(birdTexture, new Rectangle((int)drawPosition.X, (int)drawPosition.Y, imageWidth, imageHeight), sourceRectangle, Color.White);

        if (progressBar.Progress == 0.0f && mouseClickedAtZeroProgress)
        {
            spriteBatch.DrawString(font, "Please Use The SpaceBar To Select\nA Speed First Then Click", new Vector2(10, 10), Color.Red);
        }
        if (progressBar.Progress > 0.0f)
        {
            mouseClickedAtZeroProgress = false;
        }

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
