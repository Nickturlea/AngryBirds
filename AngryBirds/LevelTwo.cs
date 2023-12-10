﻿using Microsoft.Xna.Framework;
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
        private int counterOfAnimals;
        private SpriteFont gameFont;
        private Random random = new Random();
        private SpriteFont explanationFont;
        private string gameExplanation = "To start the game hold 'SpaceBar'\n to increase the speed of the\n bird then let go at desired\n location, then click in the\n" +
            "shooting area to fire the bird\n at the boxes";
        private Texture2D tmpTex;
        public int score = 0;


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
                    score += 1;
                }
            }

            // Check collisions with barrels
            foreach (BarrelComponent barrelItem in Components.OfType<BarrelComponent>().ToList())
            {
                if (birdBounds.Intersects(barrelItem.GetBounds()))
                {
                    Components.Remove(barrelItem);
                    score += 1;
                }
            }

            // Check collisions with pigs
            foreach (PigComponent pigItem in Components.OfType<PigComponent>().ToList())
            {
                if (birdBounds.Intersects(pigItem.GetBounds()))
                {
                    counterOfAnimals--;
                    Components.Remove(pigItem);
                    score += 5;
                }
            }

            // Check collisions with yellow birds
            foreach (YellowBirdComponent yellowBirdItem in Components.OfType<YellowBirdComponent>().ToList())
            {
                if (birdBounds.Intersects(yellowBirdItem.GetBounds()))
                {
                    counterOfAnimals--;
                    Components.Remove(yellowBirdItem);
                    score += 10;
                }
            }
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
            explanationFont = g.Content.Load<SpriteFont>("Fonts/MainFont");

            birdAimShot = new birdAimShot(game, birdAimShotTexture, 50, 50);

            Vector2 aimShotPosition = new Vector2(200, 65);
            aimShot = new AimShotComponent(game, aimShotPosition, aimshotTexture, 100, 300);

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
            pig = new PigComponent(game, pigPosition, pigTexture, (int)pigSize.X, (int)pigSize.Y);
            counterOfAnimals++;

            Vector2 yellowSize = new Vector2(75, 50);
            Vector2 yellowBPosition = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
            yellowBird = new YellowBirdComponent(game, yellowBPosition, yellowBirdTexture, (int)yellowSize.X, (int)yellowSize.Y);
            counterOfAnimals++;

            Vector2 yellowBPosition2 = GenerateRandomPosition((int)yellowSize.X, (int)yellowSize.Y);
            YellowBirdComponent yellowBird2 = new YellowBirdComponent(game, yellowBPosition2, yellowBirdTexture, (int)yellowSize.X, (int)yellowSize.Y);
            counterOfAnimals++;

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
            Components.Add(yellowBird2);
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
                birdAimShot.Visible = true; // Only show the aimShot when it's first set
            }
            else if (bird.IsLaunched || bird.MouseClickedAtZeroProgress)
            {
                progressBar.ResetProgress();
                birdAimShot.Visible = false; // Hide the aimShot after the bird is launched or if there's no power.
            }

            // Update bird component logic
            bird.Update(gameTime); // This call will handle the bird's launching and respawning logic.
                                   // Check if the bird shots left using teh respawn count and the counter animals in case they get rid of all the bird 
            if (bird.RespawnCount >= BirdComponent.MaxRespawnLimit || counterOfAnimals == 0)
            {
                // Transition to the EndScene
                Game1 game = (Game1)Game;
                game.hideAllScenes();
                game.ShowEndScene(); // Make sure this method exists in your Game1 class
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // Start putting images on the screen, with normal layer order and see-through parts
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            sb.Draw(currBackGround, new Rectangle(0, 0, (int)Shared.stage.X, (int)Shared.stage.Y), Color.White);
            sb.DrawString(gameFont, $"Remaining Animals: {counterOfAnimals.ToString()}", new Vector2(525, 40), Color.Green);

            // If the left mouse button is pressed, draw the bird aim shot at its current location.
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


            // Display the current score at the top of the screen
            string scoreText = "Score: " + score;
            Vector2 scorePosition = new Vector2(600, 10);

            sb.DrawString(gameFont, scoreText, scorePosition, Color.Gold);

            // End the batch of draw calls and render everything to the screen
            sb.End();

            // Call the base method to complete any additional drawing by the base class
            base.Draw(gameTime);
        }
    }
}