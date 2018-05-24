using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{
    enum ClickCombos { up, down, nothing }

    //Allt i denna klass gjordes av Kilian

    abstract class SuperMenu
    {
        //En lista där alla knappar från alla menyer läggs till.  
        protected List<SuperButtons> buttonLista = new List<SuperButtons>();

        //Andra variabler som behövdes. 
        protected Texture2D menuTexture;
        protected KeyboardState nowButtonState, lastButtonState;
        protected MouseState nowMouseState, lastMouseState;
        protected SpriteFont buyJump;

        protected bool keysUsed = false;
        protected int valdKnapp = -1, gammalValdKnapp = -1;


        // Draw metoden som alla menyerna använder.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //Ritar ut bakgrunden för den menyn du befiner dig i. 
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);

            //Ritar ut alla knappar i den menyn du befiner dig.
            foreach (SuperButtons buttons in buttonLista)
            {
                buttons.Draw(spriteBatch);
            }
        }


        // Metoden kör en forloop som loopar igenom buttonLista.
        protected void ButtonListForloop ()
        {
            for (int i = 0; i < buttonLista.Count; i++)
            {
                //Skickar in ett värde av ButtonLook så att knapparna vet när de har markerats
                if (buttonLista[i].MouseOnButton() == ButtonLook.lookingButton)
                {
                    buttonLista[i].Update(ButtonLook.lookingButton);
                }

                //Skickar in ett värde av ButtonLook så att knapparna vet när de inte är markerade
                if (buttonLista[i].MouseOnButton() == ButtonLook.normalButton)
                {
                    buttonLista[i].Update(ButtonLook.normalButton);
                }
            }
        }


        //En metod som retunerar värdet om man vill markera en knapp över eller under den man är på.
        //Den ser också till att man inte kan hålla in piltangenterna utan man måste släppa 
        //tangenten för att och sedan trycka ner den igen för att markera knappen under eller över.
        protected ClickCombos ClickCombo(KeyboardState nowButton, KeyboardState lastButton)
        { 
            if (nowButton != lastButton && Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                return ClickCombos.up;
            }

            if (nowButton != lastButton && Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                return ClickCombos.down;
            }
            
            else
            {
                return ClickCombos.nothing;
            }

        }


        //En metod som "nollställer" alla knapparnas texture
        //och resetar knapp värdet på vald och gammalknapp. 
        protected void ResetingButtos()
        {
            valdKnapp = -1;
            gammalValdKnapp = -1;
            buttonLista[0].Update(ButtonLook.normalButton);
            buttonLista[1].Update(ButtonLook.normalButton);
            buttonLista[2].Update(ButtonLook.normalButton);
        }


        // Kollar om man har tryckt på en ny knapp, använt muspekaren och sätter gammalknapp
        // till den knappen som va markerad innan den som är markerad nu. 
        protected void GettingNewValues ()
        {
            nowButtonState = Keyboard.GetState();
            nowMouseState = Mouse.GetState();
            gammalValdKnapp = valdKnapp;
        }


        //En metod som retunerar en bool beroende på om man kommer in i en meny och andvänder piltangenterna.
        //Metoden används för att markera den första knappen om man trycke på up eller ner tangenten.  
        protected bool FirtButtonActive ()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && valdKnapp == -1 && gammalValdKnapp == -1  || Keyboard.GetState().IsKeyDown(Keys.Down) && valdKnapp == -1 && gammalValdKnapp == -1)
                return true;

            else
                return false;
        }

    }
}
