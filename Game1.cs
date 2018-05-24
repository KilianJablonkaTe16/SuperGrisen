using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 


    enum Gamestates { inGame, startmenu, pausemenu, shopmenu, levelmenu, gameOverMenu, exitgame }



    public class Game1 : Game
    {
        SoundEffect effect;
        

        Gamestates gamestates = new Gamestates();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont buyJump;
        Level1 lvl1;
        Camera camera = new Camera();
        public static Random rng = new Random();


        public Texture2D background, startmenuTexture, pausemenuTexture, shopmenuTexture, playButton, playButtonActive, shopButton,
                  shopButtonActive, exitButton, exitButtonActive, resumeButton, resumeButtonActive, leaveButton, leaveButtonActive,
                  buyButton, buyButtonActive, backButton, backButtonActive, flyingsprite, level1Texture, level2Texture,
<<<<<<< HEAD
                  level3Texture, level4Texture, damagesprite, groundBlockTexture, munkSprite, levelmenuBackground, level1ZoomTexture, playerSprite, healthTexture, levelOneTexture, grenSprite, levelOneTextureActive;
=======
                  level3Texture, level4Texture, damagesprite, groundBlockTexture, munkSprite, levelmenuBackground, level1ZoomTexture,
                  playerSprite, healthTexture, levelOneTexture, grenSprite, levelOneTextureActive;
>>>>>>> 97348797a00f7a208e4ce2e351492e2b2ae2d875


        Vector2 backgroundTest;
        float backgroundWidth;
           

        //Instanser av klasser
        #region Klassinstanser
        Startmenu startmenu;
        Shopmenu shopmenu;
        Pausemenu pausemenu;
        Player player;
        LevelMenu levelMenu;
        GameOverMenu gameOverMenu;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            gamestates = Gamestates.startmenu;

            // Inladning av alla textures
            #region Inladning av textures

            playerSprite = Content.Load<Texture2D>("piggy");
            Texture2D playerCrouch = Content.Load<Texture2D>("Crouch");
            healthTexture = Content.Load<Texture2D>("health");
            grenSprite = Content.Load<Texture2D>("GREN");
            flyingsprite = Content.Load<Texture2D>("flygande_gren");
            groundBlockTexture = Content.Load<Texture2D>("srort_block");
            damagesprite = Content.Load<Texture2D>("Mario_runing");
            background = Content.Load<Texture2D>("Forest-31");
            munkSprite = Content.Load<Texture2D>("munk");
            backgroundWidth = background.Width;
            backgroundTest = new Vector2(0, 0);

            //Meny bakgrunder
            #region Meny Bakgrunder
            startmenuTexture = Content.Load<Texture2D>("title_screen_almost");
            shopmenuTexture = Content.Load<Texture2D>("new_shop_screen");
            pausemenuTexture = Content.Load<Texture2D>("liten_pausescreen_test");
            levelmenuBackground = Content.Load<Texture2D>("levelmeny_bakgrund_utan");
            Texture2D gameOverMenuTexture = Content.Load<Texture2D>("new_gameOver_screen");
            #endregion

            //Alla knappterturers
            #region Knapptexturers
            playButton = Content.Load<Texture2D>("playButton");
            playButtonActive = Content.Load<Texture2D>("playButton_active");

            shopButton = Content.Load<Texture2D>("shopButton");
            shopButtonActive = Content.Load<Texture2D>("shopButton_active");

            exitButton = Content.Load<Texture2D>("exitButton");
            exitButtonActive = Content.Load<Texture2D>("exitButton_active");

            resumeButton = Content.Load<Texture2D>("resumeButton");
            resumeButtonActive = Content.Load<Texture2D>("resumeButton_active");

            leaveButton = Content.Load<Texture2D>("leaveButton");
            leaveButtonActive = Content.Load<Texture2D>("leaveButton_active");

            backButton = Content.Load<Texture2D>("backButton");
            backButtonActive = Content.Load<Texture2D>("backButton_active");

            buyButton = Content.Load<Texture2D>("buyButton");
            buyButtonActive = Content.Load<Texture2D>("buyButton_active");

            level1Texture = Content.Load<Texture2D>("level-1");
            level1ZoomTexture = Content.Load<Texture2D>("level-1-zoom");

            Texture2D easyButton = Content.Load<Texture2D>("easy_button");
            Texture2D easyButtonActive = Content.Load<Texture2D>("easy_button_active");

            Texture2D normalButton = Content.Load<Texture2D>("normal_button");
            Texture2D normalButtonActive = Content.Load<Texture2D>("normal_button_active");

            Texture2D hardButton = Content.Load<Texture2D>("hard_button");
            Texture2D hardButtonActive = Content.Load<Texture2D>("hard_button_active");

            Texture2D sonicButton = Content.Load<Texture2D>("sonic_button");
            Texture2D sonicButtonActive = Content.Load<Texture2D>("sonic_button_active");

            levelOneTexture = Content.Load<Texture2D>("level_one_texture");
            levelOneTextureActive = Content.Load<Texture2D>("levelOne_active");

            level2Texture = Content.Load<Texture2D>("level-2");
            level3Texture = Content.Load<Texture2D>("level-3");
            level4Texture = Content.Load<Texture2D>("level-4");
            #endregion

            buyJump = Content.Load<SpriteFont>("BuyJump");
            #endregion

            startmenu = new Startmenu(startmenuTexture, playButton, playButtonActive, shopButton, shopButtonActive, exitButton, exitButtonActive);
            shopmenu = new Shopmenu(shopmenuTexture, buyButton, buyButtonActive, backButton, backButtonActive);
            levelMenu = new LevelMenu(playButton, playButtonActive, levelmenuBackground, levelOneTexture, levelOneTextureActive, backButton, backButtonActive, easyButton, easyButtonActive, normalButton, normalButtonActive, hardButton, hardButtonActive, sonicButton, sonicButtonActive);
            pausemenu = new Pausemenu(pausemenuTexture, resumeButton, resumeButtonActive, leaveButton, leaveButtonActive);
            player = new Player(playerSprite, playerCrouch, healthTexture);
            gameOverMenu = new GameOverMenu(gameOverMenuTexture, backButton, backButtonActive, playButton, playButtonActive);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            lvl1 = new Level1(player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);

            effect = Content.Load<SoundEffect>("JUMP");
            

          
          

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //All kod i regonen nedan  alltså mellan de två gröna sträcken är av Kilian 
            //===========================================================================================================================
            #region Kilians del i Game1
            // De olika if-satserna  nedan som anroppar de olika "Update" metoderna 
            // är till för att när man till exempel spelaren inte ska fortsätta springa runt 
            // när man har pausat spelet.  


            //Updaterar "testleveln och playern"
            if (gamestates == Gamestates.inGame)
            #region Allt i test level och player
            {
                IsMouseVisible = false;
                gamestates = lvl1.Update(gameTime, player, effect);

                camera.Update(player.position);
                lvl1.Update(gameTime, player, effect);
            }
            #endregion

            //Lämnar spelet
            if (gamestates == Gamestates.exitgame)
            {
                Exit();
            }


            //Updaterar start menyn
            if (gamestates == Gamestates.startmenu)
            {
                IsMouseVisible = true;
                gamestates = startmenu.Update();
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Delete))
                    Exit();
            }


            //Updaterar level menyn
            if (gamestates == Gamestates.levelmenu)
            {
                IsMouseVisible = true;
                gamestates = levelMenu.Update(player);
            }


            //Updaterar paus menyn 
            if (gamestates == Gamestates.pausemenu)
            {
                IsMouseVisible = true;
                gamestates = pausemenu.Update();

                if (gamestates == Gamestates.startmenu)
                {
<<<<<<< HEAD
                    lvl1 = new Level1(player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);
=======
                    //Nollställer lvl1 när man lämnar spelet.
                    lvl1 = new Level1(player, groundBlockTexture, damagesprite, munkSprite, grenSprite);

                    //Nollställer
>>>>>>> 97348797a00f7a208e4ce2e351492e2b2ae2d875
                    player = new Player(playerSprite, playerSprite, healthTexture);
                }
            }


            //Updaterar shop menyn
            if (gamestates == Gamestates.shopmenu)
            {
                IsMouseVisible = true;
                gamestates = shopmenu.Update(player);
            }


            //Updaterar gameover menyn
            if (gamestates == Gamestates.gameOverMenu)
            {
<<<<<<< HEAD
                lvl1 = new Level1(player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);
=======
                //Nollställertt lvl1 när man har förlorat.
                lvl1 = new Level1(player, groundBlockTexture, damagesprite, munkSprite, grenSprite);

                //Nollställer playern när man har förlorat. 
>>>>>>> 97348797a00f7a208e4ce2e351492e2b2ae2d875
                player = new Player(playerSprite, playerSprite, healthTexture);

                IsMouseVisible = true;
                gamestates = gameOverMenu.Update(player);
            }

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // De olika "Draw" metoderna nedan anroppas beroende på i vilken värde "gamestates" har. 

            //Ritar ut startmenyn.
            if (gamestates == Gamestates.startmenu)
            {
                spriteBatch.Begin();
                startmenu.Draw(spriteBatch);
                spriteBatch.End();
            }


            //Ritar ut levelmenyn.
            if (gamestates == Gamestates.levelmenu)
            {
                spriteBatch.Begin();
                levelMenu.Draw(spriteBatch);
                spriteBatch.End();
            }


            //Ritar ut shopmenyn.
            if (gamestates == Gamestates.shopmenu)
            {
                spriteBatch.Begin();
                shopmenu.Draw(spriteBatch, buyJump);
                spriteBatch.End();
            }


            //Ritar ut leveln och player.
            if (gamestates == Gamestates.inGame)

            #region InGame Draw (Samuel)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);

                for (float i = 0; i < 100; i++)
                {
                    spriteBatch.Draw(background, backgroundTest, Color.White);
                    backgroundTest = new Vector2(backgroundWidth * i, 0);
                }

                lvl1.Draw(spriteBatch);
                player.Draw(spriteBatch);
               
                spriteBatch.End();
            }
            #endregion


            //Ritar ut pausmenyn.
            if (gamestates == Gamestates.pausemenu)
            {
                spriteBatch.Begin();
                pausemenu.Draw(spriteBatch);
                spriteBatch.End();
            }


            //Ritar ut gameovermenyn.
            if (gamestates == Gamestates.gameOverMenu)
            {
                spriteBatch.Begin();
                gameOverMenu.Draw(spriteBatch);
                spriteBatch.End();
            }
            #endregion
            //===========================================================================================================================
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
