using App05MonoGame.Controllers;
using App05MonoGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace App05MonoGame
{
    /// <summary>
    /// This game creates a variety of sprites as an example.  
    /// There is no game to play yet. The spaceShip and the 
    /// asteroid can be used for a space shooting game, the player, 
    /// the coin and the enemy could be used for a pacman
    /// style game where the player moves around collecting
    /// random coins and the enemy tries to catch the player.
    /// </summary>
    /// <authors>
    /// Galimir Bozmarov and Max Roe
    /// </authors>
    public class App05Game : Game
    {
        #region Constants

        public const int HD_Height = 720;
        public const int HD_Width = 1280;

        #endregion

        #region Attribute

        private readonly GraphicsDeviceManager graphicsManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        private SpriteFont arialFont;
        private SpriteFont calibriFont;

        private Texture2D backgroundImage;

        private readonly CoinsController coinsController;

        private AnimatedPlayer playerSprite;
        private AnimatedSprite enemySprite;
        private AnimatedSprite enemySprite2;
        private AnimatedSprite enemySprite3;

        private Button restartButton;

        private int score;
        private int health;

        #endregion

        public App05Game()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            coinsController = new CoinsController();
        }

        /// <summary>
        /// Setup the game window size to 720P 1280 x 720 pixels
        /// Simple fixed playing area with no camera or scrolling
        /// </summary>
        protected override void Initialize()
        {
            graphicsManager.PreferredBackBufferWidth = HD_Width;
            graphicsManager.PreferredBackBufferHeight = HD_Height;

            graphicsManager.ApplyChanges();

            graphicsDevice = graphicsManager.GraphicsDevice;

            score = 0;
            health = 100;

            base.Initialize();
        }

        /// <summary>
        /// use Content to load your game images, fonts,
        /// music and sound effects
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>(
                "backgrounds/green_background720p");

            // Load Music and SoundEffects

            SoundController.LoadContent(Content);

            // Load Fonts

            arialFont = Content.Load<SpriteFont>("fonts/arial");
            calibriFont = Content.Load<SpriteFont>("fonts/calibri");

            restartButton = new Button(arialFont,
                Content.Load<Texture2D>("Controls/button-icon-png-200"))
            {
                Position = new Vector2(1100, 600),
                Text = "Quit",
                Scale = 0.6f
            };

            restartButton.click += RestartButton_click;

            // animated sprites suitable for pacman type game

            SetupAnimatedPlayer();
            SetupEnemy();
            SetupEnemy2();
            SetupEnemy3();


            Texture2D coinSheet = Content.Load<Texture2D>("Actors/coin_copper");
            coinsController.CreateCoin(graphicsDevice, coinSheet);
        }

        private void RestartButton_click(object sender, System.EventArgs e)
        {
            //TODO: do something when the button is clicked!
            
            Exit();
        }

        /// <summary>
        /// This is a Sprite with four animations for the four
        /// directions, up, down, left and right
        /// </summary>
        private void SetupAnimatedPlayer()
        {
            Texture2D sheet4x3 = Content.Load<Texture2D>("Actors/Sasuke");

            AnimationController contoller = new AnimationController(graphicsDevice, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };
            contoller.CreateAnimationGroup(keys);

            playerSprite = new AnimatedPlayer()
            {
                CanWalk = true,
                Scale = 2.0f,

                Position = new Vector2(200, 200),
                Speed = 200,
                Direction = new Vector2(1, 0),

                Rotation = MathHelper.ToRadians(0),
                RotationSpeed = 0f
            };

            contoller.AppendAnimationsTo(playerSprite);
        }

        /// <summary>
        /// This is an enemy Sprite with four animations for the four
        /// directions, up, down, left and right.  Has no intelligence!
        /// </summary>
        private void SetupEnemy()
        {
            Texture2D sheet4x3 = Content.Load<Texture2D>("Actors/rsc-sprite-sheet3");

            AnimationController manager = new AnimationController(graphicsDevice, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };

            manager.CreateAnimationGroup(keys);

            enemySprite = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 200),
                Direction = new Vector2(-1, 0),
                Speed = 200,

                Rotation = MathHelper.ToRadians(0),
            };

            manager.AppendAnimationsTo(enemySprite);
            enemySprite.PlayAnimation("Left");
        }
        /// <summary>
        /// This is an enemy Sprite with four animations for the four
        /// directions, up, down, left and right.  Has no intelligence!
        /// </summary>
        private void SetupEnemy2()
        {
            Texture2D sheet4x3 = Content.Load<Texture2D>("Actors/rsc-sprite-sheet2");

            AnimationController manager = new AnimationController(graphicsDevice, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };

            manager.CreateAnimationGroup(keys);

            enemySprite2 = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 400),
                Direction = new Vector2(-1, 0),
                Speed = 150,

                Rotation = MathHelper.ToRadians(0),
            };

            manager.AppendAnimationsTo(enemySprite2);
            enemySprite2.PlayAnimation("Left");
        }
        /// <summary>
        /// This is an enemy Sprite with four animations for the four
        /// directions, up, down, left and right.  Has no intelligence!
        /// </summary>
        private void SetupEnemy3()
        {
            Texture2D sheet4x3 = Content.Load<Texture2D>("Actors/sprite-sheet1");

            AnimationController manager = new AnimationController(graphicsDevice, sheet4x3, 4, 3);

            string[] keys = new string[] { "Down", "Left", "Right", "Up" };

            manager.CreateAnimationGroup(keys);

            enemySprite3 = new AnimatedSprite()
            {
                Scale = 2.0f,

                Position = new Vector2(1000, 600),
                Direction = new Vector2(-1, 0),
                Speed = 150,

                Rotation = MathHelper.ToRadians(0),
            };

            manager.AppendAnimationsTo(enemySprite3);
            enemySprite3.PlayAnimation("Left");
        }


        /// <summary>
        /// Called 60 frames/per second and updates the positions
        /// of all the drawable objects
        /// </summary>
        /// <param name="gameTime">
        /// Can work out the elapsed time since last call if
        /// you want to compensate for different frame rates
        /// </param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            restartButton.Update(gameTime);

            playerSprite.Update(gameTime);
            enemySprite.Update(gameTime);
            enemySprite2.Update(gameTime);
            enemySprite3.Update(gameTime);

            if (playerSprite.HasCollided(enemySprite))
            {
                playerSprite.IsActive = false;
                playerSprite.IsAlive = false;
                enemySprite.IsActive = false;
                LoadContent();
                score = 0;
            }

            if (playerSprite.HasCollided(enemySprite2))
            {
                playerSprite.IsActive = false;
                playerSprite.IsAlive = false;
                enemySprite2.IsActive = false;
                LoadContent();
                score = 0;
            }

            if (playerSprite.HasCollided(enemySprite3))
            {
                playerSprite.IsActive = false;
                playerSprite.IsAlive = false;
                enemySprite3.IsActive = false;
                LoadContent();
                score = 0;
            }

            if (gameTime.TotalGameTime.Milliseconds % 2000 == 0)
            {
                score += 10;
            }

            if (gameTime.TotalGameTime.Seconds == 8)
            {
                SetupEnemy();
                SetupEnemy2();
                SetupEnemy3();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Called 60 frames/per second and Draw all the 
        /// sprites and other drawable images here
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LawnGreen);

            spriteBatch.Begin();


            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            restartButton.Draw(spriteBatch);

            // Draw Chase game

            playerSprite.Draw(spriteBatch);
            coinsController.Draw(spriteBatch);
            enemySprite.Draw(spriteBatch);
            enemySprite2.Draw(spriteBatch);
            enemySprite3.Draw(spriteBatch);

            DrawGameStatus(spriteBatch);
            DrawGameFooter(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Display the name fo the game and the current score
        /// and health of the player at the top of the screen
        /// </summary>
        public void DrawGameStatus(SpriteBatch spriteBatch)
        {
            Vector2 topLeft = new Vector2(4, 4);
            string status = $"Score = {score:##0}";

            spriteBatch.DrawString(arialFont, status, topLeft, Color.White);

            string game = "Coin Chase";
            Vector2 gameSize = arialFont.MeasureString(game);
            Vector2 topCentre = new Vector2((HD_Width/2 - gameSize.X/2), 4);
            spriteBatch.DrawString(arialFont, game, topCentre, Color.White);

            string healthText = $"Health = {health}%";
            Vector2 healthSize = arialFont.MeasureString(healthText);
            Vector2 topRight = new Vector2(HD_Width - (healthSize.X + 4), 4);
            spriteBatch.DrawString(arialFont, healthText, topRight, Color.White);

        }

        /// <summary>
        /// Display the Module, the authors and the application name
        /// at the bottom of the screen
        /// </summary>
        public void DrawGameFooter(SpriteBatch spriteBatch)
        {
            int margin = 20;

            string names = "Galimir and Max";
            string app = "App05: MonogGame";
            string module = "BNU CO453-2020";

            Vector2 namesSize = calibriFont.MeasureString(names);
            Vector2 appSize = calibriFont.MeasureString(app);

            Vector2 bottomCentre = new Vector2((HD_Width - namesSize.X)/ 2, HD_Height - margin);
            Vector2 bottomLeft = new Vector2(margin, HD_Height - margin);
            Vector2 bottomRight = new Vector2(HD_Width - appSize.X - margin, HD_Height - margin);

            spriteBatch.DrawString(calibriFont, names, bottomCentre, Color.Yellow);
            spriteBatch.DrawString(calibriFont, module, bottomLeft, Color.Yellow);
            spriteBatch.DrawString(calibriFont, app, bottomRight, Color.Yellow);

        }
    }
}
