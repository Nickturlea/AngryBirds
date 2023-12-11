using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace AngryBirds
{
    internal class ScoreScene : GameScene
    {
        private SpriteBatch sb;
        private Texture2D backgroudAboutTexture;
        private SpriteFont mainFont;
        private string[] scoreLines; // Array to hold the lines of the score file

        public ScoreScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;
            backgroudAboutTexture = g.Content.Load<Texture2D>("Images/AboutSceneBackground");
            mainFont = g.Content.Load<SpriteFont>("fonts/MainFont");

            // Direct path to the file in the Documents folder
            string fileName = "MadBirdsScores.txt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);


            try
            {
                // Read the file content
                if (File.Exists(filePath))
                {
                    scoreLines = File.ReadAllLines(filePath);
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

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            sb.Draw(backgroudAboutTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.Red);

            Vector2 textPosition = new Vector2(50, 50);

            if (scoreLines != null)
            {
                foreach (string line in scoreLines)
                {
                    sb.DrawString(mainFont, line, textPosition, Color.Red);
                    textPosition.Y += mainFont.LineSpacing;
                }
            }
            else
            {
                sb.DrawString(mainFont, "No scores found.", textPosition, Color.Red);
            }

            sb.End();
            base.Draw(gameTime);
        }
    }
}
