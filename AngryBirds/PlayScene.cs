using AngryBirds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

internal class PlayScene : GameScene
{
    private SpriteBatch sb;
    private Texture2D currBackGround;
    private Texture2D birdAimShotTexture; 
    private SlingShotComponent slingShot;
    private BirdComponent bird;
    private BoxComponent brownBox;
    private BarrelComponent barrel;
    private ProgressBarComponent progressBar;
    private PigComponent pig;
    private birdAimShot birdAimShot;
    private YellowBirdComponent yellowBird;
    private AimShotComponent aimShot;
    private Random random = new Random();


    private Vector2 GenerateRandomPosition(int width, int height)
    {
        int minX = (int)(Shared.stage.X / 2);
        int maxX = (int)Shared.stage.X - width;

        int minY = 0;
        int maxY = (int)Shared.stage.Y - height; 

        int x = random.Next(minX, maxX + 1);
        int y = random.Next(minY, maxY + 1);

        return new Vector2(x, y);
    }

    private void CheckCollisions()
    {
        Rectangle birdBounds = bird.GetBounds();

        // Check collisions with boxes
        foreach (BoxComponent box in Components.OfType<BoxComponent>().ToList())
        {
            if (birdBounds.Intersects(box.GetBounds()))
            {
                Components.Remove(box);
            }
        }

        // Check collisions with barrels
        foreach (BarrelComponent barrelItem in Components.OfType<BarrelComponent>().ToList())
        {
            if (birdBounds.Intersects(barrelItem.GetBounds()))
            {
                Components.Remove(barrelItem);
            }
        }

        // Check collisions with pigs
        foreach (PigComponent pigItem in Components.OfType<PigComponent>().ToList())
        {
            if (birdBounds.Intersects(pigItem.GetBounds()))
            {
                Components.Remove(pigItem);
            }
        }

        // Check collisions with yellow birds
        foreach (YellowBirdComponent yellowBirdItem in Components.OfType<YellowBirdComponent>().ToList())
        {
            if (birdBounds.Intersects(yellowBirdItem.GetBounds()))
            {
                Components.Remove(yellowBirdItem);
            }
        }
    }




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
        Texture2D aimshotTex = Game.Content.Load<Texture2D>("Images/aimShot");
        Texture2D birdAimShotTexture = Game.Content.Load<Texture2D>("Images/birdAimShot");


        birdAimShot = new birdAimShot(game, birdAimShotTexture, 50, 50);


        Vector2 aimShotPosition = new Vector2(200, -40);
        aimShot = new AimShotComponent(game, aimShotPosition, aimshotTex, 200, 500);

        // Initializes instances 
        Vector2 slingShotPosition = new Vector2(100, 220);
        slingShot = new SlingShotComponent(game, slingShotPosition, slingShotTexture, 100, 150);

        
        Vector2 boxSize = new Vector2(75, 75);
        Vector2 boxPosition1 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        brownBox = new BoxComponent(game, boxPosition1, boxTexture, (int)boxSize.X, (int)boxSize.Y);


        Vector2 boxPosition2 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box2 = new BoxComponent(game, boxPosition2, boxTexture, (int)boxSize.X, (int)boxSize.Y);


        Vector2 boxPosition3 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box3 = new BoxComponent(game, boxPosition3, boxTexture, (int)boxSize.X, (int)boxSize.Y);


        Vector2 boxPosition4 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box4 = new BoxComponent(game, boxPosition4, boxTexture, (int)boxSize.X, (int)boxSize.Y);



        Vector2 barrelSize = new Vector2(50, 75);
        Vector2 barrelPosition = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        barrel = new BarrelComponent(game, barrelPosition, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);

        Vector2 barrelPosition2 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        BarrelComponent barrel2 = new BarrelComponent(game, barrelPosition2, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);

        Vector2 barrelPosition3 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        BarrelComponent barrel3 = new BarrelComponent(game, barrelPosition3, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);


        Vector2 pigSize = new Vector2(50, 50);
        Vector2 pigPosition = GenerateRandomPosition((int)pigSize.X, (int)pigSize.Y);
        pig = new PigComponent(game, pigPosition, pigTexure, (int)pigSize.X, (int)pigSize.Y);


        Vector2 yellowSize = new Vector2(75, 50);
        Vector2 yellowBPosition = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
        yellowBird = new YellowBirdComponent(game, yellowBPosition, yellowBirdTexure, (int)yellowSize.X, (int)yellowSize.Y);

        Vector2 yellowBPosition2 = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
        YellowBirdComponent yellowBird2 = new YellowBirdComponent(game, yellowBPosition2, yellowBirdTexure, (int)yellowSize.X, (int)yellowSize.Y);


        Vector2 birdPosition = new Vector2(slingShotPosition.X + 67, slingShotPosition.Y - 5);
        Vector2 progressBarPosition = new Vector2(birdPosition.X - 150, birdPosition.Y - 110); 
        progressBar = new ProgressBarComponent(game, progressBarPosition, 200, 20);
        bird = new BirdComponent(game, birdPosition, birdTexture, 105, 105, aimShot, progressBar);

        Components.Add(progressBar); 
        Components.Add(yellowBird);
        Components.Add(pig);
        Components.Add(aimShot);
        Components.Add(brownBox);
        Components.Add(barrel);
        Components.Add(slingShot); 
        Components.Add(bird);
        Components.Add(box2);
        Components.Add(box3);
        Components.Add(box4);
        Components.Add(barrel2);
        Components.Add(barrel3);
        Components.Add(yellowBird2);
        Components.Add(birdAimShot);

    }
    public override void Update(GameTime gameTime)
    {
        CheckCollisions();

        MouseState mouseState = Mouse.GetState();


        if (mouseState.LeftButton == ButtonState.Pressed && aimShot.GetBounds().Contains(mouseState.Position))
        {

            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 aimShotPosition = new Vector2(
                mousePosition.X - birdAimShot.ImageWidth / 2,
                mousePosition.Y - birdAimShot.ImageHeight / 2
            );

            birdAimShot.UpdatePosition(aimShotPosition);
            birdAimShot.Visible = true; 
        }
        else
        {
            birdAimShot.Visible = false; 
        }

        CheckCollisions();

        base.Update(gameTime);
    }




    public override void Draw(GameTime gameTime)
    {
        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        // Draw the background and other components
        sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);

        // Draw aimShot only when the left mouse button is pressed
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            birdAimShot.Draw(gameTime);
        }
        progressBar.Draw(gameTime);
        yellowBird.Draw(gameTime);
        pig.Draw(gameTime);
        barrel.Draw(gameTime);
        slingShot.Draw(gameTime);
        bird.Draw(gameTime);
        aimShot.Draw(gameTime);
        sb.End();

        base.Draw(gameTime);
    }



}
