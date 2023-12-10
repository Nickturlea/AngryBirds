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
    private bool spriteChanged = false;
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

    private const int NumFrames = 3; 
    private int currentAnimationFrame = 0; 
    private const float FrameSwitchInterval = 0.2f; 
    private float frameSwitchTimer = 0.0f; 

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
        if (!IsLaunched && respawnCount < MaxRespawnLimit)
        {
            velocity = direction * power * 10;
            IsLaunched = true;
            LaunchCount++;
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

        currentAnimationFrame = 0;
        spriteChanged = false;
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
                    Vector2 aimDirection = Vector2.Normalize(new Vector2(mouseState.X, mouseState.Y) - slingShot.Position);
                    Launch(progress, aimDirection);


                    if (!spriteChanged)
                    {
                        currentAnimationFrame = 1; 
                        spriteChanged = true;
                    }

                }
                else
                {
                    mouseClickedAtZeroProgress = true;
                }
            }
        }


        if (IsLaunched && progressBar.Progress > 0.0f)
        {
            frameSwitchTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (frameSwitchTimer >= FrameSwitchInterval)
            {
                currentAnimationFrame++;
                if (currentAnimationFrame >= NumFrames)
                {
                    currentAnimationFrame = 0;
                }

                frameSwitchTimer = 0.0f;
            }
        }
        else
        {
            frameSwitchTimer = 0.0f;
        }

        if (IsLaunched)
        {
            position += velocity;
        }

        if (IsOutOfScreenBounds(position) && respawnCount < MaxRespawnLimit)
        {
            Respawn();
        }
        else if (IsOutOfScreenBounds(position) && respawnCount >= MaxRespawnLimit)
        {
            velocity = Vector2.Zero;
            IsLaunched = false;


            currentAnimationFrame = 0;
            spriteChanged = false;
        }

        previousMouseState = mouseState;

        base.Update(gameTime);
    }


    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();

        Rectangle sourceRectangle = new Rectangle(currentAnimationFrame * 115, 0, 125, 125);
        Vector2 drawPosition = new Vector2(position.X - imageWidth / 2, position.Y - imageHeight / 2);

        spriteBatch.Draw(birdTexture, new Rectangle((int)drawPosition.X, (int)drawPosition.Y, imageWidth, imageHeight), sourceRectangle, Color.White);

        Vector2 countTextPosition = new Vector2(7, 410);
        string countText = $"Total Allowed Bird Flys is 3!\nHere's Your Count: {respawnCount.ToString()}";

        Vector2 countTextSize = font.MeasureString(countText);

        Texture2D whiteTexture = new Texture2D(GraphicsDevice, 1, 1);
        whiteTexture.SetData(new Color[] { Color.White });

        // Draw the background
        Rectangle backgroundRect = new Rectangle(
            (int)countTextPosition.X,
            (int)countTextPosition.Y,
            (int)countTextSize.X,
            (int)countTextSize.Y);

        spriteBatch.Draw(whiteTexture, backgroundRect, Color.White);

        spriteBatch.DrawString(font, countText, countTextPosition, Color.Orange);

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
