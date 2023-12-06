using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class PlayScene : GameScene
{
    private SpriteBatch sb;
    private SlingShotComponent slingShot;

    public PlayScene(Game game) : base(game)
    {
        Game1 g = (Game1)game;
        this.sb = g._spriteBatch;
        Texture2D slingShotTexture = Game.Content.Load<Texture2D>("Images/SlingShot");
        slingShot = new SlingShotComponent(game, new Vector2(100, 200), slingShotTexture, 100, 150); 
        Components.Add(slingShot);
    }

    public override void Draw(GameTime gameTime)
    {
        sb.Begin();

        slingShot.Draw(gameTime);

        sb.End();

        base.Draw(gameTime);
    }

}
