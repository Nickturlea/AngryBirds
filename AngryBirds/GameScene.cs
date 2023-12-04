using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class GameScene : DrawableGameComponent
    {
        private List<GameComponent> components;
        public List<GameComponent> Components { get => components; set => components = value; }

        public virtual void show() // show scene
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public virtual void hide() // hide scene
        {
            this.Enabled = false;
            this.Visible = false;
        }

        public GameScene(Game game) : base(game)
        {
            components = new List<GameComponent>();
            hide(); // when creating the scene hide the scene initially
        }


        public override void Update(GameTime gameTime)
        {
            foreach (var item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
