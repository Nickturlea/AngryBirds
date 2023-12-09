using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class ProgressBarComponent : DrawableGameComponent
{
    private SpriteBatch spriteBatch;
    private Texture2D progressBarTexture;
    private Texture2D progressBarFillTexture;
    private Rectangle progressBarRectangle;
    private Rectangle progressBarFillRectangle;
    private KeyboardState previousKeyboardState;
    private MouseState previousMouseState;

    private float progress; // Value between 0.0f (empty) and 1.0f (full)

    // Add a public property for progress
    public float Progress
    {
        get { return progress; }
        set { progress = MathHelper.Clamp(value, 0.0f, 1.0f); }
    }

    public ProgressBarComponent(Game game, Vector2 position, int width, int height)
        : base(game)
    {
        spriteBatch = new SpriteBatch(game.GraphicsDevice);

        // Load textures or create them dynamically
        progressBarTexture = CreateTexture(game.GraphicsDevice, width, height, Color.LightSkyBlue);
        progressBarFillTexture = CreateTexture(game.GraphicsDevice, width, height, Color.Red);

        // Set the rectangles for the progress bar background and fill
        progressBarRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        progressBarFillRectangle = new Rectangle((int)position.X, (int)position.Y, 0, height); // Start with an empty fill
        previousKeyboardState = Keyboard.GetState();
        previousMouseState = Mouse.GetState();
    }

    public override void Update(GameTime gameTime)
    {
        // Get the current state of the keyboard
        KeyboardState keyboardState = Keyboard.GetState();

        // Check if the space bar is currently being pressed
        if (keyboardState.IsKeyDown(Keys.Space))
        {
            // Increase the progress while the space bar is held down
            SetProgress(progress + (float)gameTime.ElapsedGameTime.TotalSeconds); // Assuming 1 second to fully charge
        }

        // Store the previous keyboard state
        previousKeyboardState = keyboardState;

        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(progressBarTexture, progressBarRectangle, Color.White); // Draw the bar background
        spriteBatch.Draw(progressBarFillTexture, progressBarFillRectangle, Color.White); // Draw the filled part
        spriteBatch.End();

        base.Draw(gameTime);
    }

    public void SetProgress(float value)
    {
        progress = MathHelper.Clamp(value, 0.0f, 1.0f);
        progressBarFillRectangle.Width = (int)(progressBarRectangle.Width * progress);
    }

    private Texture2D CreateTexture(GraphicsDevice graphicsDevice, int width, int height, Color color)
    {
        Texture2D texture = new Texture2D(graphicsDevice, width, height);
        Color[] colorData = new Color[width * height];
        for (int i = 0; i < colorData.Length; ++i)
            colorData[i] = color;
        texture.SetData(colorData);
        return texture;
    }
}
