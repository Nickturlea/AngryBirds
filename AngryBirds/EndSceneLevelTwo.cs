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
    internal class EndSceneLevelTwo : GameScene
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
        private const int lvl = 2;
        private StringBuilder inputText = new StringBuilder();
        private KeyboardState oldKeyboardState;
        private Button mainMenuButton;
        private Button nextLevelButton;
        private Button exitButton;
        private Texture2D tmpTex;
        private Texture2D btnTex;
        private bool currState;
        private string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MadBird.txt");

        PlayScene PlayScene { get; set; }

        public EndSceneLevelTwo(Game game, PlayScene playScene) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            this.PlayScene = playScene;
            gameFont = g.Content.Load<SpriteFont>("Fonts/MainFont");
            currBackGround = g.Content.Load<Texture2D>("Images/theEndLvlTwo");

            tmpTex = new Texture2D(GraphicsDevice, 1, 1);
            tmpTex.SetData(new[] { Color.LightSkyBlue });

            btnTex = new Texture2D(GraphicsDevice, 200, 50);
            var buttonColor = new Color[200 * 50];
            for (int i = 0; i < buttonColor.Length; i++)
                buttonColor[i] = Color.White;
            btnTex.SetData(buttonColor);

            var screenWidth = GraphicsDevice.Viewport.Width;
            var buttonWidth = tmpTex.Width;
            var buttonHeight = tmpTex.Height;
            var btnX = (screenWidth - buttonWidth) / 2;


            this.mainMenuButton = new Button(btnTex, new Vector2(btnX, 200), gameFont, "Main Menu");
            this.exitButton = new Button(btnTex, new Vector2(btnX, 400), gameFont, "Exit");


            mainMenuButton.Click += MainMenuButton_Click;
            exitButton.Click += ExitButton_Click;


            saveScore = new SaveScore();

        }

        public void ResetEndScene()
        {
            isNameEntered = false;
            currState = false;
            displayError = false;
            playerName = string.Empty;
            inputText.Clear();
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

                            inputText.Append((char)(key - Keys.A + 'A'));
                        }
                        else if (key == Keys.Enter)
                        {
                            if (inputText.Length > 0 && inputText.Length <= maxNameLength)
                            {
                                isNameEntered = true;
                                playerName = inputText.ToString();
                                saveScore.SavePlayerScore(lvl, playerName, PlayScene.currScore);
                                inputText.Clear();
                                displayError = false;
                            }
                            else
                            {
                                displayError = true;
                            }
                        }
                    }
                }
            }
            oldKeyboardState = keyboardState;
            base.Update(gameTime);

            var mouseState = Mouse.GetState();
            mainMenuButton.Update(mouseState);
            exitButton.Update(mouseState);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);


            Rectangle backgroundRectangle = new Rectangle(80, 90, 600
                , 100); // size 
            Color backgroundColor = new Color(173, 216, 230, 0.5f); // Light blue color with 50% trans
            sb.Draw(tmpTex, backgroundRectangle, backgroundColor);

            if (isNameEntered)
            {
                string scoreMessage = $"From Level {lvl}: Player: {playerName} | Score: {PlayScene.currScore}";
                Vector2 scorePosition = new Vector2(100, 100);
                sb.DrawString(gameFont, scoreMessage, scorePosition, Color.Black);
                // Draw the buttons
                mainMenuButton.Draw(sb);
                exitButton.Draw(sb);
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
                    sb.Draw(tmpTex, errorBackgroundRectangle, errorBackgroundColor);

                    Vector2 errorPosition = new Vector2(100, 225);
                    sb.DrawString(gameFont, Error, errorPosition, Color.Red);
                }
            }
            sb.End();
            base.Draw(gameTime);
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            Game1 game = (Game1)Game;
            game.hideAllScenes();
            game.ShowMenuScene();
            ResetEndScene();
            currState = true;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Game1 game = (Game1)Game;
            game.Exit();
        }

        public void ShowLvlTwoEnd()
        {
            this.Visible = true;
            this.Enabled = true;
        }


    }
}
