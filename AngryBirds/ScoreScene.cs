using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AngryBirds
{
    internal class ScoreScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D backgroudScoreTexture;
        private SpriteFont mainFont;
        private Texture2D textBackgroundTexture; // Texture for text background
        private List<string> allScoreLines = new List<string>();


        public ScoreScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            backgroudScoreTexture = g.Content.Load<Texture2D>("Images/Scores");
            mainFont = g.Content.Load<SpriteFont>("fonts/MainFont");

            // Create a 1x1 texture for a semi-transparent background
            textBackgroundTexture = new Texture2D(GraphicsDevice, 1, 1);
            textBackgroundTexture.SetData(new Color[] { new Color(255, 255, 255, 170) }); // White color, with 65% trans


            try
            {
                // Relative path from the current working directory to the file.
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string filePath = Path.Combine(desktopPath, "LastCommited", "MadBirdScores.txt");

                // Read the file content
                if (File.Exists(filePath))
                {
                    // Use ReadAllLines which reads all the lines at once into an array
                    string[] allLines = File.ReadAllLines(filePath);

                    // Process each line
                    foreach (string line in allLines)
                    {
                        // Add each line to your allScoreLines list
                        allScoreLines.Add(line);
                    }
                }
                else
                {
                    Console.WriteLine("File not found: " + filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the file: " + ex.Message);
            }
        }


        public void LoadScores()
        {
            allScoreLines.Clear();
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string filePath = Path.Combine(desktopPath, "LastCommited", "MadBirdScores.txt");

            if (File.Exists(filePath))
            {
                string[] allLines = File.ReadAllLines(filePath);
                allScoreLines.AddRange(allLines);
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }
        }

        public void Show()
        {
            this.Visible = true;
            this.Enabled = true;
            LoadScores(); // Refresh the scores list
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            sb.Draw(backgroudScoreTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

            // Calculate the position and size for the background of the scores
            Vector2 scorePosition = new Vector2(50, 180);
            float lineWidth = 0f;
            foreach (string line in allScoreLines)
            {
                Vector2 size = mainFont.MeasureString(line);
                lineWidth = Math.Max(lineWidth, size.X);
            }
            Rectangle scoreBackgroundRect = new Rectangle((int)scorePosition.X, (int)scorePosition.Y, (int)lineWidth, allScoreLines.Count * mainFont.LineSpacing);

            // Draw the semi-transparent background
            sb.Draw(textBackgroundTexture, scoreBackgroundRect, Color.Black * 0.95f);

            // Draw each line of scores
            foreach (string line in allScoreLines)
            {
                sb.DrawString(mainFont, line, scorePosition, Color.White);
                scorePosition.Y += mainFont.LineSpacing;
            }

            sb.End();
            base.Draw(gameTime);
        }
    }
    
}

