using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
    public bool IsLaunched { get; private set; }

    public Vector2 Position
    {
        get { return position; }
    }


    public float LaunchCount { get; private set; } = 0;


    private SlingShotComponent slingShot;

    public bool MouseClickedAtZeroProgress
    {
        get { return mouseClickedAtZeroProgress; }
    }

    private int respawnCount;

    public const int MaxRespawnLimit = 3;

    public int RespawnCount
    {
        get { return respawnCount; }
    }

    //private int remainingAnimals;

    //public int RemainingAnimals
    //{
    //    get { return remainingAnimals; }
    //}

    public BirdComponent(Game game, Vector2 initialPosition, Texture2D birdTexture, int width, int height, AimShotComponent aimShot, ProgressBarComponent progressBar, SpriteFont font, SlingShotComponent slingShot)
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
        this.slingShot = slingShot; 
        this.respawnCount = 0;
    }


    public void Launch(float power, Vector2 direction)
    {
        if (!IsLaunched)
        {
            // the birdAimShot should be passed
            velocity = direction * power * 10; // Apply power and direction to velocity
            IsLaunched = true;
            LaunchCount++; // Increment launch count each time the bird is launched
        }
    }


    private bool IsOutOfScreenBounds(Vector2 position)
    {
        return position.X < 0 || position.X > GraphicsDevice.Viewport.Width ||
               position.Y < 0 || position.Y > GraphicsDevice.Viewport.Height;
    }

    private void Respawn()
    {
        position = targetPosition = new Vector2(slingShot.Position.X + 67, slingShot.Position.Y - 5);
        velocity = Vector2.Zero;
        IsLaunched = false;
        respawnCount++;
        progressBar.ResetProgress();
    }


    public Rectangle GetBounds()
    {
        return new Rectangle(
            (int)(position.X - imageWidth / 2),
            (int)(position.Y - imageHeight / 2),
            imageWidth,
            imageHeight);
    }


    public override void Update(GameTime gameTime)
    {
        MouseState mouseState = Mouse.GetState();

        // Check for mouse click and if it's within the aimShot component
        if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            Rectangle aimShotBounds = new Rectangle(
                (int)aimShot.positionAimshot.X,
                (int)aimShot.positionAimshot.Y,
                aimShot.imageWidthAimshot,
                aimShot.imageHeightAimshot);

            if (aimShotBounds.Contains(mouseState.X, mouseState.Y) && !IsLaunched)
            {
                float progress = progressBar.Progress;
                if (progress > 0.0f)
                {
                    // Calculate the direction vector from the slingshot to the mouse click position.
                    Vector2 aimDirection = Vector2.Normalize(new Vector2(mouseState.X, mouseState.Y) - slingShot.Position);
                    // Call the Launch method with the progress as power and the calculated direction.
                    Launch(progress, aimDirection);
                    mouseClickedAtZeroProgress = false;
                    aimShot.Visible = false; // Hide the aimShot as the bird has been launched.
                }
                else
                {
                    mouseClickedAtZeroProgress = true;
                    aimShot.Visible = false; // Hide the aimShot as there's an attempt to launch without power.
                }
            }
        }

        // Apply velocity if the bird has been launched
        if (IsLaunched)
        {
            position += velocity;
        }

        // Check if the bird is off screen and respawn if needed
        if (IsOutOfScreenBounds(position) && respawnCount < MaxRespawnLimit)
        {
            Respawn();
        }
        else if (IsOutOfScreenBounds(position) && respawnCount >= MaxRespawnLimit)
        {
            // If the bird is off screen and the max respawn limit reached, stop the bird
            velocity = Vector2.Zero;
            IsLaunched = false; // Allow the bird to be launched again
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

        spriteBatch.DrawString(font, $"Total Allowed Bird Flys is 3!\nHere's Your Count: {respawnCount.ToString()}",new Vector2(7, 410), Color.AntiqueWhite);

        if (progressBar.Progress == 0.0f && mouseClickedAtZeroProgress)
        {
            spriteBatch.DrawString(font, "Please Use The SpaceBar To Select\nA Speed First Then Click", new Vector2(7, 19), Color.Red);
        }
        if (progressBar.Progress > 0.0f)
        {
            mouseClickedAtZeroProgress = false;
        }

        spriteBatch.End();

        base.Draw(gameTime);
    }

}
