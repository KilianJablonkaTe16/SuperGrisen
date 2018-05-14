using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpringandeGris
{
    enum ClickCombos { up, down, nothing }

    abstract class SuperMenu
    {

        protected List<SuperButtons> buttonLista = new List<SuperButtons>();
        protected Texture2D menuTexture;
        protected KeyboardState nowButtonState, lastButtonState;
        protected MouseState nowMouseState, lastMouseState;
        protected SpriteFont buyJump;

        protected bool keysUsed = false;
        protected int valdKnapp = -1, gammalValdKnapp = -1, whichButtonPressed;


        // Draw metoden som alla menyerna använder.
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuTexture, Vector2.Zero, Color.White);

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
                if (buttonLista[i].MouseOnButton() == ButtonLook.lookingButton)
                {
                    buttonLista[i].Update(ButtonLook.lookingButton);
                }

                if (buttonLista[i].MouseOnButton() == ButtonLook.normalButton)
                {
                    buttonLista[i].Update(ButtonLook.normalButton);
                }
            }
        }

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

        //En metod som "resterar" alla knapparnas texture
        // och resetar knapp värdet på knapp. 
        protected void ResetingButtos()
        {
            valdKnapp = -1;
            gammalValdKnapp = -1;
            buttonLista[0].Update(ButtonLook.normalButton);
            buttonLista[1].Update(ButtonLook.normalButton);
            buttonLista[2].Update(ButtonLook.normalButton);
        }

        // Koller om man har tryckt på en ny knapp, använt muspekaren och sätter gammalknapp
        // till den knappen som va markerad innan den som är markerad nu. 
        protected void GettingNewValues ()
        {
            nowButtonState = Keyboard.GetState();
            nowMouseState = Mouse.GetState();
            gammalValdKnapp = valdKnapp;
        }

        // Metod som gör så att du markerar första/översta knappen. 
        protected bool FirtButtonActive ()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && valdKnapp == -1 && gammalValdKnapp == -1  || Keyboard.GetState().IsKeyDown(Keys.Down) && valdKnapp == -1 && gammalValdKnapp == -1)
                return true;

            else
                return false;
        }

    }
}
