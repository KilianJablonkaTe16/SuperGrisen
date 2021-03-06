﻿using Microsoft.Xna.Framework;
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


    enum Gamestates { inGame, startmenu, pausemenu, shopmenu, levelmenu, gameOverMenu, youWinMenu, exitgame }



    public class Game1 : Game
    {
        SoundEffect effect;
        SoundEffect eatingMunk;
        

        Gamestates gamestates = new Gamestates();

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont comicSansFont;
        Level1 lvl1;
        Camera camera = new Camera();
        public static Random rng = new Random();


        public Texture2D background, playButton, playButtonActive, playeButtonPressed, leaveButton, leaveButtonActive, 
                         backButton, backButtonActive, flyingsprite, level1Texture, level2Texture, level3Texture, level4Texture, 
                         damagesprite, groundBlockTexture, snowGroundTexture, munkSprite, playerSprite, healthTexture, levelOneTexture, 
                         grenSprite, levelOneTextureActive, rainTexture, snowTexture;



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
        YouWinMenu youWinMenu;
        Rain rain;
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
            snowGroundTexture = Content.Load<Texture2D>("snwo_groundblock");
            damagesprite = Content.Load<Texture2D>("Mario_runing");
            background = Content.Load<Texture2D>("Forest-31");
            munkSprite = Content.Load<Texture2D>("sockerMunk_texture");
            backgroundWidth = background.Width;
            backgroundTest = new Vector2(0, 0);

            rainTexture = Content.Load<Texture2D>("rain");
            snowTexture = Content.Load<Texture2D>("big_snowflake2");

            //Meny bakgrunder
            #region Meny Bakgrunder
            Texture2D startmenuTexture = Content.Load<Texture2D>("title_screen_almost");
            Texture2D shopmenuTexture = Content.Load<Texture2D>("new_shop_screen");
            Texture2D pausemenuTexture = Content.Load<Texture2D>("liten_pausescreen_test");
            Texture2D levelmenuTexture = Content.Load<Texture2D>("levelmeny_bakgrund_utan");
            Texture2D gameOverMenuTexture = Content.Load<Texture2D>("new_gameOver_screen");
            Texture2D youWinMenuTexture = Content.Load<Texture2D>("wining_screen");
            #endregion

            //Alla knappterturers
            #region Knapptexturers
            playButton = Content.Load<Texture2D>("playButton");
            playButtonActive = Content.Load<Texture2D>("playButton_active");

            Texture2D shopButton = Content.Load<Texture2D>("shopButton");
            Texture2D shopButtonActive = Content.Load<Texture2D>("shopButton_active");

            Texture2D exitButton = Content.Load<Texture2D>("exitButton");
            Texture2D exitButtonActive = Content.Load<Texture2D>("exitButton_active");

            Texture2D resumeButton = Content.Load<Texture2D>("resumeButton");
            Texture2D resumeButtonActive = Content.Load<Texture2D>("resumeButton_active");

            leaveButton = Content.Load<Texture2D>("leaveButton");
            leaveButtonActive = Content.Load<Texture2D>("leaveButton_active");

            backButton = Content.Load<Texture2D>("backButton");
            backButtonActive = Content.Load<Texture2D>("backButton_active");

            Texture2D buyButton = Content.Load<Texture2D>("buyButton");
            Texture2D buyButtonActive = Content.Load<Texture2D>("buyButton_active");

            Texture2D easyButton = Content.Load<Texture2D>("easy_button");
            Texture2D easyButtonActive = Content.Load<Texture2D>("easy_button_active");

            Texture2D normalButton = Content.Load<Texture2D>("normal_button");
            Texture2D normalButtonActive = Content.Load<Texture2D>("normal_button_active");

            Texture2D hardButton = Content.Load<Texture2D>("hard_button");
            Texture2D hardButtonActive = Content.Load<Texture2D>("hard_button_active");

            Texture2D sonicButton = Content.Load<Texture2D>("sonic_button");
            Texture2D sonicButtonActive = Content.Load<Texture2D>("sonic_button_active");

            Texture2D level1Texture = Content.Load<Texture2D>("level1_button");
            Texture2D level1TextureHover = Content.Load<Texture2D>("level1_button_active");

            Texture2D level2Texture = Content.Load<Texture2D>("level2_button");
            Texture2D level2TextureHover = Content.Load<Texture2D>("level2_button_active");

            Texture2D level3Texture = Content.Load<Texture2D>("level3_button");
            Texture2D level3TextureHover = Content.Load<Texture2D>("level3_button_active");

            Texture2D level4Texture = Content.Load<Texture2D>("level4_button");
            Texture2D level4TextureHover = Content.Load<Texture2D>("level4_button_active");
            #endregion

            comicSansFont = Content.Load<SpriteFont>("BuyJump");
            #endregion

            startmenu = new Startmenu(startmenuTexture, playButton, playButtonActive, shopButton, shopButtonActive, exitButton, exitButtonActive);
            shopmenu = new Shopmenu(shopmenuTexture, buyButton, buyButtonActive, backButton, backButtonActive);
            levelMenu = new LevelMenu(playButton, playButtonActive, levelmenuTexture, level1Texture, level1TextureHover, level2Texture, level2TextureHover, level3Texture, level3TextureHover, level4Texture, level4TextureHover, backButton, backButtonActive, easyButton, easyButtonActive, normalButton, normalButtonActive, hardButton, hardButtonActive, sonicButton, sonicButtonActive);
            pausemenu = new Pausemenu(pausemenuTexture, resumeButton, resumeButtonActive, leaveButton, leaveButtonActive);
            player = new Player(playerSprite, playerCrouch, healthTexture);
            gameOverMenu = new GameOverMenu(gameOverMenuTexture, backButton, backButtonActive, playButton, playButtonActive);
            youWinMenu = new YouWinMenu(youWinMenuTexture, backButton, backButtonActive);

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

            lvl1 = new Level1(snowGroundTexture, snowTexture, rainTexture ,player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);

            effect = Content.Load<SoundEffect>("JUMP");
            eatingMunk = Content.Load<SoundEffect>("The Heavy eating his Sandvich");

          
          

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
                gamestates = lvl1.Update(gameTime, player, effect, eatingMunk);

                camera.Update(player.position);
                lvl1.Update(gameTime, player, effect, eatingMunk);
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
                    //Nollställer lvl1 när man lämnar spelet.
                    lvl1 = new Level1(snowGroundTexture, snowTexture, rainTexture, player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);

                    //Nollställer
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

                //Nollställertt lvl1 när man har förlorat.
                lvl1 = new Level1(snowGroundTexture, snowTexture, rainTexture, player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);

                //Nollställer playern när man har förlorat. 
                player = new Player(playerSprite, playerSprite, healthTexture);

                IsMouseVisible = true;
                gamestates = gameOverMenu.Update(player);
            }

            if (gamestates == Gamestates.youWinMenu)
            {
                IsMouseVisible = true;
                gamestates = youWinMenu.Update();
                //Nollställer lvl1 när man lämnar spelet.
                lvl1 = new Level1(snowGroundTexture, snowTexture, rainTexture, player, groundBlockTexture, damagesprite, munkSprite, grenSprite, flyingsprite);

                //Nollställer
                player = new Player(playerSprite, playerSprite, healthTexture);
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
                levelMenu.Draw(spriteBatch, comicSansFont);
                spriteBatch.End();
            }


            //Ritar ut shopmenyn.
            if (gamestates == Gamestates.shopmenu)
            {
                spriteBatch.Begin();
                shopmenu.Draw(spriteBatch, comicSansFont);
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

            //Ritar ut youwinmenyn
            if(gamestates == Gamestates.youWinMenu)
            {
                spriteBatch.Begin();
                youWinMenu.Draw(spriteBatch);   
                spriteBatch.End();
            }
            #endregion
            //===========================================================================================================================
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
