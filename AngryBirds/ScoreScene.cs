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

            // The file name is 'AngryBirdsScores.txt' and is located on the Desktop / downloaded file is needed 
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "AngryBirdsScores.txt");

            // Read the file content  
            if (File.Exists(filePath))
            {
                scoreLines = File.ReadAllLines(filePath);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            // Making image full screen
            int screenWidth = GraphicsDevice.Viewport.Width;
            int screenHeight = GraphicsDevice.Viewport.Height;
            sb.Draw(backgroudAboutTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.Red);

            // Starting position for the text
            Vector2 textPosition = new Vector2(50,50);

            // Need validation 
            if (scoreLines != null)
            {
                foreach (string line in scoreLines)
                {
                    sb.DrawString(mainFont, line, textPosition, Color.Red);
                    // Move the text position down for the next line
                    textPosition.Y += mainFont.LineSpacing;
                }
            }

            sb.End();
            base.Draw(gameTime);
        }
    }
}
