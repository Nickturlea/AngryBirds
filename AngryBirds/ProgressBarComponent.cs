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

    public float Progress
    {
        get { return progress; }
        set { progress = MathHelper.Clamp(value, 0.0f, 1.0f); }
    }

    public ProgressBarComponent(Game game, Vector2 position, int width, int height)
        : base(game)
    {
        spriteBatch = new SpriteBatch(game.GraphicsDevice);

        progressBarTexture = CreateTexture(game.GraphicsDevice, width, height, Color.LightSkyBlue);
        progressBarFillTexture = CreateTexture(game.GraphicsDevice, width, height, Color.Red);
        progressBarRectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        progressBarFillRectangle = new Rectangle((int)position.X, (int)position.Y, 0, height);
        previousKeyboardState = Keyboard.GetState();
        previousMouseState = Mouse.GetState();
    }

    public override void Update(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Space))
        {
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
