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
    private SpriteFont gameFont;
    private Random random = new Random();
    private SpriteFont explanationFont;
    private string gameExplanation = "To start the game hold 'SpaceBar'\n to increase the speed of the\n bird then let go at desired\n location, then click in the\n" +
        "shooting area to fire the bird\n at the boxes";
    private Texture2D tmpTex;
    public int score = 0;
    private static int totalObjectCounter = 0; 

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
                totalObjectCounter--;
                Components.Remove(box);
                score += 2;
            }
        }

        // Check collisions with barrels
        foreach (BarrelComponent barrelItem in Components.OfType<BarrelComponent>().ToList())
        {
            if (birdBounds.Intersects(barrelItem.GetBounds()))
            {
                totalObjectCounter--;
                Components.Remove(barrelItem);
                score += 3;
            }
        }

        // Check collisions with pigs
        foreach (PigComponent pigItem in Components.OfType<PigComponent>().ToList())
        {
            if (birdBounds.Intersects(pigItem.GetBounds()))
            {
                totalObjectCounter--;
                Components.Remove(pigItem);
                score += 5;
            }
        }

        // Check collisions with yellow birds
        foreach (YellowBirdComponent yellowBirdItem in Components.OfType<YellowBirdComponent>().ToList())
        {
            if (birdBounds.Intersects(yellowBirdItem.GetBounds()))
            {
                totalObjectCounter--;
                Components.Remove(yellowBirdItem);
                score += 6;
            }
        }
    }

    public PlayScene(Game game) : base(game)
    {
        Game1 g = (Game1)game;
        this.sb = g._spriteBatch;


        gameFont = g.Content.Load<SpriteFont>("Fonts/MainFont");

        currBackGround = g.Content.Load<Texture2D>("Images/startLevelBackground");
        Texture2D slingShotTexture = g.Content.Load<Texture2D>("Images/SlingShot");
        Texture2D birdTexture = g.Content.Load<Texture2D>("Images/freshBirdSprite");
        Texture2D boxTexture = g.Content.Load<Texture2D>("Images/brownBox");
        Texture2D barrelTexture = g.Content.Load<Texture2D>("Images/barrel");
        Texture2D pigTexture = g.Content.Load<Texture2D>("Images/pig");
        Texture2D yellowBirdTexture = g.Content.Load<Texture2D>("Images/yellowBird");
        Texture2D aimshotTexture = g.Content.Load<Texture2D>("Images/aimShot");
        birdAimShotTexture = g.Content.Load<Texture2D>("Images/birdAimShot");
        explanationFont = g.Content.Load<SpriteFont>("Fonts/MainFont");

        birdAimShot = new birdAimShot(game, birdAimShotTexture, 50, 50);

        Vector2 aimShotPosition = new Vector2(160, 140);
        aimShot = new AimShotComponent(game, aimShotPosition, aimshotTexture, 200, 160);

        Vector2 slingShotPosition = new Vector2(100, 220);
        slingShot = new SlingShotComponent(game, slingShotPosition, slingShotTexture, 100, 150);

        Vector2 boxSize = new Vector2(75, 75);
        Vector2 boxPosition1 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        brownBox = new BoxComponent(game, boxPosition1, boxTexture, (int)boxSize.X, (int)boxSize.Y);
        totalObjectCounter++;

        Vector2 boxPosition2 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box2 = new BoxComponent(game, boxPosition2, boxTexture, (int)boxSize.X, (int)boxSize.Y);
        totalObjectCounter++;

        Vector2 boxPosition3 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box3 = new BoxComponent(game, boxPosition3, boxTexture, (int)boxSize.X, (int)boxSize.Y);
        totalObjectCounter++;

        Vector2 boxPosition4 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
        BoxComponent box4 = new BoxComponent(game, boxPosition4, boxTexture, (int)boxSize.X, (int)boxSize.Y);
        totalObjectCounter++;

        Vector2 barrelSize = new Vector2(50, 75);
        Vector2 barrelPosition = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        barrel = new BarrelComponent(game, barrelPosition, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
        totalObjectCounter++;

        Vector2 barrelPosition2 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        BarrelComponent barrel2 = new BarrelComponent(game, barrelPosition2, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
        totalObjectCounter++;

        Vector2 barrelPosition3 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
        BarrelComponent barrel3 = new BarrelComponent(game, barrelPosition3, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
        totalObjectCounter++;

        Vector2 pigSize = new Vector2(50, 50);
        Vector2 pigPosition = GenerateRandomPosition((int)pigSize.X, (int)pigSize.Y);
        pig = new PigComponent(game, pigPosition, pigTexture, (int)pigSize.X, (int)pigSize.Y);
        totalObjectCounter++;

        Vector2 yellowSize = new Vector2(75, 50);
        Vector2 yellowBPosition = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
        yellowBird = new YellowBirdComponent(game, yellowBPosition, yellowBirdTexture, (int)yellowSize.X, (int)yellowSize.Y);
        totalObjectCounter++;


        Vector2 birdPosition = new Vector2(slingShot.Position.X + 67, slingShot.Position.Y - 5);
        Vector2 progressBarPosition = new Vector2(birdPosition.X - 150, birdPosition.Y - 110);
        progressBar = new ProgressBarComponent(game, progressBarPosition, 200, 20);
        bird = new BirdComponent(game, birdPosition, birdTexture, 105, 105, aimShot, progressBar, gameFont, slingShot);

        tmpTex = new Texture2D(GraphicsDevice, 1, 1);
        tmpTex.SetData(new[] { Color.White });

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
        Components.Add(birdAimShot);
    }


    public override void Update(GameTime gameTime)
    {
        CheckCollisions();

        MouseState mouseState = Mouse.GetState();

        // Only allow setting aimShot position if the bird has not been launched yet
        if (!bird.IsLaunched && mouseState.LeftButton == ButtonState.Pressed && aimShot.GetBounds().Contains(mouseState.Position))
        {
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 aimShotPosition = new Vector2(
                mousePosition.X - birdAimShot.ImageWidth / 2,
                mousePosition.Y - birdAimShot.ImageHeight / 2
            );

            birdAimShot.UpdatePosition(aimShotPosition);
            birdAimShot.Visible = true; 
        }
        else if (bird.IsLaunched || bird.MouseClickedAtZeroProgress)
        {
            progressBar.ResetProgress();
            birdAimShot.Visible = false; 
        }

        bird.Update(gameTime); 
        if (bird.RespawnCount >= BirdComponent.MaxRespawnLimit ||  totalObjectCounter== 0)
        {
            // Transition to the EndScene
            Game1 game = (Game1)Game;
            game.hideAllScenes();
            game.ShowEndScene(); 

        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
        sb.DrawString(gameFont, $"Total objects left: {totalObjectCounter.ToString()}", new Vector2(500, 40), Color.DarkOrange);

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

        Vector2 explanationPosition = new Vector2(20, 600);
        Vector2 explanationSize = explanationFont.MeasureString(gameExplanation);
        Rectangle backgroundRect = new Rectangle(
            (int)explanationPosition.X,
            (int)explanationPosition.Y,
            (int)explanationSize.X,
            (int)explanationSize.Y);
        sb.Draw(tmpTex, backgroundRect, Color.White);

        sb.DrawString(explanationFont, gameExplanation, explanationPosition, Color.DarkOrange);

        string scoreText = "Score: " + score;
        Vector2 scorePosition = new Vector2(600, 10);
        sb.DrawString(gameFont, scoreText, scorePosition, Color.DarkOrange);

        sb.End();

        base.Draw(gameTime);
    }
}
