using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{   

    class Startmenu:SuperMenu
    {

        public bool exitGame = false;

        //Konstruktorn
        public Startmenu(Texture2D startmenu, Texture2D playButton, Texture2D playButtonActive, Texture2D shopButton, Texture2D shopButtonActive, Texture2D exitButton, Texture2D exitButtonActive)
        {
            buttonLista.Add(new SuperButtons(playButton, playButtonActive, new Vector2(50, 100)));
            buttonLista.Add(new SuperButtons(shopButton, shopButtonActive, new Vector2(50, 250)));
            buttonLista.Add(new SuperButtons(exitButton, exitButtonActive, new Vector2(50, 400)));

            menuTexture = startmenu;
        }

        public Gamestates Update()
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
                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        whichButtonPressed = 0;
                        return Gamestates.levelmenu;
                    }

                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        whichButtonPressed = 1;
                        return Gamestates.shopmenu;
                    }

                    if (buttonLista[2].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        whichButtonPressed = 2;
                        return Gamestates.exitgame;
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
                buttonLista[1].Update(ButtonLook.normalButton);
                buttonLista[2].Update(ButtonLook.normalButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.up && valdKnapp >= 0)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp--;

                if (valdKnapp == -1)
                    valdKnapp++;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.down && valdKnapp <= 2 && gammalValdKnapp != -1)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp++;

                if (valdKnapp == 3)
                    valdKnapp--;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            lastButtonState = nowButtonState;

            //Nedan ändras gamestates beroende på vilken knapp man "aktiverar". 
            #region Gamestate retunering
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0)
            {
                ResetingButtos();
                whichButtonPressed = 0;
                return Gamestates.levelmenu;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                ResetingButtos();
                whichButtonPressed = 1;
                return Gamestates.shopmenu;
            }

            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 2)
            {
                whichButtonPressed = 3;
                return Gamestates.exitgame;
            }
            #endregion
            //=====================================================================================================================================================
            #endregion

            // Om ingen knapp aktiveras via vänsterklick på musen eller enter
            // när man har markerat den med piltangenterna så stanar man i startscrennen. 
            else
            {
                return Gamestates.startmenu;
            }
        }
    }
}
