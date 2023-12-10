using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class EndScene : GameScene
    {
        private SpriteBatch sb;
        private SpriteFont gameFont;
        private Texture2D currBackGround;
        private string playerName = "";
        private bool isNameEntered = false;
        private bool displayError = false;
        private string Error = "Invalid, Please enter a name between 1-12 letters total!";
        private SaveScore saveScore; 
        private const int maxNameLength = 12;
        private StringBuilder inputText = new StringBuilder();
        private KeyboardState oldKeyboardState;
        private Texture2D pixelTexture;
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MadBird.txt");

        PlayScene PlayScene { get; set; }

        public EndScene(Game game, PlayScene playScene) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            this.PlayScene = playScene; // make the Playsence not be null 
            gameFont = g.Content.Load<SpriteFont>("Fonts/MainFont");
            currBackGround = g.Content.Load<Texture2D>("Images/theENd");

            pixelTexture = new Texture2D(GraphicsDevice, 1, 1);
            pixelTexture.SetData(new[] { Color.LightSkyBlue });


            saveScore = new SaveScore(); 

        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            Keys[] keys = keyboardState.GetPressedKeys();

            if (!isNameEntered)
            {
                foreach (var key in keys)
                {
                    if (!oldKeyboardState.IsKeyDown(key))
                    {
                        if (key == Keys.Back && inputText.Length > 0)
                        {
                            inputText.Remove(inputText.Length - 1, 1);
                        }
                        else if (key >= Keys.A && key <= Keys.Z && inputText.Length < maxNameLength)
                        {
                            // Append the character to the input text
                            inputText.Append((char)(key - Keys.A + 'A'));
                        }
                        else if (key == Keys.Enter)
                        {
                            if (inputText.Length > 0 && inputText.Length <= maxNameLength)
                            {
                                isNameEntered = true;
                                playerName = inputText.ToString();
                                saveScore.SavePlayerScore(playerName, PlayScene.score);
                                inputText.Clear();
                                displayError = false; // Reset error messge 
                            }
                            else
                            {
                                displayError = true; // Error message 
                            }
                        }
                    }
                }
            }
            oldKeyboardState = keyboardState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);

            // Draw transparent background for text
            Rectangle backgroundRectangle = new Rectangle(80, 90, 400, 100); // Adjust the size to fit the text
            Color backgroundColor = new Color(173, 216, 230, 0.5f); // Light blue color with 50% opacity
            sb.Draw(pixelTexture, backgroundRectangle, backgroundColor);

            if (isNameEntered)
            {
                string scoreMessage = $"Player: {playerName} | Score: {PlayScene.score}";
                Vector2 scorePosition = new Vector2(100, 100);
                sb.DrawString(gameFont, scoreMessage, scorePosition, Color.Black);
            }
            else
            {
                string nameLabel = "Enter your name (A-Z):";
                Vector2 promptPosition = new Vector2(100, 100);
                sb.DrawString(gameFont, nameLabel, promptPosition, Color.Black);
                Vector2 nameMessageSize = gameFont.MeasureString(nameLabel);

                Vector2 namePosition = new Vector2(100, promptPosition.Y + nameMessageSize.Y + 10); // Added 10 pixels for padding
                sb.DrawString(gameFont, inputText.ToString(), namePosition, Color.Black);

                // If there is an error, display the error message
                if (displayError)
                {
                    Rectangle errorBackgroundRectangle = new Rectangle(80, 220, 730, 50); 
                    Color errorBackgroundColor = new Color(173, 216, 230, 0.5f);
                    sb.Draw(pixelTexture, errorBackgroundRectangle, errorBackgroundColor); // Draw the semi-transparent background

                    Vector2 errorPosition = new Vector2(100, 225); 
                    sb.DrawString(gameFont, Error, errorPosition, Color.Red); // Draw the error text on top
                }
            }
            sb.End();
            base.Draw(gameTime);
        }


        public void Show()
        {
            this.Visible = true;
            this.Enabled = true;
        }

    }
}
