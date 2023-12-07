using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class StartScene : GameScene
    {
        public MenuComponent Menu { get; set; }
        private SpriteBatch sb;
        private Texture2D backgroundmenuTexture;
        string[] menuItems = { "Start Game", "Controls", "Contact Support / Help", "High Score", "About", "Quit"}; 

        public StartScene(Game game) : base(game)
        {

            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;

            backgroundmenuTexture = game.Content.Load<Texture2D>("images/MadBirdsMenu");
            SpriteFont mainFont = game.Content.Load<SpriteFont>("fonts/MainFont");
            SpriteFont menuFont = game.Content.Load<SpriteFont>("fonts/MenuFont");



            Menu = new MenuComponent(game, this.sb, mainFont, menuFont, menuItems, backgroundmenuTexture);
            this.Components.Add(Menu);
        }

    }
}
