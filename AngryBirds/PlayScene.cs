using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class PlayScene : GameScene
{
    private SpriteBatch sb;
    private Texture2D currBackGround;
    private SlingShotComponent slingShot;
    private BirdComponent bird;
    private BoxComponent brownBox;
    private BarrelComponent barrel;


    public PlayScene(Game game) : base(game)
    {
        Game1 g = (Game1)game;
        this.sb = g._spriteBatch;

        // Loading the proper textures 
        currBackGround = Game.Content.Load<Texture2D>("Images/startLevelBackground");
        Texture2D slingShotTexture = Game.Content.Load<Texture2D>("Images/SlingShot");
        Texture2D birdTexture = Game.Content.Load<Texture2D>("Images/BirdSprite");
        Texture2D boxTexture = Game.Content.Load<Texture2D>("Images/brownBox");
        Texture2D barrelTexture = Game.Content.Load<Texture2D>("Images/barrel");




        // Initializes instances 
        Vector2 slingShotPosition = new Vector2(100, 220);
        slingShot = new SlingShotComponent(game, slingShotPosition, slingShotTexture, 100, 150);

        // Position the bird on the slingshot
        Vector2 birdPosition = new Vector2(slingShotPosition.X + 67, slingShotPosition.Y -5); // Perfact measurements for starting the bird on the slingshot, nick
        bird = new BirdComponent(game, birdPosition, birdTexture, 200, 100);


        // Position the Box on the slingshot
        Vector2 boxPosition = new Vector2(birdPosition.X + 927, birdPosition.Y + 397); // Perfact measurements for starting the bird on the slingshot, nick
        brownBox = new BoxComponent(game, boxPosition, boxTexture, 200, 200);

        // Position the Box on the slingshot
        Vector2 barrelPosition = new Vector2(boxPosition.X + 20, boxPosition.Y - 90); // Perfact measurements for starting the bird on the slingshot, nick
        barrel = new BarrelComponent(game, barrelPosition, barrelTexture, 200, 200);


        // Addinng componets 
        Components.Add(brownBox);
        Components.Add(barrel);
        Components.Add(slingShot);
        Components.Add(bird);
    }

    public override void Draw(GameTime gameTime)
    {
        sb.Begin();
        sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
        barrel.Draw(gameTime);
        slingShot.Draw(gameTime);
        bird.Draw(gameTime);
        sb.End();

        base.Draw(gameTime);
    }

}
