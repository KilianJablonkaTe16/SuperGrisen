using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{
    //Allt i denna klass gjordes av Kilian
    /*
     * De flesta sakerna som finns i denna klass bekrivs redan i startmenyn klassen, det ända som varierar.
     * Det ända som varierar är att det skapas inte lika många knappar och att knapparna retunerar andra värden när man trycker på dem.
     * Så de sakrena som inte är kommenterade i denna klass kommer vara komenterade i Startmenu klassen om kommentarer behövs.
    */

    class LevelMenu :SuperMenu
    {
        private bool button1Pressed = false, button2Pressed = false;
        private string whichLevel, whichDifficulty;

        public LevelMenu(Texture2D playButton, Texture2D playButtonActive, Texture2D levelmenyBackground, Texture2D level1Texture, Texture2D level1TextureHover, Texture2D level2Texture, Texture2D level2TextureHover, Texture2D level3Texture, Texture2D level3TextureHover, Texture2D level4Texture, Texture2D level4TextureHover, Texture2D backButton, Texture2D backButtonActive, Texture2D easyButton, Texture2D easyButtonActive, Texture2D normalButton, Texture2D normalButtonActive, Texture2D hardButton, Texture2D hardButtonActive, Texture2D sonicButton, Texture2D sonicButtonActive)
        {
            buttonLista.Add(new SuperButtons(playButton, playButtonActive, new Vector2(50, 200)));

            buttonLista.Add(new SuperButtons(easyButton, easyButtonActive, new Vector2(50, 350)));
            buttonLista.Add(new SuperButtons(normalButton, normalButtonActive, new Vector2(50, 500)));
            buttonLista.Add(new SuperButtons(hardButton, hardButtonActive, new Vector2(50, 650)));
            buttonLista.Add(new SuperButtons(sonicButton, sonicButtonActive, new Vector2(50, 800)));

            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 950)));

            buttonLista.Add(new SuperButtons(level1Texture, level1TextureHover, new Vector2(500, 20)));
            buttonLista.Add(new SuperButtons(level2Texture, level2TextureHover, new Vector2(500, 580)));
            buttonLista.Add(new SuperButtons(level3Texture, level3TextureHover, new Vector2(1200, 20)));
            buttonLista.Add(new SuperButtons(level4Texture, level4TextureHover, new Vector2(1200, 580)));

            menuTexture = levelmenyBackground;
        }

        public Gamestates Update(Player player)
        {
            // Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                keysUsed = true;
            }

            // Nedan är det som gör så att du kan välja knapp med muspekaren. 
            #region Funktionalitet för muspekar användning
            foreach (SuperButtons button in buttonLista)
            {
                if (nowMouseState.Position != lastMouseState.Position)
                {
                    keysUsed = false;
                }

                if (keysUsed == false)
                {
                    // Vad metoden gör beskrivs i SuperMenus klassen.
                    ButtonListForloop();

                    #region Gamstates ändring för musanvändning
                    //Nedan ändra på  gamestates beroende på wilken knapp man tycker på.

                    //If-satsen statar spelet när man trycker på Play knappen.
                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton && button1Pressed == true && button2Pressed == true)
                    {
                        ResetingLevelMenu();
                        return Gamestates.inGame;
                    }

                    //If-satsen gör ställer in spelet på den lättaste svårighetsgraden när man trycker på Easy knappen.
                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 4;
                        player.jumpHeight = -18;
                        player.gravity.Y = 0.4f;
                        button1Pressed = true;
                        whichDifficulty = "Easy";
                    }

                    //If-satsen gör ställer in spelet på den lagomsvåra svårighetsgraden när man trycker på Normal knappen.
                    if (buttonLista[2].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 8;
                        player.jumpHeight = -21;
                        player.gravity.Y = 0.6f;
                        button1Pressed = true;
                        whichDifficulty = "Normal";
                    }

                    //If-satsen gör ställer in spelet på den svåra svårighetsgraden när man trycker på Hard knappen.
                    if (buttonLista[3].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 16;
                        player.jumpHeight = -24;
                        player.gravity.Y = 0.8f;
                        button1Pressed = true;
                        whichDifficulty = "Hard";
                    }

                    //If-satsen gör ställer in spelet på den svåraste svårighetsgraden när man trycker på Sonic? knappen.
                    if (buttonLista[4].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 32;
                        player.jumpHeight = -28;
                        player.gravity.Y = 1f;
                        button1Pressed = true;
                        whichDifficulty = "Sonic?";
                    }

                    if (buttonLista[5].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        ResetingLevelMenu();
                        return Gamestates.startmenu;
                    }

                    if (buttonLista[6].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        button2Pressed = true;
                        whichLevel = "Level 1";
                    }

                    if (buttonLista[7].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        button2Pressed = true;
                        whichLevel = "Level 2";
                    }

                    if (buttonLista[8].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        button2Pressed = true;
                        whichLevel = "Level 3";
                    }

                    if (buttonLista[9].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        button2Pressed = true;
                        whichLevel = "Level 4";
                    }

                    lastMouseState = nowMouseState;
                    valdKnapp = -1;
                    gammalValdKnapp = -1;                    
                    #endregion
                }
            }
            #endregion

            // Nedan är det som gör så att du kan välja knapp med piltangenter.
            #region Piltangent funktionaliteten

            usingKeys(10);
                

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && valdKnapp >= 6)
            {
                ResetingButtos(10);
                valdKnapp = 0;
                buttonLista[0].Update(ButtonLook.lookingButton);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && valdKnapp < 6)
            {
                ResetingButtos(10);
                valdKnapp = 6;
                buttonLista[6].Update(ButtonLook.lookingButton);
            }
            lastButtonState = nowButtonState;

            //Nedan ändras gamestates beroende på vilken knapp man "aktiverar". 
            #region Gamestate retunering

            //If-satsen gör så att man startar spelet när man trycker på Play knappen.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0 && button1Pressed == true && button2Pressed == true)
            {
                button1Pressed = false;
                button2Pressed = false;
                whichDifficulty = "";
                whichLevel = "";
                return Gamestates.inGame;
            }


            //If-satsen gör ställer in spelet på den lättaste svårighetsgraden när man trycker på Easy knappen.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                player.velocity.X = 4;
                player.jumpHeight = -18;
                player.gravity.Y = 0.4f;
                button1Pressed = true;
                whichDifficulty = "Easy";
            }

            //If-satsen gör ställer in spelet på den lagomsvåra svårighetsgraden när man trycker på Normal knappen.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 2)
            {
                player.velocity.X = 8;
                player.jumpHeight = -21;
                player.gravity.Y = 0.6f;
                button1Pressed = true;
                whichDifficulty = "Normal";
            }

            //If-satsen gör ställer in spelet på den svåra svårighetsgraden när man trycker på Hard knappen.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 3)
            {
                player.velocity.X = 16;
                player.jumpHeight = -24;
                player.gravity.Y = 0.8f;
                button1Pressed = true;
                whichDifficulty = "Hard";
            }

            //If-satsen gör ställer in spelet på den svåraste svårighetsgraden när man trycker på Sonic? knappen.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 4)
            {
                player.velocity.X = 32;
                player.jumpHeight = -28;
                player.gravity.Y = 1f;
                button1Pressed = true;
                whichDifficulty = "Sonic?";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 6)
            {
                button2Pressed = true;
                whichLevel = "Level 1";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 7)
            {
                button2Pressed = true;
                whichLevel = "Level 2";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 8)
            {
                button2Pressed = true;
                whichLevel = "Level 3";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 9)
            {
                button2Pressed = true;
                whichLevel = "Level 4";
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 5)
            {
                ResetingButtos(buttonLista.Count);
                ResetingLevelMenu();
                return Gamestates.startmenu;
            }
            #endregion
            #endregion

            else
                return Gamestates.levelmenu;

        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont comicSansFont)
        {
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);

            foreach (SuperButtons pauseButton in buttonLista)
            {
                pauseButton.Draw(spriteBatch);
            }

            //Skriver ut hur många munkar man har.
            spriteBatch.DrawString(comicSansFont, "Difficulty: " + whichDifficulty, new Vector2(25, 20), Color.White);

            //Skriver ut hur 
            spriteBatch.DrawString(comicSansFont, "Level: " + whichLevel, new Vector2(25, 100), Color.White);
        }

        // En metod som resetar texturen av alla knappar och gör lite till.
        protected void ResetingLevelMenu()
        {
            whichDifficulty = "";
            whichLevel = "";
            button1Pressed = false;
            button2Pressed = false;
        }
    }
}
