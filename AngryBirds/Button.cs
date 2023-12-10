using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AngryBirds
{
    /// <summary>
    /// To create a button, that's all, mono games does really hurt my head! 
    /// </summary>
    public class Button
    {
        // Props 
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        private SpriteFont gameFont;
        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); } }
        public string Text { get; set; }
        public SpriteFont Font { get; private set; }

        public bool IsHovering { get; private set; }
        public event EventHandler Click;


        // Constructor of the Button class
        public Button(Texture2D texture, Vector2 position, SpriteFont tmpGFont, string text)
        {
            Texture = texture;
            Position = position;
            gameFont = tmpGFont;
            Text = text;
        }


        public void Update(MouseState mouse)
        {
            IsHovering = false;

            if (Rectangle.Contains(mouse.Position))
            {
                IsHovering = true;

                if (mouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var borderColor = Color.White; // Color for the border
            var borderWidth = 2; // Width of the border
            var borderRectangle = new Rectangle(Rectangle.X - borderWidth, Rectangle.Y - borderWidth, Rectangle.Width + borderWidth * 2, Rectangle.Height + borderWidth * 2);

            // Draw border
            spriteBatch.Draw(Texture, borderRectangle, borderColor);

            // Draw button background
            spriteBatch.Draw(Texture, Rectangle, Color.White);

            // Draw button text centered
            var textSize = gameFont.MeasureString(Text);
            var textPosition = new Vector2(Rectangle.Center.X - textSize.X / 2, Rectangle.Center.Y - textSize.Y / 2);
            spriteBatch.DrawString(gameFont, Text, textPosition, Color.Black);
        }

    }
}
