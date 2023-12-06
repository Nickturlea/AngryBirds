using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class PlayScene : GameScene
{
    private SpriteBatch sb;
    private SlingShotComponent slingShot;
    private BirdComponent bird;

    public PlayScene(Game game) : base(game)
    {
        Game1 g = (Game1)game;
        this.sb = g._spriteBatch;
        Texture2D slingShotTexture = Game.Content.Load<Texture2D>("Images/SlingShot");
        Texture2D birdTexture = Game.Content.Load<Texture2D>("Images/Bird");
        slingShot = new SlingShotComponent(game, new Vector2(100, 200), slingShotTexture, 100, 150);
        bird = new BirdComponent(game, new Vector2(200, 100), slingShotTexture, 50, 50);
        Components.Add(slingShot);
        Components.Add(bird);
    }

    public override void Draw(GameTime gameTime)
    {
        sb.Begin();

        slingShot.Draw(gameTime);
        bird.Draw(gameTime);

        sb.End();

        base.Draw(gameTime);
    }

}
