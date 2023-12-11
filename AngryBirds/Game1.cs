using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AngryBirds
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private StartScene startScene;
        private AboutScene aboutScene;
        EndSceneLevelOne endSceneLvlOne;
        EndSceneLevelTwo endSceneLvlTwo;
        LevelTwo levelTwo;
        private PlayScene playScene;
        ScoreScene scoreScene;
        private ControlScene controlScene;
        private ContactScene contactScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1500; 
            _graphics.PreferredBackBufferHeight = 800;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.show();

            aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);


            scoreScene = new ScoreScene(this);
            this.Components.Add(scoreScene);

            playScene = new PlayScene(this);
            this.Components.Add(playScene);

            levelTwo = new LevelTwo(this);
            this.Components.Add(levelTwo);

            endSceneLvlOne = new EndSceneLevelOne(this, playScene); // Level one scene 
            this.Components.Add(endSceneLvlOne);


            endSceneLvlTwo = new EndSceneLevelTwo(this, levelTwo); // Level 2 end scene 
            this.Components.Add(endSceneLvlTwo); 


            controlScene = new ControlScene(this);
            this.Components.Add(controlScene);

            contactScene = new ContactScene(this);
            this.Components.Add(contactScene);


        }

        public void hideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    GameScene gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        public void ShowEndSceneOne() // Lvl 1 end scene 
        {
            hideAllScenes();
            endSceneLvlOne.ShowLvlOneEnd();
        }


        public void ShowLevelTwoScene() 
        {
            hideAllScenes();
            levelTwo = new LevelTwo(this);
            this.Components.Add(levelTwo);
            levelTwo.show();
        }

        public void EndLevelTwo()
        {
            // When level2 Ends 
            hideAllScenes();
            endSceneLvlTwo = new EndSceneLevelTwo(this, levelTwo); 
            this.Components.Add(endSceneLvlTwo);
            ShowEndSceneTwo();
        }

        public void ShowEndSceneTwo() // Lvl 2 end scene 
        {
            hideAllScenes();
            endSceneLvlTwo.ShowLvlTwoEnd();
        }

        public void ShowMenuScene()
        {
            hideAllScenes();
            startScene.Show();
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here

            int selectedIndex = 0;
            KeyboardState ks = Keyboard.GetState();

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.selectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    playScene.ResetGame(); // Reset and re-initialize the game state 
                    hideAllScenes();
                    playScene.show();
                }
                else if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter)) // Controls 
                {
                    hideAllScenes();
                    controlScene.show();
                   
                } 
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter)) // Contact Support / help
                {
                    hideAllScenes();
                    contactScene.show();
                }
                else if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter)) // High Scores
                {
                    hideAllScenes();
                    scoreScene.show();
                }
                else if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter)) // About 
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter)) // Quit 
                {
                    Exit();
                }
            }
            if (aboutScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (playScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (controlScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                hideAllScenes();
                startScene.show();
                }
            }
            if (contactScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }
            if (scoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    hideAllScenes();
                    startScene.show();
                }
            }


            base.Update(gameTime);
        }

     


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
