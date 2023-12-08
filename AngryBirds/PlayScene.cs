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
    private PigComponent pig;
    private YellowBirdComponent yellowBird;


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
        Texture2D pigTexure= Game.Content.Load<Texture2D>("Images/pig");
        Texture2D yellowBirdTexure = Game.Content.Load<Texture2D>("Images/yellowBird");
        Texture2D powerBarSheet = Game.Content.Load<Texture2D>("Images/SpeedOfBird");




        // Initialize the SpeedBarComponent
        Vector2 powerBarPosition = new Vector2(100, 100); // Set this position to where you want the power bar on the screen

        // Initializes instances 
        Vector2 slingShotPosition = new Vector2(100, 220);
        slingShot = new SlingShotComponent(game, slingShotPosition, slingShotTexture, 100, 150);

        // Position the bird on the slingshot
        Vector2 birdPosition = new Vector2(slingShotPosition.X + 67, slingShotPosition.Y -5); // Perfact measurements for starting the bird on the slingshot, nick
        bird = new BirdComponent(game, birdPosition, birdTexture, 105, 105);


        // Position the Box on the slingshot
        Vector2 boxPosition = new Vector2(birdPosition.X + 927, birdPosition.Y + 397); // Perfact measurements for starting the bird on the slingshot, nick
        brownBox = new BoxComponent(game, boxPosition, boxTexture, 200, 200);

        // Position the Box on the slingshot
        Vector2 barrelPosition = new Vector2(boxPosition.X + 55, boxPosition.Y - 350); // Perfact measurements for starting the bird on the slingshot, nick
        barrel = new BarrelComponent(game, barrelPosition, barrelTexture, 150, 120);

        // Position the Box on the slingshot
        Vector2 pigPosition = new Vector2(birdPosition.X + 1015, barrelPosition.Y - 77); // Perfact measurements for starting the bird on the slingshot, nick
        pig = new PigComponent(game, pigPosition, pigTexure, 80, 80);

        // Position the Box on the slingshot
        Vector2 yellowBPosition = new Vector2(boxPosition.X + 5, barrelPosition.Y + 235); // Perfact measurements for starting the bird on the slingshot, nick
        yellowBird = new YellowBirdComponent(game, yellowBPosition, yellowBirdTexure, 155, 130);





        // Addinng componets 
        Components.Add(yellowBird);
        Components.Add(pig);
        Components.Add(brownBox);
        Components.Add(barrel);
        Components.Add(slingShot); 
        Components.Add(bird);

    }

    public override void Draw(GameTime gameTime)
    {
        sb.Begin();
        sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
        yellowBird.Draw(gameTime);
        pig.Draw(gameTime);
        barrel.Draw(gameTime);
        slingShot.Draw(gameTime);
        bird.Draw(gameTime);
        sb.End();

        base.Draw(gameTime);
    }

}
