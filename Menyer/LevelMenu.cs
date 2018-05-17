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
    class LevelMenu:SuperMenu
    {
        public LevelMenu(Texture2D playButton, Texture2D playButtonActive, Texture2D levelmenyBackground,Texture2D levelOneTexture, Texture2D levelOneTextureActive, Texture2D backButton, Texture2D backButtonActive, Texture2D easyButton, Texture2D easyButtonActive, Texture2D normalButton, Texture2D normalButtonActive, Texture2D hardButton, Texture2D hardButtonActive, Texture2D sonicButton, Texture2D sonicButtonActive)
        {
            buttonLista.Add(new SuperButtons(playButton, playButtonActive, new Vector2(50, 200)));

            buttonLista.Add(new SuperButtons(easyButton, easyButtonActive, new Vector2(50, 350)));
            buttonLista.Add(new SuperButtons(normalButton, normalButtonActive, new Vector2(50, 500)));
            buttonLista.Add(new SuperButtons(hardButton, hardButtonActive, new Vector2(50, 650)));
            buttonLista.Add(new SuperButtons(sonicButton, sonicButtonActive, new Vector2(50, 800)));

            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 950)));

            buttonLista.Add(new SuperButtons(levelOneTexture, levelOneTextureActive, new Vector2(500, 20)));
            buttonLista.Add(new SuperButtons(levelOneTexture, levelOneTextureActive, new Vector2(1200, 20)));
            buttonLista.Add(new SuperButtons(levelOneTexture, levelOneTextureActive, new Vector2(500, 580)));
            buttonLista.Add(new SuperButtons(levelOneTexture, levelOneTextureActive, new Vector2(1200, 580)));

            menuTexture = levelmenyBackground;
        }

        public Gamestates Update(Player player)
        {
            // Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
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
                    //=================================================================================================================
                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        return Gamestates.inGame;
                    }

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 4;
                        player.jumpHeight = -18;
                        player.gravity.Y = 0.4f;
                    }

                    if (buttonLista[2].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 8;
                        player.jumpHeight = -21;
                        player.gravity.Y = 0.6f;
                    }

                    if (buttonLista[3].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 16;
                        player.jumpHeight = -24;
                        player.gravity.Y = 0.8f;
                    }

                    if (buttonLista[4].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        player.velocity.X = 32;
                        player.jumpHeight = -28;
                        player.gravity.Y = 1f;
                    }

                    if (buttonLista[5].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        return Gamestates.startmenu;
                    }

                    lastMouseState = nowMouseState;
                    valdKnapp = -1;
                    gammalValdKnapp = -1;
                    //=================================================================================================================
                    #endregion
                }
            }
            #endregion

            // Nedan är det som gör så att du kan välja knapp med piltangenter.
            #region Piltangent funktionaliteten
            //=============================================================================================================================================================================
            if (FirtButtonActive() == true)
            {
                valdKnapp++;
                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.up && valdKnapp >= 0)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp--;

                if (valdKnapp == -1)
                    valdKnapp++;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.down && valdKnapp <= 5 && gammalValdKnapp != -1)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp++;

                if (valdKnapp == 6)
                    valdKnapp--;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            lastButtonState = nowButtonState;

            //Nedan ändras gamestates beroende på vilken knapp man "aktiverar". 
            #region Gamestate retunering
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0 && whichButtonPressed != 0)
            {
                return Gamestates.inGame;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                player.velocity.X = 4;
                player.jumpHeight = -18;
                player.gravity.Y = 0.4f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 2)
            {
                player.velocity.X = 8;
                player.jumpHeight = -21;
                player.gravity.Y = 0.6f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 3)
            {
                player.velocity.X = 16;
                player.jumpHeight = -24;
                player.gravity.Y = 0.8f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 4)
            {
                player.velocity.X = 32;
                player.jumpHeight = -28;
                player.gravity.Y = 1f;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 5)
            {
                ResetingLevelMenuButtons();
                return Gamestates.startmenu;
            }
            #endregion
            //=====================================================================================================================================================
            #endregion


            else if (Keyboard.GetState().IsKeyDown(Keys.M))
                return Gamestates.inGame;

            else
                return Gamestates.levelmenu;

        }

        // En metod som resetar texturen av alla knappar och gör lite till.
        protected void ResetingLevelMenuButtons()
        {
            valdKnapp = -1;
            gammalValdKnapp = -1;
            buttonLista[0].Update(ButtonLook.normalButton);
            buttonLista[1].Update(ButtonLook.normalButton);
            buttonLista[2].Update(ButtonLook.normalButton);
            buttonLista[3].Update(ButtonLook.normalButton);
            buttonLista[4].Update(ButtonLook.normalButton);
            buttonLista[5].Update(ButtonLook.normalButton);
        }
    }
}
