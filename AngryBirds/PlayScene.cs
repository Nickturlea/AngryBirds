using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class PlayScene: GameScene
    {
        public levelComponent levelMenu { get; set; }
        private SpriteBatch sb;
        string[] levelItems = { "Level 1", "Level 2", "Level 3"};

        public PlayScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;

            //g.Content can work too instead of game
            SpriteFont mainFont = game.Content.Load<SpriteFont>("fonts/MainFont");



            levelMenu = new levelComponent(game, this.sb, mainFont, levelItems);
            this.Components.Add(levelMenu);
        }
    }
}
