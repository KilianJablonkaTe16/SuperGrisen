using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{
    //Allt i denna klass gjordes av Kilian

    class Startmenu :SuperMenu
    {
        //En boll som används för att stänga av spelet. 
        public bool exitGame = false;

        //Konstruktorn
        public Startmenu(Texture2D startmenu, Texture2D playButton, Texture2D playButtonActive, Texture2D shopButton, Texture2D shopButtonActive, Texture2D exitButton, Texture2D exitButtonActive)
        {
            //Här skapas knapparna som finns i startmenyn.
            buttonLista.Add(new SuperButtons(playButton, playButtonActive, new Vector2(50, 100)));
            buttonLista.Add(new SuperButtons(shopButton, shopButtonActive, new Vector2(50, 250)));
            buttonLista.Add(new SuperButtons(exitButton, exitButtonActive, new Vector2(50, 400)));

            menuTexture = startmenu;
        }

        public Gamestates Update()
        {
            // Vad metoden gör beskirvs i SuperMenus.
            GettingNewValues();

            //If-satsen kollar om man använder piltangenterna för att markera knapparna
            //om man gör det så "deaktiverar" den updateringen av muspekar funktionaliteten.
            if (Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                keysUsed = true;
            }

            // Nedan är det som gör så att du kan välja knapp med muspekaren. 
            #region Funktionalitet för muspekar användning
            foreach (SuperButtons button in buttonLista)
            {
                //If-satsen kollar om man använder muspekaren för att markera knapparna
                //om man gör det så "deaktiverar" den updateringen av piltangent funktionaliteten.  
                if (nowMouseState.Position != lastMouseState.Position)
                {
                    keysUsed = false;
                }

                if (keysUsed == false)
                {
                    //Vad metoden gör beskrivs i SuperMenus klassen.
                    ButtonListForloop();

                    #region Gamstates ändring för musanvändning
                    //Nedan ändra på  gamestates beroende på wilken knapp man tycker på.

                    //Om man trycker på första knappen går man in i levelmenyn.
                    if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.levelmenu;
                    }
                    
                    //Om man tycker på andra knappen går man in i shopmenyn.
                    if (buttonLista[1].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.shopmenu;
                    }

                    //Om man trycker på tredje knappen stänger man av spelet
                    if (buttonLista[2].MouseOnButton() == ButtonLook.clickingButton)
                    {
                        return Gamestates.exitgame;
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

            //Här används en metod i if-satsen som jag förklarar i SuperMenu
            //Det if-satsen gör är att den markerar den första knappen när man vill använda piltangenterna.
            if (FirtButtonActive() == true)
            {
                valdKnapp++;
                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
                buttonLista[1].Update(ButtonLook.normalButton);
                buttonLista[2].Update(ButtonLook.normalButton);
            }

            //Denna if-sats gör så att man kan markera en kanpp med piltangenterna över den som är 
            //markerad om det finns en knapp över den.
            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.up && valdKnapp >= 0)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp--;

                //If-satsen gör så att man inte kan markera en knapp som inte finns.
                if (valdKnapp == -1)
                    valdKnapp++;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }


            //Denna if-sats gör så att man kan markera en kanpp med piltangenterna under den som är 
            //markerad om det finns en knapp under den.
            if (ClickCombo(nowButtonState, lastButtonState) == ClickCombos.down && valdKnapp <= 2 && gammalValdKnapp != -1)
            {
                buttonLista[valdKnapp].Update(ButtonLook.normalButton);
                valdKnapp++;

                //If-satsen gör så att man inte kan markera en knapp som inte finns.
                if (valdKnapp == 3)
                    valdKnapp--;

                buttonLista[valdKnapp].Update(ButtonLook.lookingButton);
            }

            lastButtonState = nowButtonState;

            //Nedan ändras gamestates beroende på vilken knapp man "aktiverar".

            #region Gamestate retunering


            //Om man har markerat den första knappen med hjälp av pitangenterna 
            //och man har tryckt på enter så går man in i levelmenyn.
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0)
            {
                ResetingButtos();
                return Gamestates.levelmenu;
            }


            //Om man har markerat den andra knappen med hjälp av pitangenterna 
            //och man har tryckt på enter så går man in i shopmenyn.
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 1)
            {
                ResetingButtos();
                return Gamestates.shopmenu;
            }


            //Om man har markerat den tredje knappen med hjälp av pitangenterna 
            //och man har tryckt på enter så lämnar man spelet.
            else if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 2)
            {
                return Gamestates.exitgame;
            }
            #endregion

            // Om ingen knapp aktiveras via vänsterklick på musen eller enter
            // när man har markerat den med piltangenterna så stanar man i startscrennen. 
            else
            {
                return Gamestates.startmenu;
            }
            #endregion
        }
    }
}
