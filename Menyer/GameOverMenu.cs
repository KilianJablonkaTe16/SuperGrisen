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
    class GameOverMenu :SuperMenu
    {
        public GameOverMenu(Texture2D gameOverMenuTexture, Texture2D backButton, Texture2D backButtonActive, Texture2D playButton, Texture2D playButtonActive)
        {
            menuTexture = gameOverMenuTexture;
            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 150)));
            buttonLista.Add(new SuperButtons(playButton, playButtonActive, new Vector2(50, 300)));
        }

        public Gamestates Update(Player player)
        {
            //Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();

            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                keysUsed = true;
            }

            //Nedan är det som gör så att du kan välja en knapp med muspekaren. 
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
                    //Nedan ändras på  gamestates beroende på vilken knapp man tycker på.
                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        return Gamestates.startmenu;
                    }

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton && lastMouseState.LeftButton != ButtonState.Pressed)
                    {
                        return Gamestates.levelmenu;
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

            //
            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.down && valdKnapp <= 2 && gammalValdKnapp != -1)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp++;

                if (valdKnapp == 0)
                    valdKnapp--;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            lastButtonState = nowButtonState;

            //Nedan ändras gamestates beroende på vilken knapp man "aktiverar". 
            #region Gamestate retunering
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                return Gamestates.startmenu;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 2)
            {
                return Gamestates.levelmenu;
            }
            #endregion
            #endregion

            //Gör så att man stannar i gameOver menyn om man inte trycker på en knapp.
            else
                return Gamestates.gameOverMenu;
        }
    }
}
