using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class EvilBirdComponent : DrawableGameComponent
{
    private SpriteBatch spriteBatch;
    private Texture2D evilTexture;
    private Vector2 position;
    private Vector2 initialPosition;
    private int imageWidth;
    private int imageHeight;
    private int currentFrame;
    private float timer;
    private const float AnimationInterval = 1.0f; // Duration for each animation cycle in seconds
    private const float VerticalSpeed = 200.0f; // Speed of vertical movement
    private const float PauseInterval = 0.5f; // Pause interval in seconds
    private const int MaxFramesUp = 400; // Number of frames to move up

    private bool isMovingUp;
    private bool isPaused;
    private float pauseTimer;

    public EvilBirdComponent(Game game, Vector2 initialPosition, Texture2D evilTexture, int width, int height) : base(game)
    {
        this.spriteBatch = new SpriteBatch(game.GraphicsDevice);
        this.position = initialPosition;
        this.initialPosition = initialPosition;
        this.evilTexture = evilTexture;
        this.imageWidth = width;
        this.imageHeight = height;
        this.currentFrame = 0;
        this.timer = 0.0f;
        this.isMovingUp = true;
        this.isPaused = false;
        this.pauseTimer = 0.0f;
    }
    public override void Update(GameTime gameTime)
    {
        float elapsedSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

        timer += elapsedSeconds;

        if (!isPaused)
        {
            if (timer >= AnimationInterval / 3.0f)
            {
                currentFrame++;
                if (currentFrame >= 3)
                {
                    currentFrame = 0;
                }

                timer = 0.0f;
            }

            if (isMovingUp)
            {
                position.Y -= VerticalSpeed * elapsedSeconds;

                if (position.Y <= initialPosition.Y - MaxFramesUp)
                {
                    isMovingUp = false;
                    isPaused = true;
                    pauseTimer = 0.0f;
                }
            }
            else
            {
                position.Y += VerticalSpeed * elapsedSeconds;
                if (position.Y >= initialPosition.Y)
                {
                    isMovingUp = true;
                    isPaused = true;
                    pauseTimer = 0.0f;
                }
            }
        }
        else
        {
            pauseTimer += elapsedSeconds;

            if (pauseTimer >= PauseInterval)
            {
                isPaused = false;
            }
        }

        base.Update(gameTime);
    }


    public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(evilTexture, new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight),
                             new Rectangle(currentFrame * 120, 0, 125, 125), Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, imageWidth, imageHeight);
        }
    }
