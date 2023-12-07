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

        // Loading the proper textures 
        Texture2D slingShotTexture = Game.Content.Load<Texture2D>("Images/SlingShot");
        Texture2D birdTexture = Game.Content.Load<Texture2D>("Images/BirdSprite");


        // Initializes instances 
        Vector2 slingShotPosition = new Vector2(100, 200);
        slingShot = new SlingShotComponent(game, slingShotPosition, slingShotTexture, 100, 150);

        // Position the bird on the slingshot
        Vector2 birdPosition = new Vector2(slingShotPosition.X + 67, slingShotPosition.Y -5); // Perfact measurements for starting the bird on the slingshot, nick
        bird = new BirdComponent(game, birdPosition, birdTexture, 200, 100);

        // Addinng componets 
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
