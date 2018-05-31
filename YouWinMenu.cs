using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringandeGris
{
    class YouWinMenu : SuperMenu
    {
        public YouWinMenu(Texture2D menuBackground, Texture2D backButton, Texture2D backButtonActive)
        {
            menuTexture = menuBackground;
            buttonLista.Add(new SuperButtons(backButton, backButtonActive, new Vector2(50, 100)));
        }

        public Gamestates Update()
        {
            foreach (SuperButtons Button in buttonLista)
            {
                if (nowMouseState.Position != lastMouseState.Position)
                {
                    keysUsed = false;
                }

                if (keysUsed == false)
                {
                    Button.MouseOnButton();

                    // Vad metoden gör beskrivs i SuperMenus klassen.
                    ButtonListForloop();
                }

                //Nedan ändrar if-satserna på gamestatsen beroende på vilken knapp man trycker på. 

                if (buttonLista[0].MouseOnButton() == ButtonLook.clickingButton)
                {
                    return Gamestates.startmenu;
                }

                lastMouseState = nowMouseState;
            }

            usingKeys(1);

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && valdKnapp == 0)
            {
                ResetingButtos(buttonLista.Count);
                return Gamestates.startmenu;
            }

            else
                return Gamestates.youWinMenu;
        }



    }
}
