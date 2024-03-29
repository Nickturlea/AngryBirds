﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AngryBirds
{
    internal class LevelTwo : GameScene
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
        private EvilBirdComponent evil;
        private Texture2D tmpTex;
        private int score = 0;
        private static int totalObjectCounter = 0;
        private SoundEffect boomSound;


        public int CurrScore { get; set; }


        public void AddScore(int points)
        {
            score += points;
            CurrScore = score; // Update the CurrScore property
        }


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
                    boomSound.Play();
                    AddScore(1);
                }
            }

            // Check collisions with barrels
            foreach (BarrelComponent barrelItem in Components.OfType<BarrelComponent>().ToList())
            {
                if (birdBounds.Intersects(barrelItem.GetBounds()))
                {
                    totalObjectCounter--;
                    Components.Remove(barrelItem);
                    boomSound.Play();
                    AddScore(1);
                }
            }

            // Check collisions with pigs
            foreach (PigComponent pigItem in Components.OfType<PigComponent>().ToList())
            {
                if (birdBounds.Intersects(pigItem.GetBounds()))
                {
                    totalObjectCounter--;
                    Components.Remove(pigItem);
                    boomSound.Play();
                    AddScore(3);
                }
            }

            // Check collisions with yellow birds
            foreach (YellowBirdComponent yellowBirdItem in Components.OfType<YellowBirdComponent>().ToList())
            {
                if (birdBounds.Intersects(yellowBirdItem.GetBounds()))
                {
                    totalObjectCounter--;
                    Components.Remove(yellowBirdItem);
                    boomSound.Play();
                    AddScore(4);
                }
            }
            // Check collisions with yellow birds
            foreach (EvilBirdComponent evilBird in Components.OfType<EvilBirdComponent>().ToList())
            {
                if (birdBounds.Intersects(evilBird.GetBounds()))
                {
                    totalObjectCounter--;
                    Components.Remove(evilBird);
                    boomSound.Play();
                    AddScore(4);
                }
            }


        }


        private void InitializeComponents()
        { 
            //'Game' is a property of the base class that points to the current Game instance.
            Game1 g = (Game1)this.Game; // Use 'this.Game' to access the Game instance.
            this.sb = g._spriteBatch;


            gameFont = g.Content.Load<SpriteFont>("Fonts/MainFont");

            currBackGround = g.Content.Load<Texture2D>("Images/levelTwoBackground");
            Texture2D slingShotTexture = g.Content.Load<Texture2D>("Images/SlingShot");
            Texture2D birdTexture = g.Content.Load<Texture2D>("Images/freshBirdSprite");
            Texture2D boxTexture = g.Content.Load<Texture2D>("Images/brownBox");
            Texture2D barrelTexture = g.Content.Load<Texture2D>("Images/barrel");
            Texture2D pigTexture = g.Content.Load<Texture2D>("Images/pig");
            Texture2D yellowBirdTexture = g.Content.Load<Texture2D>("Images/yellowBird");
            Texture2D aimshotTexture = g.Content.Load<Texture2D>("Images/aimShot");
            birdAimShotTexture = g.Content.Load<Texture2D>("Images/birdAimShot");
            Texture2D evilBirdTexture = g.Content.Load<Texture2D>("Images/BirdSprite");
            SoundEffect launchSound = g.Content.Load<SoundEffect>("Music/birdLaunch");


            birdAimShot = new birdAimShot(this.Game, birdAimShotTexture, 50, 50);


            Vector2 aimShotPosition = new Vector2(160, 140);
            aimShot = new AimShotComponent(this.Game, aimShotPosition, aimshotTexture, 200, 160);

            Vector2 slingShotPosition = new Vector2(100, 220);
            slingShot = new SlingShotComponent(this.Game, slingShotPosition, slingShotTexture, 100, 150);

            Vector2 boxSize = new Vector2(75, 75);
            Vector2 boxPosition1 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
            brownBox = new BoxComponent(this.Game, boxPosition1, boxTexture, (int)boxSize.X, (int)boxSize.Y);
            totalObjectCounter++;

            Vector2 boxPosition2 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
            BoxComponent box2 = new BoxComponent(this.Game, boxPosition2, boxTexture, (int)boxSize.X, (int)boxSize.Y);
            totalObjectCounter++;

            Vector2 boxPosition3 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
            BoxComponent box3 = new BoxComponent(this.Game, boxPosition3, boxTexture, (int)boxSize.X, (int)boxSize.Y);
            totalObjectCounter++;

            Vector2 boxPosition4 = GenerateRandomPosition((int)boxSize.X, (int)boxSize.Y);
            BoxComponent box4 = new BoxComponent(this.Game, boxPosition4, boxTexture, (int)boxSize.X, (int)boxSize.Y);
            totalObjectCounter++;

            Vector2 barrelSize = new Vector2(50, 75);
            Vector2 barrelPosition = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
            barrel = new BarrelComponent(this.Game, barrelPosition, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
            totalObjectCounter++;

            Vector2 barrelPosition2 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
            BarrelComponent barrel2 = new BarrelComponent(this.Game, barrelPosition2, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
            totalObjectCounter++;

            Vector2 barrelPosition3 = GenerateRandomPosition((int)barrelSize.X, (int)barrelSize.Y);
            BarrelComponent barrel3 = new BarrelComponent(this.Game, barrelPosition3, barrelTexture, (int)barrelSize.X, (int)barrelSize.Y);
            totalObjectCounter++;

            Vector2 pigSize = new Vector2(50, 50);
            Vector2 pigPosition = GenerateRandomPosition((int)pigSize.X, (int)pigSize.Y);
            pig = new PigComponent(this.Game, pigPosition, pigTexture, (int)pigSize.X, (int)pigSize.Y);
            totalObjectCounter++;

            Vector2 yellowSize = new Vector2(75, 50);
            Vector2 yellowBPosition = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
            yellowBird = new YellowBirdComponent(this.Game, yellowBPosition, yellowBirdTexture, (int)yellowSize.X, (int)yellowSize.Y);
            totalObjectCounter++;

            Vector2 birdPosition = new Vector2(slingShot.Position.X + 67, slingShot.Position.Y - 5);
            Vector2 progressBarPosition = new Vector2(birdPosition.X - 150, birdPosition.Y - 110);
            progressBar = new ProgressBarComponent(this.Game, progressBarPosition, 200, 20);

            bird = new BirdComponent(this.Game, birdPosition, birdTexture, 75, 75, aimShot, progressBar, gameFont, slingShot, launchSound);

            Vector2 evilBirdPosition = new Vector2(1200, 550);
            evil = new EvilBirdComponent(this.Game, evilBirdPosition, evilBirdTexture, 50, 50);
            totalObjectCounter++;


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
            Components.Add(evil);
        }


        public void ResetGame()
        {
            CurrScore = 0;
            score = 0;
            totalObjectCounter = 0;
            // Clear existing components
            Components.Clear();
            // Re-initialize all components
            InitializeComponents();
        }



        public LevelTwo(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            this.sb = g._spriteBatch;


            gameFont = g.Content.Load<SpriteFont>("Fonts/MainFont");

            currBackGround = g.Content.Load<Texture2D>("Images/levelTwoBackground");
            Texture2D slingShotTexture = g.Content.Load<Texture2D>("Images/SlingShot");
            Texture2D birdTexture = g.Content.Load<Texture2D>("Images/freshBirdSprite");
            Texture2D boxTexture = g.Content.Load<Texture2D>("Images/brownBox");
            Texture2D barrelTexture = g.Content.Load<Texture2D>("Images/barrel");
            Texture2D pigTexture = g.Content.Load<Texture2D>("Images/pig");
            Texture2D yellowBirdTexture = g.Content.Load<Texture2D>("Images/yellowBird");
            Texture2D aimshotTexture = g.Content.Load<Texture2D>("Images/aimShot");
            birdAimShotTexture = g.Content.Load<Texture2D>("Images/birdAimShot");
            Texture2D evilBirdTexture = g.Content.Load<Texture2D>("Images/BirdSprite");
            SoundEffect launchSound = g.Content.Load<SoundEffect>("Music/birdLaunch");
            boomSound = g.Content.Load<SoundEffect>("Music/Explosion");


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

            bird = new BirdComponent(game, birdPosition, birdTexture, 75, 75, aimShot, progressBar, gameFont, slingShot, launchSound);

            Vector2 evilBirdPosition = new Vector2(1200, 550);
            evil = new EvilBirdComponent(game, evilBirdPosition, evilBirdTexture, 50, 50);
            totalObjectCounter++;


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
            Components.Add(evil);
        }

        public override void Update(GameTime gameTime)
        {
            CheckCollisions();

            MouseState mouseState = Mouse.GetState();

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
                birdAimShot.Visible = false; // Hide the aimShot after the bird is launched or if there's no power.
            }

            // Update bird component logic
            bird.Update(gameTime); 
            if (bird.RespawnCount >= BirdComponent.MaxRespawnLimit || totalObjectCounter == 0)
            {
                // Transition to the EndScene
                Game1 game = (Game1)Game;
                game.hideAllScenes();
                game.EndLevelTwo();
                CurrScore = score; // Current score 
                game.ShowEndSceneTwo();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
            sb.DrawString(gameFont, $"Total Objectsc Left: {totalObjectCounter.ToString()}", new Vector2(525, 40), Color.DarkOrange);


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
            evil.Draw(gameTime);

            string scoreText = "Score: " + score;
            Vector2 scorePosition = new Vector2(600, 10);

            sb.DrawString(gameFont, scoreText, scorePosition, Color.DarkOrange);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
